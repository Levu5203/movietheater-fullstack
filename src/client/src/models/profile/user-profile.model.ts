export interface UserProfileViewModel {
  readonly id: string;
  firstName: string;
  lastName: string;
  address?: string;
  dateOfBirth?: Date;
  gender: string;
  identityCard: string;
  avatar?: string;
  readonly email: string;
  phoneNumber: string;
  readonly username: string;
}