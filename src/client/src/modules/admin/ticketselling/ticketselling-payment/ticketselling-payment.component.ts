import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { UserModel } from '../../../../models/user/user.model';
import { InvoiceViewModel } from '../../../../models/invoice/invoiceview.model';
import { Router, RouterLink } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CustomFormatPipe } from '../../../../pipes/custom-format.pipe';

@Component({
  selector: 'app-ticketselling-payment',
  imports: [CommonModule, FormsModule, RouterLink, CustomFormatPipe],
  templateUrl: './ticketselling-payment.component.html',
  styleUrls: ['./ticketselling-payment.component.css'],
  standalone: true,
})
export class TicketsellingPaymentComponent implements OnInit {
  selectedSeats: any[] = [];
  totalPrice: number = 0;
  customer?: UserModel;
  isShowMemberInfo: boolean = false;
  showTimeId!: string;
  phoneNumber: string = '';
  public invoice: InvoiceViewModel;
  public invoiceById!: InvoiceViewModel;
  constructor(private router: Router, private http: HttpClient) {
    const nav = this.router.getCurrentNavigation();
    this.invoice = nav?.extras?.state?.['invoice'];
    // this.getInvoiceById(this.invoice.id);
  }
  ngOnInit(): void {
    if (this.invoice?.id) {
      this.getInvoiceById(this.invoice.id);
    }
  }
  checkMember() {
    const token = localStorage.getItem('accessToken');
    if (!token) {
      alert('You need to log in!');
      return;
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    this.http
      .get<UserModel>(
        `http://localhost:5063/api/v1/Customers/phone/${this.phoneNumber}`,
        { headers }
      )
      .subscribe((response: UserModel) => {
        this.customer = response;
        console.log('Customer', this.customer);
        if(this.customer !== null) {
          this.isShowMemberInfo = true;
        }
      });
  }

  // Hàm gọi API để lấy thông tin hóa đơn theo ID
  getInvoiceById(invoiceId: string) {
    this.http
      .get<InvoiceViewModel>(
        `http://localhost:5063/api/v1/Invoice/${invoiceId}`
      )
      .subscribe((response: InvoiceViewModel) => {
        this.invoiceById = response;
        this.showTimeId = response.showTimeId;
        // console.log('InvoiceId data:', this.invoiceById);
        console.log('ShowtimeId data:', this.showTimeId);
      });
  }
  payInvoice() {
    const paymentCommand = {
      invoiceId: this.invoice.id,
      phoneNumber: this.customer?.phoneNumber,
    };
    const token = localStorage.getItem('accessToken');
    if (!token) {
      alert('You need to log in!');
      return;
    }
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    // Gọi API payment
    this.http
      .post(
        'http://localhost:5063/api/v1/Invoice/employee/payment',
        paymentCommand
      )
      .subscribe({
        next: (response) => {
          console.log('Payment successful', response);

          this.router.navigate(['/admin/ticketselling']);
        },
        error: (error) => {
          console.error('Payment failed', error);
          // Bạn có thể xử lý lỗi như hiển thị thông báo lỗi cho người dùng
        },
      });
  }
}
