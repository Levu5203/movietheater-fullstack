export class UserInformationFromToken {
  public id!: string;
  public username!: string;
  public email!: string;
  public displayName!: string;
  public roles!: string[];
  public exp!: number;
}
