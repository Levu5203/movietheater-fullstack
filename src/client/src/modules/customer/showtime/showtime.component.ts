import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SeatshowtimeComponent } from '../seatshowtime/seatshowtime.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-showtime',
  imports: [CommonModule, RouterModule],
  templateUrl: './showtime.component.html',
  styleUrl: './showtime.component.css'
})
export class ShowtimeComponent {

}
