import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ServicesModule } from '../../../services/services.module';
import { ShowtimeviewModel } from '../../../models/showtime/showtimeview.model';
import { CinemaRoomViewModel } from '../../../models/room/room.model';
import { SeatViewModel } from '../../../models/seat/seat.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InvoiceViewModel } from '../../../models/invoice/invoiceview.model';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { SeatshowtimeviewModel } from '../../../models/seatshowtime/seatshowtimeview.model';
@Component({
  selector: 'app-seatshowtime',
  imports: [RouterModule, CommonModule, ServicesModule],
  templateUrl: './seatshowtime.component.html',
  styleUrl: './seatshowtime.component.css',
})
export class SeatshowtimeComponent implements OnInit {
  totalPrice: number = 0;
  showtimeId!: string;
  movieId!: string;
  movie!: MovieviewModel;
  showtime!: ShowtimeviewModel;
  room!: CinemaRoomViewModel;
  seats: SeatViewModel[] = [];
  seatShowtimeList: SeatshowtimeviewModel[] = [];
  selectedSeats: SeatViewModel[] = []; // Mảng ghế đã chọn
  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
  ) {}
  ngOnInit() {
    // Lấy showtimeId từ URL hoặc queryParams
    this.route.paramMap.subscribe((params) => {
      this.showtimeId = params.get('id') || '';
      if (this.showtimeId) {
        this.getRoomAndSeats();
      }
    });
    this.route.queryParams.subscribe((params) => {
      if (params['id']) {
        this.showtimeId = params['id'];
        this.getRoomAndSeats();
      }
    });
  }
  private getRoomAndSeats() {
    this.fetchRoom();
    this.fetchSeats();
    this.fetchSeatShowtimes();
    this.fetchShowtime();
  }

  private fetchRoom() {
    this.http
      .get<CinemaRoomViewModel>(
        `http://localhost:5063/api/v1/showtime/${this.showtimeId}/room`
      )
      .subscribe((response: CinemaRoomViewModel) => {
        this.room = response;
        console.log('Room data:', this.room);
      });
  }

  private fetchSeats() {
    this.http
      .get<SeatViewModel[]>(
        `http://localhost:5063/api/v1/showtime/${this.showtimeId}/seats`
      )
      .subscribe((response: SeatViewModel[]) => {
        this.seats = response;
        this.markBookedSeats(); // Gọi khi đã load xong seats và seatShowtimeList
      });
  }

  private fetchSeatShowtimes() {
    this.http
      .get<SeatshowtimeviewModel[]>(
        `http://localhost:5063/api/v1/showtime/${this.showtimeId}/seatshowtimes`
      )
      .subscribe((seatShowtimes: SeatshowtimeviewModel[]) => {
        this.seatShowtimeList = seatShowtimes;
        this.markBookedSeats(); // Gọi lại để đánh dấu
      });
  }
  private markBookedSeats() {
    if (!this.seats.length || !this.seatShowtimeList.length) return;

    const bookedSeatIds = new Set(
      this.seatShowtimeList.filter((s) => s.status === 2).map((s) => s.seatId)
    );

    this.seats.forEach((seat) => {
      seat.isBooked = bookedSeatIds.has(seat.id);
    });
  }
  private fetchShowtime() {
    this.http
      .get<ShowtimeviewModel>(
        `http://localhost:5063/api/v1/showtime/${this.showtimeId}`
      )
      .subscribe((response: ShowtimeviewModel) => {
        this.showtime = response;
        console.log('showtime data:', this.showtime.movieId);
        this.getMovieDetail();
      });
  }
  // Chọn hoặc bỏ chọn ghế
  toggleSeatSelection(seat: SeatViewModel) {
    if ((seat as any).isBooked) return;
    seat.isActive = !seat.isActive;
    console.log(`Seat ${seat.row}${seat.column} selected:`, seat.isActive);
    this.selectedSeats = this.getSelectedSeats();
    this.updateTotalPrice();
  }
  updateTotalPrice() {
    this.totalPrice = this.selectedSeats.reduce((total, seat) => {
      if (seat.seatType === 2) {
        return total + 70000; // Ghế VIP
      } else {
        return total + 50000; // Ghế thường
      }
    }, 0);
  }
  // Lấy danh sách ghế đã chọn để tạo invoice
  getSelectedSeats() {
    return this.seats.filter((seat) => seat.isActive === false);
  }
  confirmSeats() {
    this.selectedSeats = this.getSelectedSeats();
    if (this.selectedSeats.length === 0) {
      alert('Vui lòng chọn ít nhất một ghế!');
      return;
    }
    const seatIds = this.selectedSeats.map((seat) => seat.id); // Lấy danh sách ID ghế

    if (!this.showtimeId) {
      alert('Không tìm thấy showtimeId!');
      return;
    }

    console.log('Danh sách ghế đã chọn:', seatIds);
    console.log('Showtime ID:', this.showtimeId);

    // Gọi API đặt vé
    this.reserveSeats(this.showtimeId, seatIds).subscribe((response) => {
      console.log('Đặt vé thành công!', response);
      this.router.navigate(['/booking'], {
        state: { invoice: response },
      });
    });
  }
  reserveSeats(
    showTimeId: string,
    seatIds: string[]
  ): Observable<InvoiceViewModel> {
    return this.http.post<InvoiceViewModel>(
      `http://localhost:5063/api/v1/Seat/reserve`,
      {
        showTimeId,
        seatIds,
      }
    );
  }
  //getmoviedetail
  private getMovieDetail() {
    this.movieId = this.showtime.movieId;
    this.http
      .get<MovieviewModel>(`http://localhost:5063/api/v1/Movie/${this.movieId}`)
      .subscribe((response: MovieviewModel) => {
        this.movie = response;
        console.log('Movie data:', this.showtime.movieId);
      });
  }

  groupSeatsByRow(): { [key: string]: SeatViewModel[] } {
    return this.seats.reduce((acc, seat) => {
      if (!acc[seat.row]) {
        acc[seat.row] = [];
      }
      acc[seat.row].push(seat);
      return acc;
    }, {} as { [key: string]: SeatViewModel[] });
  }
  formatShowtimeDate(date: Date) {
    const daysOfWeek = ['Chủ nhật', 'Thứ hai', 'Thứ ba', 'Thứ tư', 'Thứ năm', 'Thứ sáu', 'Thứ bảy'];
    const dayOfWeek = daysOfWeek[new Date(date).getDay()]; // Lấy thứ trong tuần
    const day = new Date(date).getDate(); // Lấy ngày trong tháng
    const month = new Date(date).getMonth() + 1; // Lấy tháng, cộng 1 vì getMonth() trả về giá trị từ 0 - 11

    return {
      day,
      month,
      dayOfWeek
    };
  }
}
