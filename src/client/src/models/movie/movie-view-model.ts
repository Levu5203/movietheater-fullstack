import { MasterBaseModel } from "../../core/models/master-base.model";
import { ShowtimeviewModel } from "../showtime/showtimeview.model";

export class MovieViewModel extends MasterBaseModel {
    public name!: string;
    public duration!: number;
    public origin!: string;
    public description!: string;
    public director!: string;
    public actors!: string;
    public version!: string;
    public posterImage!: string;
    public status!: string;
    public releasedDate!: Date;
    public endDate!: Date;
    public cinemaRoomId!: string;
    public selectedGenres!: string[];
    public selectedShowTimeSlots!: string[];
    public showtimes!: ShowtimeviewModel[];
}
