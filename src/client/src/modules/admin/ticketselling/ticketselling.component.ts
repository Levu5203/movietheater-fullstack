import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faAngleLeft,
  faAngleRight,
  faAnglesLeft,
  faAnglesRight,
  faArrowLeft,
} from '@fortawesome/free-solid-svg-icons';
import { TiketsellingSelectseatComponent } from './tiketselling-selectseat/tiketselling-selectseat.component';
import { TicketsellingPaymentComponent } from './ticketselling-payment/ticketselling-payment.component';
import { TicketSellingService } from './ticketselling.service';
import { ShowtimeviewModel } from '../../../models/showtime/showtimeview.model';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';
import { ServicesModule } from '../../../services/services.module';
import {
  SHOWTIME_SERVICE,
} from '../../../constants/injection.constant';
import { IShowtimeServiceInterface } from '../../../services/showtime/showtime-service.interface';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { TicketsellingMoviesComponent } from "./ticketselling-movies/ticketselling-movies.component";

@Component({
  selector: 'app-ticketselling',
  imports: [
    FontAwesomeModule,
    CommonModule,
    FormsModule,
    ServicesModule,
    TicketsellingMoviesComponent
],
  templateUrl: './ticketselling.component.html',
  styleUrls: ['./ticketselling.component.css'],
  standalone: true,
})
export class TicketsellingComponent
  extends MasterDataListComponent<ShowtimeviewModel>
  implements OnInit
{
  //#region Font Awesome Icons
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  public faAnglesRight: IconDefinition = faAnglesRight;
  //#endregion

  public override currentPage: number = 1;
  public totalPages: number = 10;
  public showtimes: ShowtimeviewModel[] = [];
  public movies: MovieviewModel[] = [];
  selectedShowtime!: Date;


  currentView: 'ticketselling' | 'select-seat' | 'payment' = 'ticketselling';

  constructor(
    private ticketService: TicketSellingService,
    @Inject(SHOWTIME_SERVICE)
    private readonly showtimeService: IShowtimeServiceInterface,
  ) {
    super();
  }

  override ngOnInit() {
    this.getShowtimes();
    this.ticketService.getCurrentView().subscribe((view) => {
      this.currentView = view;
    });
  }
  onSelectShowtime() {
    this.ticketService.setCurrentView('select-seat');
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
        ); // Sắp xếp theo ngày tăng dần

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
