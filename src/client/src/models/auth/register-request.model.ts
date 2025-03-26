export class RegisterRequest {
  public username!: string;
  public email!: string;
  public password!: string;
  public confirmPassword!: string;
  public firstName!: string;
  public lastName!: string;
  public dateOfBirth!: Date | null;
  public identityCard!: string;
  public phoneNumber!: string | null;
  public gender!: string;
}
