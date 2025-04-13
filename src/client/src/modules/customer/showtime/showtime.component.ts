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
  styleUrl: './showtime.component.css'
})
export class ShowtimeComponent extends MasterDataListComponent<ShowtimeviewModel> implements OnInit{
  public showtimes: ShowtimeviewModel[] = [];
  // public movies: MovieviewModel[] = [];
  selectedShowtime!: Date;
  constructor(
    @Inject(SHOWTIME_SERVICE) private readonly showtimeService: IShowtimeServiceInterface
  ) {
    super();
  }
  override ngOnInit(): void {
    this.getShowtimes();
  }
  private getShowtimes(): void {
    this.showtimeService.getAll().subscribe((res) => {
      this.showtimes = res.filter((showtime, index, self) =>
        index === self.findIndex(s => s.showDate === showtime.showDate)
      );
      // Chọn suất chiếu đầu tiên mặc định
      if (this.showtimes.length > 0) {
        this.selectedShowtime = this.showtimes[0].showDate;
      }
    });
}
  // Xử lý sự kiện khi nhấn nút
  onShowtimeClick(showtime: Date) {
    this.selectedShowtime = showtime;  // Lưu giá trị của showtime được nhấn
    console.log('Selected Showtime:', this.selectedShowtime);
  }



}
