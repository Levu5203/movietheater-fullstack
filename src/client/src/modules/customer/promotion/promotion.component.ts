import { Component, OnInit } from '@angular/core';
import { PromotionModel } from '../../../models/promotion/promotion.model';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-promotion',
  imports: [CommonModule, RouterLink],
  templateUrl: './promotion.component.html',
  styleUrl: './promotion.component.css'
})
export class PromotionComponent implements OnInit {
  public promotions: PromotionModel[] = [];
  public currentPage: number = 1;
  public itemsPerPage: number = 8;
  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.fetchPromotions();
  }
  private fetchPromotions() {
    this.http
      .get<PromotionModel[]>(`http://localhost:5063/api/Promotion`)
      .subscribe((response: PromotionModel[]) => {
        this.promotions = response;
        console.log('Promotion data:', this.promotions);
      });
  }
  get paginatedPromotions(): PromotionModel[] {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    return this.promotions.slice(start, start + this.itemsPerPage);
  }

  get totalPages(): number {
    return Math.ceil(this.promotions.length / this.itemsPerPage);
  }

  goNext() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  goBack() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }
}
