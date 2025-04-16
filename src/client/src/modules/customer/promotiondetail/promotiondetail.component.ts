import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PromotionModel } from '../../../models/promotion/promotion.model';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-promotiondetail',
  imports: [CommonModule],
  templateUrl: './promotiondetail.component.html',
  styleUrl: './promotiondetail.component.css'
})
export class PromotiondetailComponent implements OnInit {
  public promotionId!: string;
  public promotion!: PromotionModel;
  constructor(private http: HttpClient, private route: ActivatedRoute) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.promotionId = params.get('id') || '';
      if (this.promotionId) {
        this.fetchPromotion();
      }
    });
    this.route.queryParams.subscribe((params) => {
      if (params['id']) {
        this.promotionId = params['id'];
        this.fetchPromotion();
      }
    });
  }
  private fetchPromotion() {
      this.http
        .get<PromotionModel>(`http://localhost:5063/api/Promotion/${this.promotionId}`)
        .subscribe((response: PromotionModel) => {
          this.promotion = response;
          console.log('Promotion data:', this.promotion);
        });
    }

}
