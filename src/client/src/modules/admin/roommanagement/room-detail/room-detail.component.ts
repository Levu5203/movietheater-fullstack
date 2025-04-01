import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

interface Seat {
  label: string;
  isVip: boolean;
}

@Component({
  selector: 'app-room-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './room-detail.component.html',
  styleUrls: ['./room-detail.component.css'],
})
export class RoomDetailComponent {
}