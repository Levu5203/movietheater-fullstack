import { MasterBaseModel } from "../../core/models/master-base.model";
import { ShowtimeviewModel } from "../showtime/showtimeview.model";

export class MovieViewModel extends MasterBaseModel {
    public name!: string;
    public duration!: number;
    public origin!: string;
    public description!: string;
    public director!: string;
    public actors!: string;
    public version!: number;
    public posterImage!: string;
    public status!: number;
    public releasedDate!: Date;
    public endDate!: Date;
    public cinemaRooms!: string[];
    public genres!: string[];
    public selectedShowTimeSlots!: string[];
    public showtimes!: ShowtimeviewModel[];
}
