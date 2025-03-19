import { Component } from '@angular/core';
import { RoomDetailComponent } from '../room-detail/room-detail.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {
  faAngleLeft,
  faAngleRight,
  faArrowLeft,
  faInfoCircle,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-roommanagement',
  imports: [RoomDetailComponent, FontAwesomeModule],
  templateUrl: './room-list.component.html',
  styleUrl: './room-list.component.css',
})
export class RoommanagementComponent {
  //#region Font Awesome Icons
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
}
