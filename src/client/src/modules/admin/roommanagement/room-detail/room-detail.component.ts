import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

interface Seat {
  label: string;
  isVip: boolean;
}

@Component({
  selector: 'app-room-detail',
  imports: [CommonModule],
  templateUrl: './room-detail.component.html',
  styleUrl: './room-detail.component.css',
})
export class RoomDetailComponent {
  public seats: Seat[] = [
    // Row A
    { label: 'A1', isVip: false }, { label: 'A2', isVip: false }, { label: 'A3', isVip: false }, { label: 'A4', isVip: false },
    { label: 'A5', isVip: false }, { label: 'A6', isVip: false }, { label: 'A7', isVip: false }, { label: 'A8', isVip: false },
    // Row B
    { label: 'B1', isVip: false }, { label: 'B2', isVip: false }, { label: 'B3', isVip: false }, { label: 'B4', isVip: false },
    { label: 'B5', isVip: false }, { label: 'B6', isVip: false }, { label: 'B7', isVip: false }, { label: 'B8', isVip: false },
    // Row C
    { label: 'C1', isVip: false }, { label: 'C2', isVip: false }, { label: 'C3', isVip: false }, { label: 'C4', isVip: false },
    { label: 'C5', isVip: false }, { label: 'C6', isVip: false }, { label: 'C7', isVip: false }, { label: 'C8', isVip: false },
    // Row D
    { label: 'D1', isVip: true }, { label: 'D2', isVip: true }, { label: 'D3', isVip: true }, { label: 'D4', isVip: true },
    { label: 'D5', isVip: true }, { label: 'D6', isVip: true }, { label: 'D7', isVip: true }, { label: 'D8', isVip: true },
    // Row E
    { label: 'E1', isVip: true }, { label: 'E2', isVip: true }, { label: 'E3', isVip: true }, { label: 'E4', isVip: true },
    { label: 'E5', isVip: true }, { label: 'E6', isVip: true }, { label: 'E7', isVip: true }, { label: 'E8', isVip: true },
    // Row F
    { label: 'F1', isVip: true }, { label: 'F2', isVip: true }, { label: 'F3', isVip: true }, { label: 'F4', isVip: true },
    { label: 'F5', isVip: true }, { label: 'F6', isVip: true }, { label: 'F7', isVip: true }, { label: 'F8', isVip: true }
  ];

  public toggleSeat(seat: Seat): void {
    seat.isVip = !seat.isVip;
  }
}
