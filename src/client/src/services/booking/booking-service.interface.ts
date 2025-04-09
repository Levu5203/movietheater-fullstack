import { IMasterDataService } from '../master-data/master-data.service.interface';
import { InvoiceViewModel } from '../../models/invoice/invoiceview.model';
import { Observable } from 'rxjs';

export interface IBookingService extends IMasterDataService<InvoiceViewModel> {
    update(id: string): Observable<InvoiceViewModel>
}
