import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { faAngleLeft, faAngleRight, faAnglesLeft, faAnglesRight, faArrowLeft, faEdit, faFilter, faInfoCircle, faRotateLeft, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { PromotionService } from '../../../services/promotion/promotion-service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-promotionmanagement',
  imports: [CommonModule, FontAwesomeModule, FormsModule],
  templateUrl: './promotionmanagement.component.html',
  styleUrl: './promotionmanagement.component.css'
})
export class PromotionmanagementComponent implements OnInit {
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

  constructor(private promotionService: PromotionService, private router: Router) {
    
  }
  
  // Call
  promotions: any[] = [];
  
  ngOnInit(): void {
    this.loadPromotions();
  }

  loadPromotions() {
    this.promotionService.getPromotions().subscribe(data => {
      this.promotions = data;
      this.totalPages = Math.ceil(this.promotions.length / this.itemsPerPage);
      this.currentPage = 1; // Đặt lại trang về 1 khi tải dữ liệu mới
      this.updatePagination();
    });
  }

  // update
  updatePromotion(id: string) {
    this.router.navigate(['admin/updatepromotion', id]);
  }

  //delete
  deletePromotion(id: string): void {
    if (confirm('Bạn có chắc chắn muốn xóa promotion này?')) {
      this.promotionService.deletePromotion(id).subscribe(() => {
        alert('Promotion đã được xóa thành công!');
        this.loadPromotions(); // Cập nhật danh sách sau khi xóa
      });
    }
  }

  //paging
  displayedPromotions: any[] = [];

  currentPage: number = 1;
  itemsPerPage: number = 3;
  totalPages: number = 1;

  updatePagination() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.displayedPromotions = this.promotions.slice(startIndex, endIndex);
  }

  goToPage(page: number) {
    // Validate page value - must be a number, not NaN
    if (isNaN(page) || page === null) {
      // Reset to current valid page if input is invalid
      this.currentPage = Math.min(Math.max(1, this.currentPage), this.totalPages);
      return;
    }
    
    // Ensure page is within valid boundaries
    const validatedPage = Math.min(Math.max(1, Math.round(page)), this.totalPages);
    
    // Only update if it's a valid page
    if (validatedPage >= 1 && validatedPage <= this.totalPages) {
      this.currentPage = validatedPage;
      this.updatePagination();
    } else {
      // If invalid page, reset to current valid page
      this.currentPage = Math.min(Math.max(1, this.currentPage), this.totalPages);
    }
  }

  getRowIndex(localIndex: number): number {
    // Use a validated currentPage to calculate index
    const validPage = Math.min(Math.max(1, this.currentPage), this.totalPages);
    return (validPage - 1) * this.itemsPerPage + localIndex + 1;
  }
  
  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePagination();
    }
  }
  
  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagination();
    }
  }
  
  firstPage() {
    this.currentPage = 1;
    this.updatePagination();
  }
  
  lastPage() {
    this.currentPage = this.totalPages;
    this.updatePagination();
  }
  
  //search
  // search
searchKeyword: string = '';

updateSearchResults() {
  if (!this.searchKeyword.trim()) {
    this.updatePagination(); // Nếu ô tìm kiếm rỗng, hiển thị danh sách phân trang bình thường
    return;
  }

  const keyword = this.searchKeyword.toLowerCase();

  // Lọc các promotion theo tiêu chí
  const filtered = this.promotions.filter(promotion =>
    promotion.promotionTitle.toLowerCase().includes(keyword) ||
    promotion.description.toLowerCase().includes(keyword) ||
    promotion.discount.toString().includes(keyword) // Tìm kiếm discount bằng số
  );

  this.displayedPromotions = filtered.slice(0, this.itemsPerPage);
  this.totalPages = Math.ceil(filtered.length / this.itemsPerPage);
  this.currentPage = 1; // Đưa về trang đầu tiên khi tìm kiếm
}

}
