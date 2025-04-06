import { Component, Inject, OnInit } from '@angular/core';
import { RoomDetailComponent } from '../room-detail/room-detail.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {
  faAngleLeft,
  faAngleRight,
  faInfoCircle,
  IconDefinition,
  faFilter,
  faRotateLeft,
  faAnglesLeft,
  faAnglesRight,
} from '@fortawesome/free-solid-svg-icons';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TableColumn } from '../../../../core/models/table-column.model';
import { MasterDataListComponent } from '../../../../core/components/master-data/master-data.component';
import { CinemaRoomViewModel } from '../../../../models/room/room.model';
import { IRoomService } from '../../../../services/room/room-service.interface';
import {
  MODAL_SERVICE,
  ROOM_SERVICE,
} from '../../../../constants/injection.constant';
import { ServicesModule } from '../../../../services/services.module';
import { TableComponent } from '../../../../core/components/table/table.component';
import { ModalService } from '../../../../services/modal.service';

@Component({
  selector: 'app-roommanagement',
  imports: [
    FontAwesomeModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    ServicesModule,
    TableComponent,
    RoomDetailComponent,
  ],
  templateUrl: './room-list.component.html',
  styleUrl: './room-list.component.css',
})
export class RoommanagementComponent
  extends MasterDataListComponent<CinemaRoomViewModel>
  implements OnInit
{
  //#region Font Awesome Icons
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faFilter: IconDefinition = faFilter;
  public faRotateLeft: IconDefinition = faRotateLeft;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  public faAnglesRight: IconDefinition = faAnglesRight;
  //#endregion

  public selectedRoom: CinemaRoomViewModel | null = null;
  public isDropdownOpen: boolean = false;
  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  public override columns: TableColumn[] = [
    { name: 'Room Id', value: 'id' },
    { name: 'Room Name', value: 'name' },
    { name: 'Capacity', value: 'capacity' },
  ];

  constructor(
    @Inject(ROOM_SERVICE) private readonly roomService: IRoomService,
    @Inject(MODAL_SERVICE) private readonly modalService: ModalService
  ) {
    super();
    this.isShowDetail = false;
  }

  protected override createForm(): void {
    this.searchForm = new FormGroup({
      keyword: new FormControl(''),
      minCapacity: new FormControl(null),
      maxCapacity: new FormControl(null),
    });
  }

  syncValidation(changedField: 'min' | 'max') {
    const minCtrl = this.searchForm.get('minCapacity');
    const maxCtrl = this.searchForm.get('maxCapacity');

    if (minCtrl?.value && maxCtrl?.value) {
      if (minCtrl.value > maxCtrl.value) {
        minCtrl.setErrors({ invalidRange: true });
        maxCtrl.setErrors({ invalidRange: true });
      } else {
        minCtrl.setErrors(null);
        maxCtrl.setErrors(null);
      }
    }
    maxCtrl?.updateValueAndValidity(); // Cập nhật validation maxCtrl
    minCtrl?.updateValueAndValidity(); // Cập nhật validation minCtrl
  }

  protected override searchData(): void {
    this.roomService.search(this.filter).subscribe((res) => {
      this.data = res;
    });
  }

  public viewRoomDetails(roomId: string): void {
    this.roomService.loadRoomInfo(roomId).subscribe((room) => {
      // Lấy danh sách ghế của phòng
      this.roomService.loadRoomSeats(roomId).subscribe((seats) => {
        this.selectedRoom = { ...room };
        this.selectedRoom.seats = seats; // Thêm danh sách ghế vào phòng
        this.isShowDetail = true;
      });
    });
  }
}
