import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { PipesModule } from '../../../pipes/pipe.module';
import { CommonModule, DatePipe } from '@angular/common';
import { CustomFormatPipe } from '../../../pipes/custom-format.pipe';
import { ShowtimeviewModel } from '../../../models/showtime/showtimeview.model';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-moviedetail',
  imports: [CommonModule, CustomFormatPipe, FontAwesomeModule, RouterLink],
  templateUrl: './moviedetail.component.html',
  styleUrl: './moviedetail.component.css',
})
export class MoviedetailComponent implements OnInit {
  public faArrowLeft: IconDefinition = faArrowLeft;

  @Input() public selectedItem!: MovieviewModel | undefined | null;
  @Output() close = new EventEmitter<void>();

  selectedDate!: Date;
  showtimesBySelectedDate!: ShowtimeviewModel[];

  ngOnInit(): void {
    // Chọn suất chiếu đầu tiên mặc định
    if (
      this.selectedItem != undefined &&
      this.selectedItem?.showtimes.length > 0
    ) {
      this.selectedDate = this.selectedItem?.showtimes[0].showDate;
      this.getShowtimesByDate(this.selectedDate);
    }
  }

  // Xử lý sự kiện khi nhấn nút
  onShowtimeClick(showtime: Date) {
    this.selectedDate = showtime; // Lưu giá trị của showtime được nhấn
    this.getShowtimesByDate(this.selectedDate);
    console.log('Selected Showtime:', this.selectedDate);
  }

  getShowtimesByDate(date: Date) {
    this.showtimesBySelectedDate =
      this.selectedItem?.showtimes.filter((x) => x.showDate == date) ?? [];
  }

  selectShowtime(id: string) {
    //TODO: chuyển sang seatshowtime
  }

  onClose() {
    this.close.emit();
  }
}

