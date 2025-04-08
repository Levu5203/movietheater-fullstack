import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MasterDataService } from '../master-data/master-data.service';
import { IBookingService } from './booking-service.interface';
import { InvoiceViewModel } from '../../models/invoice/invoiceview.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookingService
  extends MasterDataService<InvoiceViewModel>
  implements IBookingService
{
  constructor(protected override http: HttpClient) {
    super(http, 'bookings');
  }

  override update(id: string): Observable<InvoiceViewModel> {
    const url = `${this.baseUrl}/${id}`;
    return this.http.put<InvoiceViewModel>(url, null);
  }
}
