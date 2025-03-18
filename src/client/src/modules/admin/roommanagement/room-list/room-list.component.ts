import { Component } from '@angular/core';
import { RoomDetailComponent } from '../room-detail/room-detail.component';

@Component({
  selector: 'app-roommanagement',
  imports: [RoomDetailComponent],
  templateUrl: './room-list.component.html',
  styleUrl: './room-list.component.css'
})
export class RoommanagementComponent {

}
