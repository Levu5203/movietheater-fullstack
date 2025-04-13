import { MasterBaseModel } from '../../core/models/master-base.model';

export class EmployeeModel extends MasterBaseModel {
  public firstName!: string;
  public lastName!: string;
  public displayName!: string;
  public username!: string;
  public phoneNumber!: string;
  public gender!: string;
  public email!: string;
  public dateOfBirth!: Date;
  public identityCard!: string;
  public address!: string;
  public avatar!: File | string | null;
}
