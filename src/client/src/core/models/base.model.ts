export class BaseModel {
  public id!: string;
  public createdBy!: string;
  public createdAt!: Date;
  public updatedAt?: Date;
  public updatedBy?: string;
  public deletedAt?: Date;
  public deletedBy?: string;
  public isDelete!: boolean;
}
