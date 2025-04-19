import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { InvoiceViewModel } from '../../../models/invoice/invoiceview.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Location } from '@angular/common';
import { SeatViewModel } from '../../../models/seat/seat.model';

@Component({
  selector: 'app-booking',
  imports: [CommonModule, RouterModule],
  templateUrl: './booking.component.html',
  styleUrl: './booking.component.css'
})
export class BookingComponent implements OnInit{
  // private modalService!: ModalService,
  public showtimeId: string = ''; 
  public invoice: InvoiceViewModel;
  public invoiceById!: InvoiceViewModel;
  constructor(private router: Router, private http: HttpClient) {
    const nav = this.router.getCurrentNavigation();
    this.invoice = nav?.extras?.state?.['invoice'];
    this.getInvoiceById(this.invoice.id);
  }
  ngOnInit(): void {
    console.log('Invoice loaded:', this.invoice); 
  }
  goBackToSeatShowtime(showtimeId: string): void {
    // Điều hướng về trang seatshowtime và truyền query parameter 'id'
    this.router.navigate(['/seatshowtime'], {
      queryParams: { id: showtimeId }  // Truyền 'id' trong query parameters
    });
  }
  // Hàm gọi API để lấy thông tin hóa đơn theo ID
  getInvoiceById(invoiceId: string){
    this.http
          .get<InvoiceViewModel>(
            `http://localhost:5063/api/v1/Invoice/${invoiceId}`
          )
          .subscribe((response: InvoiceViewModel) => {
            this.invoiceById = response;
            console.log('showtime data:', this.invoiceById);
          });
  }
  payInvoice() {
    const paymentCommand = {
      invoiceId: this.invoice.id // Giả sử bạn có trường id trong InvoiceviewModel
    };
    const token = localStorage.getItem('accessToken');
    if (!token) {
      alert('You need to log in!');
      return;
    }
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    // Gọi API payment
    this.http.post('http://localhost:5063/api/v1/Invoice/payment', paymentCommand)
      .subscribe({
        next: (response) => {
          console.log('Payment successful', response);
          
          this.router.navigate(['/home']);
        },
        error: (error) => {
          console.error('Payment failed', error);
          alert('Seat already booked! Please choose another seat.');
        }
      });
  }
}
