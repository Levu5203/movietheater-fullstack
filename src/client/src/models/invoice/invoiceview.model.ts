import { MasterBaseModel } from "../../core/models/master-base.model";
import { TicketViewModel } from "../ticket/ticketview.model";

export class InvoiceViewModel extends MasterBaseModel{
    public userId!: string;
    public userFullName!: string;
    public userEmail!: string;
    public userPhoneNumber!: string;
    public totalMoney!: number;
    public addedScore!: number;
    public tickets!: TicketViewModel[];
    public roomName!: string;
    public movieName!: string;
    public showDate!: Date;
    public startTime!: string;
    public ticketIssued!: boolean;
    public showtimeId!: string;
}
