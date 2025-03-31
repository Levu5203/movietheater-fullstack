import { MasterBaseModel } from '../../core/models/master-base.model';

export class UserModel extends MasterBaseModel {
  public displayName!: string;
  public username!: string;
  public phoneNumber!: string;
  public gender!: string;
  public email!: string;
  public dateOfBirth!: Date;
}
