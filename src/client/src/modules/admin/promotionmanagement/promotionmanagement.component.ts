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
    this.updatePagination();
  }
  
  // Call
  promotions: any[] = [];
  
  ngOnInit(): void {
    this.loadPromotions();
  }

  loadPromotions() {
    this.promotionService.getPromotions().subscribe(data => {
      this.promotions = data;
    });
  }
  
  
  //Paging
  currentPage: number = 1;
  itemsPerPage: number = 7;
  paginatedPromotions: any[] = [];


  updatePagination() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedPromotions = this.promotions.slice(startIndex, endIndex);
  }

  goToPage(page: number) {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePagination();
    }
  }

  get totalPages(): number {
    return Math.ceil(this.promotions.length / this.itemsPerPage);
  }

  // update
  updatePromotion(id: string) {
    this.router.navigate(['admin/updatepromotion', id]);
  }
}
