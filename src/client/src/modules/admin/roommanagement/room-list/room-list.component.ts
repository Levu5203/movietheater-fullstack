import { Component, OnInit } from '@angular/core';
import { RoomDetailComponent } from '../room-detail/room-detail.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {
  faAngleLeft,
  faAngleRight,
  faArrowLeft,
  faInfoCircle,
  IconDefinition,
  faFilter,
  faRotateLeft,
  faAnglesLeft,
  faAnglesRight,
} from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RoomManagementService } from '../roommanagement.service';

@Component({
  selector: 'app-roommanagement',
  imports: [RoomDetailComponent, FontAwesomeModule, FormsModule, CommonModule],
  templateUrl: './room-list.component.html',
  styleUrl: './room-list.component.css',
})
export class RoommanagementComponent implements OnInit {
  //#region Font Awesome Icons
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faFilter: IconDefinition = faFilter;
  public faRotateLeft: IconDefinition = faRotateLeft;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  public faAnglesRight: IconDefinition = faAnglesRight;
  //#endregion

  currentView: 'list' | 'detail' = 'list';

  constructor(public roomService: RoomManagementService) { }

  ngOnInit(): void {
    this.roomService.currentView$.subscribe((view) => {
      this.currentView = view;
    });
  }

  public isDropdownOpen: boolean = false;
  public currentPage: number = 1;
  public totalPages: number = 10;

  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  onViewDetail(room: any): void {
    this.roomService.goToDetail(room);
  }
}
