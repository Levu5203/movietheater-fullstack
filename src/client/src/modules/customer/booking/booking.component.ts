import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { InvoiceviewModel } from '../../../models/invoice/invoiceview.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-booking',
  imports: [CommonModule, RouterModule],
  templateUrl: './booking.component.html',
  styleUrl: './booking.component.css'
})
export class BookingComponent implements OnInit{
  public invoice: InvoiceviewModel;
  constructor(private router: Router, private http: HttpClient) {
    const nav = this.router.getCurrentNavigation();
    this.invoice = nav?.extras?.state?.['invoice'];
  }
  ngOnInit(): void {
    console.log('Invoice loaded:', this.invoice);
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
          // Bạn có thể xử lý sau khi thanh toán thành công, ví dụ như chuyển hướng tới một trang khác, chuyển sang trang Homepage nếu là customer
          this.router.navigate(['/home']);
        },
        error: (error) => {
          console.error('Payment failed', error);
          // Bạn có thể xử lý lỗi như hiển thị thông báo lỗi cho người dùng
        }
      });
  }
}
