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

}
