import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { UserProfileViewModel } from '../models/profile/user-profile.model';
import { AuthService } from './auth/auth.service';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private apiUrl = 'http://localhost:5063/api/profile';
  private getProfileUrl = `${this.apiUrl}`;
  private updateProfileUrl = `${this.apiUrl}/edit`;

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getHeaders() {
    const token = this.authService.getAccessToken();
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
  }

  getProfile(): Observable<UserProfileViewModel> {
    return this.http
      .get<UserProfileViewModel>(this.getProfileUrl, {
        headers: this.getHeaders(),
      })
      .pipe(
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
      .put<UserProfileViewModel>(this.updateProfileUrl, profileData, {
        headers: this.getHeaders(),
      })
      .pipe(
        catchError((error) => {
          console.error('Failed to update profile:', error);
          throw error;
        })
      );
  }
}
