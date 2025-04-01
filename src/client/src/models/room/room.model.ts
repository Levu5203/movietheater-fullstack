import { MasterBaseModel } from '../../core/models/master-base.model';

export class CinemaRoomViewModel extends MasterBaseModel {
  public name!: string;
  public seatRows!: number;
  public seatColumns!: number;
  public capacity!: number;
}
