import { MasterBaseModel } from "../../core/models/master-base.model";

export class ShowtimeviewModel extends MasterBaseModel{
    public movieId!: string;
    public roomId!: string;
    public showDate!: Date;
    public startTime!: Date;
    public movieName!: string;
    public roomName!: string;
}
