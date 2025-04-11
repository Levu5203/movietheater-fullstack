import { Observable } from 'rxjs';
import { EmployeeModel } from '../../models/user/employee.model';
import { IMasterDataService } from '../master-data/master-data.service.interface';
import { UserProfileViewModel } from '../../models/profile/user-profile.model';

export interface IProfileService
  extends IMasterDataService<UserProfileViewModel> {
  getProfile(): Observable<UserProfileViewModel>;
  updateProfile(
    profileData: Partial<UserProfileViewModel>
  ): Observable<UserProfileViewModel>;
  updateProfileWithFile(
    profileData: FormData
  ): Observable<UserProfileViewModel>;
  changePassword(data: any): Observable<any>;
}
