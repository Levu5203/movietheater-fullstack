import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { CinemaRoomViewModel } from '../../../../models/room/room.model';
import { SeatViewModel } from '../../../../models/seat/seat.model';
import { CommonModule } from '@angular/common';
import { RoomService } from '../../../../services/room/room.service';
import { ROOM_SERVICE } from '../../../../constants/injection.constant';
import { IRoomService } from '../../../../services/room/room-service.interface';

@Component({
  selector: 'app-room-detail',
  imports: [CommonModule],
  templateUrl: './room-detail.component.html',
  styleUrls: ['./room-detail.component.css'],
})
export class RoomDetailComponent implements OnChanges {
  @Input() room: CinemaRoomViewModel | null = null;
  public clonedSeats: SeatViewModel[] = [];
  constructor(
    @Inject(ROOM_SERVICE) private readonly roomService: IRoomService
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['room'] && changes['room'].currentValue) {
      this.cloneSeats();
    }
  }

  cloneSeats(): void {
    if (this.room && this.room.seats) {
      this.clonedSeats = this.room?.seats.map((seat) => ({ ...seat }));
    }
    else{
      throw new Error('Error cloning seats')
    }
  }

  getRows(): number[] {
    return Array.from({ length: this.room?.seatRows || 0 }, (_, i) => i);
  }

  getCols(): number[] {
    return Array.from({ length: this.room?.seatColumns || 0 }, (_, i) => i);
  }

  toggleSeat(seat: SeatViewModel): void {
    seat.seatType = seat.seatType === 1 ? 2 : 1;
  }

  updateSeats(): void {
    const roomId = this.room?.id;
    if (roomId == null) {
      throw new Error('Room not found!');
    }
    this.roomService.updateSeats(roomId, this.clonedSeats).subscribe({
      next: (response) => console.log('Seats updated successfully', response),
      error: (error) => console.error('Error updating seats', error),
    });
    alert('Seats updated successfully!');
    this.onClose.emit()
  }

  @Output() public onClose: EventEmitter<string> = new EventEmitter<string>();
}
