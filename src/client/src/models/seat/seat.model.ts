import { MasterBaseModel } from '../../core/models/master-base.model';

export class SeatViewModel extends MasterBaseModel {
  public row!: string;
  public column!: number;
  public seatType!: number;
  public seatStatus!: number;
  public isBooked!: boolean;
}
