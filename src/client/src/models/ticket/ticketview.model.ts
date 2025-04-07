import { MasterBaseModel } from "../../core/models/master-base.model";
import { SeatViewModel } from "../seat/seat.model";

export class TicketViewModel extends MasterBaseModel {
    public price!: number;
    public bookingDate!: Date;
    public status!: string;
    public seat?: SeatViewModel;
    public roomName!: string;
    public movieName!: string;
    public invoiceId!: string;
    public promotionId?: string;
    // public promotion?: PromotionViewModel;
    public showDate!: Date;
    public startTime!: string;
  }
  