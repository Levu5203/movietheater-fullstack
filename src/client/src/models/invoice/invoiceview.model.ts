import { MasterBaseModel } from "../../core/models/master-base.model";
import { TicketviewModel } from "../ticket/ticketview.model";

export class InvoiceviewModel extends MasterBaseModel{
    public userId!: string;
    public totalMoney!: number;
    public addedScore!: number;
    public tickets!: TicketviewModel[];
    public roomName!: string;
    public movieName!: string;
    public showDate!: Date;
    public startTime!: Date;

}
