import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-promotionmanagement',
  imports: [CommonModule],
  templateUrl: './promotionmanagement.component.html',
  styleUrl: './promotionmanagement.component.css'
})
export class PromotionmanagementComponent {
  public promotions = [
    {
      "id": 1,
      "promotionId": "PROMO001",
      "title": "Summer Sale",
      "startTime": "2025-06-01T00:00:00",
      "endTime": "2025-06-30T23:59:59",
      "discountLevel": "20%",
      "detail": "Enjoy a 20% discount on all summer items."
    },
    {
      "id": 2,
      "promotionId": "PROMO002",
      "title": "Back to School",
      "startTime": "2025-08-01T00:00:00",
      "endTime": "2025-08-15T23:59:59",
      "discountLevel": "15%",
      "detail": "Special discount for students & teachers."
    },
    {
      "id": 3,
      "promotionId": "PROMO003",
      "title": "Black Friday Mega Sale",
      "startTime": "2025-11-29T00:00:00",
      "endTime": "2025-11-29T23:59:59",
      "discountLevel": "50%",
      "detail": "Huge discounts on all categories for Black Friday."
    },
    {
      "id": 4,
      "promotionId": "PROMO004",
      "title": "Cyber Monday Deals",
      "startTime": "2025-12-02T00:00:00",
      "endTime": "2025-12-02T23:59:59",
      "discountLevel": "40%",
      "detail": "Limited-time tech deals on Cyber Monday."
    },
    {
      "id": 5,
      "promotionId": "PROMO005",
      "title": "Christmas Special",
      "startTime": "2025-12-20T00:00:00",
      "endTime": "2025-12-25T23:59:59",
      "discountLevel": "25%",
      "detail": "Holiday gifts at discounted prices!"
    },
    {
      "id": 6,
      "promotionId": "PROMO006",
      "title": "New Year’s Celebration",
      "startTime": "2025-12-31T00:00:00",
      "endTime": "2026-01-01T23:59:59",
      "discountLevel": "30%",
      "detail": "Ring in the new year with great savings!"
    },
    {
      "id": 7,
      "promotionId": "PROMO007",
      "title": "Valentine’s Day Sale",
      "startTime": "2026-02-10T00:00:00",
      "endTime": "2026-02-14T23:59:59",
      "discountLevel": "15%",
      "detail": "Get romantic gifts at special prices."
    },
    {
      "id": 8,
      "promotionId": "PROMO008",
      "title": "Spring Clearance",
      "startTime": "2026-03-15T00:00:00",
      "endTime": "2026-03-31T23:59:59",
      "discountLevel": "35%",
      "detail": "Clearance sale for spring collection."
    },
    {
      "id": 9,
      "promotionId": "PROMO009",
      "title": "Easter Savings",
      "startTime": "2026-04-10T00:00:00",
      "endTime": "2026-04-14T23:59:59",
      "discountLevel": "20%",
      "detail": "Enjoy Easter with exclusive deals."
    },
    {
      "id": 10,
      "promotionId": "PROMO010",
      "title": "Mother’s Day Special",
      "startTime": "2026-05-05T00:00:00",
      "endTime": "2026-05-12T23:59:59",
      "discountLevel": "25%",
      "detail": "Show your love with discounted gifts."
    },
    {
      "id": 11,
      "promotionId": "PROMO011",
      "title": "Father’s Day Deals",
      "startTime": "2026-06-10T00:00:00",
      "endTime": "2026-06-17T23:59:59",
      "discountLevel": "20%",
      "detail": "Special offers for dads."
    },
    {
      "id": 12,
      "promotionId": "PROMO012",
      "title": "Independence Day Sale",
      "startTime": "2026-07-01T00:00:00",
      "endTime": "2026-07-04T23:59:59",
      "discountLevel": "30%",
      "detail": "Celebrate freedom with big savings!"
    },
    {
      "id": 13,
      "promotionId": "PROMO013",
      "title": "Labor Day Discount",
      "startTime": "2026-09-01T00:00:00",
      "endTime": "2026-09-05T23:59:59",
      "discountLevel": "20%",
      "detail": "Exclusive deals for the hardworking people!"
    },
    {
      "id": 14,
      "promotionId": "PROMO014",
      "title": "Halloween Special",
      "startTime": "2026-10-25T00:00:00",
      "endTime": "2026-10-31T23:59:59",
      "discountLevel": "15%",
      "detail": "Spooky deals for Halloween."
    },
    {
      "id": 15,
      "promotionId": "PROMO015",
      "title": "Thanksgiving Treat",
      "startTime": "2026-11-20T00:00:00",
      "endTime": "2026-11-25T23:59:59",
      "discountLevel": "10%",
      "detail": "Celebrate Thanksgiving with exclusive discounts."
    },
    {
      "id": 16,
      "promotionId": "PROMO016",
      "title": "Tech Week Sale",
      "startTime": "2026-08-20T00:00:00",
      "endTime": "2026-08-27T23:59:59",
      "discountLevel": "35%",
      "detail": "Best deals on tech gadgets."
    },
    {
      "id": 17,
      "promotionId": "PROMO017",
      "title": "Fashion Week",
      "startTime": "2026-09-15T00:00:00",
      "endTime": "2026-09-20T23:59:59",
      "discountLevel": "25%",
      "detail": "Trendy outfits at discounted prices."
    },
    {
      "id": 18,
      "promotionId": "PROMO018",
      "title": "Travel Season Discount",
      "startTime": "2026-10-01T00:00:00",
      "endTime": "2026-10-10T23:59:59",
      "discountLevel": "20%",
      "detail": "Best travel deals available now!"
    },
    {
      "id": 19,
      "promotionId": "PROMO019",
      "title": "Grocery Discount Week",
      "startTime": "2026-07-10T00:00:00",
      "endTime": "2026-07-17T23:59:59",
      "discountLevel": "10%",
      "detail": "Big discounts on daily essentials."
    },
    {
      "id": 20,
      "promotionId": "PROMO020",
      "title": "Exclusive VIP Sale",
      "startTime": "2026-12-10T00:00:00",
      "endTime": "2026-12-15T23:59:59",
      "discountLevel": "50%",
      "detail": "Members-only discounts on luxury brands."
    }
  ]
  
}
