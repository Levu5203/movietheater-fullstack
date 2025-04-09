import { MasterBaseModel } from "../../core/models/master-base.model"

export class PromotionModel extends MasterBaseModel {
    
    public promotionTitle!:string

    public description!: string 

    public discount!: number

    public startDate!: Date

    public endDate!: Date
    
    public image!: File
}
