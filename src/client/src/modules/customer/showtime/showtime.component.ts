import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SeatshowtimeComponent } from '../seatshowtime/seatshowtime.component';
import { CommonModule } from '@angular/common';
import { ShowtimeviewModel } from '../../../models/showtime/showtimeview.model';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { SHOWTIME_SERVICE } from '../../../constants/injection.constant';
import { IShowtimeServiceInterface } from '../../../services/showtime/showtime-service.interface';
import { ServicesModule } from '../../../services/services.module';
import { MoviesComponent } from '../movies/movies.component';

@Component({
  selector: 'app-showtime',
  imports: [CommonModule, RouterModule, ServicesModule, MoviesComponent],
  templateUrl: './showtime.component.html',
  styleUrl: './showtime.component.css',
})
export class ShowtimeComponent
  extends MasterDataListComponent<ShowtimeviewModel>
  implements OnInit
{
  public showtimes: ShowtimeviewModel[] = [];
  // public movies: MovieviewModel[] = [];
  selectedShowtime!: Date;
  constructor(
    @Inject(SHOWTIME_SERVICE)
    private readonly showtimeService: IShowtimeServiceInterface
  ) {
    super();
  }
  override ngOnInit(): void {
    this.getShowtimes();
  }
  private getShowtimes(): void {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const oneWeekLater = new Date();
    oneWeekLater.setDate(today.getDate() + 7);
    oneWeekLater.setHours(0, 0, 0, 0);

    this.showtimeService.getAll().subscribe((res) => {
      // Sắp xếp showtimes theo showDate
      this.showtimes = res
        // Lọc các showtime có showDate lớn hơn hoặc bằng hôm nay
        .filter(showtime => {
          const showDate = new Date(showtime.showDate);
          showDate.setHours(0, 0, 0, 0);
          return showDate >= today && showDate <= oneWeekLater;
        })
        .filter(
          (showtime, index, self) =>
            index === self.findIndex((s) => s.showDate === showtime.showDate) // Lọc các showtime trùng lặp
        )
        .sort(
          (a, b) =>
            new Date(a.showDate).getTime() - new Date(b.showDate).getTime()
        ).slice(0, 7); // Sắp xếp theo ngày tăng dần

      // Chọn suất chiếu đầu tiên mặc định
      if (this.showtimes.length > 0) {
        this.selectedShowtime = this.showtimes[0].showDate; // Cập nhật selectedShowtime
      }
    });
  }
  // Xử lý sự kiện khi nhấn nút
  onShowtimeClick(showtime: Date) {
    this.selectedShowtime = showtime; // Lưu giá trị của showtime được nhấn
    console.log('Selected Showtime:', this.selectedShowtime);
  }
}
