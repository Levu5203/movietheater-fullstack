import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faCalendar,
  faSearch,
  faArrowLeft,
  faEdit,
  faTrash,
  faInfoCircle,
  faAngleRight,
  faAngleLeft,
  faFilter,
  faAnglesLeft,
  faAnglesRight,
  faRotateLeft,
} from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-booked-tickets',
  imports: [CommonModule, FontAwesomeModule, FormsModule],
  templateUrl: './booked-tickets.component.html',
  styleUrl: './booked-tickets.component.css',
})
export class BookedTicketsComponent implements OnInit {
  public faSearch: IconDefinition = faSearch;
  public faCalendar: IconDefinition = faCalendar;
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faRotateLeft: IconDefinition = faRotateLeft;
  public faFilter: IconDefinition = faFilter;
  public faEdit: IconDefinition = faEdit;
  public faTrash: IconDefinition = faTrash;
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  public faAnglesRight: IconDefinition = faAnglesRight;

  public isDropdownOpen: boolean = false;

  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  bookedTickets: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadBookedTickets();
  }

  loadBookedTickets() {
    const token = localStorage.getItem('accessToken');

    if (!token) {
      console.error('⚠ Không tìm thấy token! Hãy đăng nhập lại.');
      return;
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    this.http.get<any[]>('http://localhost:5063/api/Ticket/booked', { headers })
      .subscribe(
        (data) => {
          this.bookedTickets = data;
          console.log('✅ Danh sách vé đã đặt:', this.bookedTickets);
        },
        (error) => {
          console.error('❌ Lỗi khi lấy danh sách vé đã đặt:', error);
        }
      );
  }


  public Math = Math;
  currentPage: number = 1;
  itemsPerPage: number = 1;

  get paginatedTickets() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.bookedTickets.slice(startIndex, startIndex + this.itemsPerPage);
  }

  nextPage() {
    if (this.currentPage < Math.ceil(this.bookedTickets.length / this.itemsPerPage)) {
      this.currentPage++;
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }
}
