import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketSellingService } from '../ticketselling.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft, IconDefinition } from '@fortawesome/free-solid-svg-icons';

interface Seat {
  label: string;
  isVip: boolean;
  isAvailable: boolean;
  isSelected: boolean;
}

@Component({
  selector: 'app-tiketselling-selectseat',
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './tiketselling-selectseat.component.html',
  styleUrls: ['./tiketselling-selectseat.component.css'],
  standalone: true,
})
export class TiketsellingSelectseatComponent implements OnInit {
  faArrowLeft: IconDefinition = faArrowLeft;

  seats: any[] = [];
  selectedSeats: any[] = [];
  totalPrice: number = 0;

  constructor(private ticketService: TicketSellingService) {
    for (let i = 0; i < 48; i++) {
      this.seats.push({
        label: String.fromCharCode(65 + Math.floor(i / 8)) + (i % 8 + 1),
        isAvailable: true,
        isSelected: false,
        isVip: i >= 24
      });
    }
  }

  ngOnInit() {
    this.ticketService.getSelectedSeats().subscribe(seats => {
      this.selectedSeats = seats;
    });

    this.ticketService.getTotalPrice().subscribe(price => {
      this.totalPrice = price;
    });
  }

  toggleSeat(seat: any) {
    if (seat.isAvailable) {
      seat.isSelected = !seat.isSelected;
      if (seat.isSelected) {
        this.selectedSeats.push(seat);
      } else {
        this.selectedSeats = this.selectedSeats.filter(s => s !== seat);
      }
      this.calculateTotalPrice();
      this.ticketService.setSelectedSeats(this.selectedSeats);
    }
  }

  calculateTotalPrice() {
    this.totalPrice = this.selectedSeats.reduce((total, seat) => {
      return total + (seat.isVip ? 100000 : 80000);
    }, 0);
    this.ticketService.setTotalPrice(this.totalPrice);
  }

  onPayment() {
    this.ticketService.setCurrentView('payment');
  }

  onCancel() {
    this.ticketService.setCurrentView('ticketselling');
  }
}
