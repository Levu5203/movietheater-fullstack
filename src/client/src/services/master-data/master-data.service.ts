import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMasterDataService } from './master-data.service.interface';
import { PaginatedResult } from '../../core/models/paginated-result.model';

@Injectable({
  providedIn: 'root',
})
export class MasterDataService<T> implements IMasterDataService<T> {
  protected readonly baseUrl = 'http://localhost:5063/api/v1/';

  constructor(protected http: HttpClient, protected apiUrl: String) {
    this.baseUrl += apiUrl;
  }

  getAll(): Observable<T[]> {
    return this.http.get<T[]>(this.baseUrl);
  }

  search(filter: any): Observable<PaginatedResult<T>> {
    
    return this.http.post<PaginatedResult<T>>(this.baseUrl + '/search', filter);
  }

  getById(id: number): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${id}`);
  }

  create(data: T): Observable<T> {
    return this.http.post<T>(this.baseUrl, data);
  }

  update(id: string, data: T): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${id}`, data);
  }

  delete(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${id}`);
  }
}
