import { MasterBaseModel } from '../../core/models/master-base.model';
import { SeatViewModel } from '../seat/seat.model';

export class CinemaRoomViewModel extends MasterBaseModel {
  public name!: string;
  public seatRows!: number;
  public seatColumns!: number;
  public capacity!: number;

  public seats!: SeatViewModel[]
}
