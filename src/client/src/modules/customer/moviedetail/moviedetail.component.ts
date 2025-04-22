import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MovieviewModel } from '../../../models/movie/movieview.model';
import { CommonModule } from '@angular/common';
import { CustomFormatPipe } from '../../../pipes/custom-format.pipe';
import { ShowtimeviewModel } from '../../../models/showtime/showtimeview.model';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-moviedetail',
  imports: [CommonModule, CustomFormatPipe, FontAwesomeModule, RouterModule],
  templateUrl: './moviedetail.component.html',
  styleUrl: './moviedetail.component.css',
})
export class MoviedetailComponent implements OnInit {
  public faArrowLeft: IconDefinition = faArrowLeft;

  @Input() public selectedItem!: MovieviewModel | undefined | null;
  @Output() close = new EventEmitter<void>();

  showDates!: Date[];
  selectedDate!: Date;
  showtimesBySelectedDate!: ShowtimeviewModel[];

  ngOnInit(): void {
    // Chọn suất chiếu đầu tiên mặc định
    if (
      this.selectedItem != undefined &&
      this.selectedItem?.showtimes.length > 0
    ) {
      this.getShowDates();
      this.getShowtimesByDate(this.selectedDate);
    }
  }

  // Xử lý sự kiện khi nhấn nút
  onShowDateClick(showDate: Date): void {
    this.selectedDate = showDate; // Lưu giá trị của showtime được nhấn
    this.getShowtimesByDate(this.selectedDate);
    console.log('Selected Showtime:', this.selectedDate);
  }

  getShowDates(): void {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const oneWeekLater = new Date();
    oneWeekLater.setDate(today.getDate() + 6); // Lấy 1 tuần sau
    oneWeekLater.setHours(0, 0, 0, 0);

    const dates =
      this.selectedItem?.showtimes
        .map((x) => x.showDate)
        .filter((date) => {
          const d = new Date(date);
          d.setHours(0, 0, 0, 0);
          return d >= today && d <= oneWeekLater;
        }) ?? [];

    const uniqueDates = [...new Set(dates)];
    uniqueDates.sort((a, b) => new Date(a).getTime() - new Date(b).getTime());

    // Kiểm tra nếu hôm nay không còn suất chiếu nào hợp lệ thì loại bỏ
    const hasNoValidTodayShowtimes = this.selectedItem?.showtimes.some((x) => {
      const now = new Date();
      const dateTime = new Date(`${x.showDate}T${x.startTime}`);
      return dateTime.getDate() == now.getDate() && dateTime < now;
    });

    if (hasNoValidTodayShowtimes) {
      uniqueDates.shift();
    } // Loại bỏ ngày hôm nay nếu không có suất chiếu nào hợp lệ

    this.showDates = uniqueDates;
    // Chọn suất chiếu đầu tiên mặc định
    if (this.showDates.length > 0) {
      this.selectedDate = this.showDates[0];
    }
  }

  getShowtimesByDate(date: Date): void {
    this.showtimesBySelectedDate =
      this.selectedItem?.showtimes
        .filter((x) => x.showDate == date)
        .filter((x) => {
          return new Date(`${x.showDate}T${x.startTime}`) > new Date();
        })
        .sort((a, b) => {
          const d1 = new Date(`${a.showDate}T${a.startTime}`);
          const d2 = new Date(`${b.showDate}T${b.startTime}`);
          return d1.getTime() - d2.getTime();
        }) ?? [];
  }

  onClose() {
    this.close.emit();
  }
}
