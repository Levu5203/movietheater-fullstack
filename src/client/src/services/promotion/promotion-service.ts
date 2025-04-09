import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Promotion {
  id: string;
  promotionTitle: string;
  description: string;
  discount: number;
  startDate: string;
  endDate: string;
  image: string;
}

@Injectable({
  providedIn: 'root'
})
export class PromotionService {
  private apiUrl = 'http://localhost:5063/api/Promotion';

  constructor(private http: HttpClient) {}

  getPromotions(): Observable<Promotion[]> {
    return this.http.get<Promotion[]>(this.apiUrl);
  }

  createPromotion(formData: FormData): Observable<any> {
    return this.http.post<any>(this.apiUrl, formData);
  }

  getPromotionById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
  
  updatePromotion(id: string, promotion: FormData): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, promotion);
  }

  deletePromotion(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
