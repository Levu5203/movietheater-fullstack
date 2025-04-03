import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
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
  selector: 'app-score-history',
  imports: [CommonModule, FontAwesomeModule, FormsModule, ReactiveFormsModule],
  templateUrl: './score-history.component.html',
  styleUrl: './score-history.component.css',
})
export class ScoreHistoryComponent implements OnInit {
  public faCalendar: IconDefinition = faCalendar;
  public faSearch: IconDefinition = faSearch;
  
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

  
  //Call API
  historyScore: any[] = [];
  
  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.filterForm = this.fb.group({
        fromDate: [''],
        toDate: [''],
        status: ['']
      });
    }

    ngOnInit(): void {
      this.loadHistoryScore();
    }

    private loadHistoryScore() {
      const token = localStorage.getItem('accessToken');
    
      if (!token) {
        console.error('Không tìm thấy token! Hãy đăng nhập lại.');
        return;
      }
    
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });
    
      this.http.get<any[]>('http://localhost:5063/api/history-score', { headers })
        .subscribe(
          (data) => {
            this.historyScore = data.map(score => ({
              ...score,
              status: this.mapStatusToString(score.status)
            }));
            console.log('Danh sách vé đã đặt:', this.historyScore);
          },
          (error) => {
            console.error('Lỗi khi lấy danh sách vé đã đặt:', error);
          }
        );
    }

    mapStatusToString(status: number): string {
      switch (status) {
        case 1:
          return 'ADDED';
        case 2:
          return 'USED';
        default:
          return 'Unknown';
      }
    }

    //Pagination 
  public Math = Math;
  currentPage: number = 1;
  itemsPerPage: number = 3;

  nextPage() {
    if (this.currentPage < Math.ceil(this.historyScore.length / this.itemsPerPage)) {
      this.currentPage++;
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  //Filter and Search
  public filterForm: FormGroup;

  get filteredScore() {
    const filterData = this.filterForm.value;
    const fromDate = filterData.fromDate ? new Date(filterData.fromDate) : null;
    const toDate = filterData.toDate ? new Date(filterData.toDate) : null;
    const status = filterData.status ? filterData.status : '';
  
    return this.historyScore.filter(score => {
      const bookingDate = new Date(score.bookingDate); // Chuyển đổi ngày đặt vé sang kiểu Date
  
      // Kiểm tra ngày bắt đầu
      if (fromDate && bookingDate < fromDate) {
        return false;
      }
  
      // Kiểm tra ngày kết thúc
      if (toDate && bookingDate > toDate) {
        return false;
      }
  
      // Kiểm tra trạng thái
      if (status && score.status !== status) {
        return false;
      }
  
      return true;
    });
  }

  applyFilter() {
    const filterData = this.filterForm.value;
    console.log('Filter Data:', filterData);
    console.log(this.filteredScore)
  }

  searchKeyword: string = '';


  get searchedAndFilteredScore() {
    if (!this.searchKeyword.trim()) {
      return this.filteredScore.slice(
        (this.currentPage - 1) * this.itemsPerPage,
        this.currentPage * this.itemsPerPage
      );
    }
  
    const filtered = this.filteredScore.filter(ticket =>
      ticket.movieName.toLowerCase().includes(this.searchKeyword.toLowerCase())
    );
  
    return filtered.slice(
      (this.currentPage - 1) * this.itemsPerPage,
      this.currentPage * this.itemsPerPage
    );
  }
  
  //Reset filter
  public resetFilter() {
    this.filterForm.reset({
      fromDate: '',
      toDate: '',
      status: ''
    });
  }
}
