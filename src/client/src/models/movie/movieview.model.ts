import { MasterBaseModel } from "../../core/models/master-base.model";
import { ShowtimeviewModel } from "../showtime/showtimeview.model";

export class MovieviewModel extends MasterBaseModel{
    public name!: string;
    public duration!: number;
    public origin!: string;
    public description!: string;
    public director!: string;
    public actors!: string;
    public version!: number;
    public posterUrl!: File | string | null;
    public status!: number;
    public releasedDate!: Date;
    public showtimes!: ShowtimeviewModel[];
    public genres!: string[];
    public cinemarooms!: string[];

}
