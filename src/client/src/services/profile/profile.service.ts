import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { UserProfileViewModel } from '../../models/profile/user-profile.model';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private apiUrl = 'http://localhost:5063/api/v1/user';
  private getProfileUrl = `${this.apiUrl}`;
  private updateProfileUrl = `${this.apiUrl}/edit`;
  private changePasswordUrl = `${this.apiUrl}/change-password`;

  constructor(private http: HttpClient) {}

  getProfile(): Observable<UserProfileViewModel> {
    return this.http.get<UserProfileViewModel>(this.getProfileUrl).pipe(
      catchError((error) => {
        console.error('Failed to get profile:', error);
        throw error;
      })
    );
  }

  updateProfile(
    profileData: Partial<UserProfileViewModel>
  ): Observable<UserProfileViewModel> {
    return this.http
      .put<UserProfileViewModel>(this.updateProfileUrl, profileData)
      .pipe(
        catchError((error) => {
          console.error('Failed to update profile:', error);
          throw error;
        })
      );
  }

  updateProfileWithFile(
    profileData: FormData
  ): Observable<UserProfileViewModel> {
    return this.http
      .put<UserProfileViewModel>(this.updateProfileUrl, profileData)
      .pipe(
        catchError((error) => {
          console.error('Failed to update profile:', error);
          throw error;
        })
      );
  }

  changePassword(data: any): Observable<any> {
    return this.http.post(this.changePasswordUrl, data);
  }
}
