import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-ticketselling-payment',
  imports: [FontAwesomeModule, CommonModule],
  templateUrl: './ticketselling-payment.component.html',
  styleUrl: './ticketselling-payment.component.css',
})
export class TicketsellingPaymentComponent {
  public isShowMemberInfo: boolean = false;
  checkMember() {
    this.isShowMemberInfo = true;
  }
}
