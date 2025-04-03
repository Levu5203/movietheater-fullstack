import { MasterBaseModel } from '../../core/models/master-base.model';

export class SeatViewModel extends MasterBaseModel {
  public row!: number;
  public column!: number;
  public seatType!: number;
}
