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
import { Router, RouterModule } from '@angular/router';

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
      this.selectedDate = this.selectedItem?.showtimes[0].showDate;
      this.getShowtimesByDate(this.selectedDate);
      this.getShowDates();
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
    oneWeekLater.setDate(today.getDate() + 7);
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

    this.showDates = uniqueDates;
    // Auto-select the earliest available date
    if (this.showDates.length > 0) {
      this.selectedDate = this.showDates[0];
    }
  }

  getShowtimesByDate(date: Date): void {
    this.showtimesBySelectedDate =
      this.selectedItem?.showtimes.filter((x) => x.showDate == date) ?? [];
  }

  onClose() {
    this.close.emit();
  }
}
