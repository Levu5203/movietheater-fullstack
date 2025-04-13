import { MasterBaseModel } from "../../core/models/master-base.model";

export class MovieViewModel extends MasterBaseModel {
    public name!: string;
    public duration!: number;
    public origin!: string;
    public description!: string;
    public version!: string;
    public director!: string;
    public actors!: string;
    public status!: string;
    public releasedDate!: Date;
    public endDate!: Date;
    public posterImage!: File | null;
    public cinemaRoomId!: string[];
    public selectedGenres!: string[];
    public selectedShowTimeSlots!: string[];
}
