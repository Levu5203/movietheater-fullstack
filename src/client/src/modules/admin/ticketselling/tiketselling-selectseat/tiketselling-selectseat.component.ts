import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
  styleUrl: './tiketselling-selectseat.component.css',
})
export class TiketsellingSelectseatComponent {
  //#region Font Awesome Icons
  faArrowLeft: IconDefinition = faArrowLeft;
  //#endregion

  public seats: Seat[] = [
    // Row A
    { label: 'A1', isVip: false, isAvailable: true, isSelected: false },
    { label: 'A2', isVip: false, isAvailable: true, isSelected: false },
    { label: 'A3', isVip: false, isAvailable: true, isSelected: false },
    { label: 'A4', isVip: false, isAvailable: true, isSelected: false },
    { label: 'A5', isVip: false, isAvailable: true, isSelected: false },
    { label: 'A6', isVip: false, isAvailable: true, isSelected: false },
    { label: 'A7', isVip: false, isAvailable: true, isSelected: false },
    { label: 'A8', isVip: false, isAvailable: true, isSelected: false },
    // Row B
    { label: 'B1', isVip: false, isAvailable: true, isSelected: false },
    { label: 'B2', isVip: false, isAvailable: true, isSelected: false },
    { label: 'B3', isVip: false, isAvailable: true, isSelected: false },
    { label: 'B4', isVip: false, isAvailable: true, isSelected: false },
    { label: 'B5', isVip: false, isAvailable: true, isSelected: false },
    { label: 'B6', isVip: false, isAvailable: true, isSelected: false },
    { label: 'B7', isVip: false, isAvailable: true, isSelected: false },
    { label: 'B8', isVip: false, isAvailable: true, isSelected: false },
    // Row C
    { label: 'C1', isVip: false, isAvailable: true, isSelected: false },
    { label: 'C2', isVip: false, isAvailable: false, isSelected: false },
    { label: 'C3', isVip: false, isAvailable: false, isSelected: false },
    { label: 'C4', isVip: false, isAvailable: false, isSelected: false },
    { label: 'C5', isVip: false, isAvailable: true, isSelected: false },
    { label: 'C6', isVip: false, isAvailable: true, isSelected: false },
    { label: 'C7', isVip: false, isAvailable: true, isSelected: false },
    { label: 'C8', isVip: false, isAvailable: true, isSelected: false },
    // Row D
    { label: 'D1', isVip: true, isAvailable: true, isSelected: false },
    { label: 'D2', isVip: true, isAvailable: true, isSelected: false },
    { label: 'D3', isVip: true, isAvailable: false, isSelected: false },
    { label: 'D4', isVip: true, isAvailable: false, isSelected: false },
    { label: 'D5', isVip: true, isAvailable: true, isSelected: false },
    { label: 'D6', isVip: true, isAvailable: true, isSelected: false },
    { label: 'D7', isVip: true, isAvailable: true, isSelected: false },
    { label: 'D8', isVip: true, isAvailable: true, isSelected: false },
    // Row E
    { label: 'E1', isVip: true, isAvailable: true, isSelected: false },
    { label: 'E2', isVip: true, isAvailable: true, isSelected: false },
    { label: 'E3', isVip: true, isAvailable: true, isSelected: false },
    { label: 'E4', isVip: true, isAvailable: true, isSelected: false },
    { label: 'E5', isVip: true, isAvailable: true, isSelected: false },
    { label: 'E6', isVip: true, isAvailable: true, isSelected: false },
    { label: 'E7', isVip: true, isAvailable: true, isSelected: false },
    { label: 'E8', isVip: true, isAvailable: true, isSelected: false },
    // Row F
    { label: 'F1', isVip: true, isAvailable: true, isSelected: false },
    { label: 'F2', isVip: true, isAvailable: true, isSelected: false },
    { label: 'F3', isVip: true, isAvailable: true, isSelected: false },
    { label: 'F4', isVip: true, isAvailable: true, isSelected: false },
    { label: 'F5', isVip: true, isAvailable: true, isSelected: false },
    { label: 'F6', isVip: true, isAvailable: true, isSelected: false },
    { label: 'F7', isVip: true, isAvailable: true, isSelected: false },
    { label: 'F8', isVip: true, isAvailable: true, isSelected: false },
  ];

  public selectedSeats: Seat[] = [];

  toggleSeat(seat: Seat) {
    seat.isSelected = !seat.isSelected;
    if (seat.isSelected) {
      this.selectedSeats.push(seat);
    } else {
      this.selectedSeats = this.selectedSeats.filter((s) => s !== seat);
    }
  }

  get totalPrice(): number {
    return this.selectedSeats.reduce((total, seat) => {
      return total + (seat.isVip ? 120000 : 90000);
    }, 0);
  }
}
