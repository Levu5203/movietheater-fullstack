import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { BookedTicketsComponent } from './booked-tickets/booked-tickets.component';
import { ScoreHistoryComponent } from './score-history/score-history.component';
import { UserProfileViewModel } from '../../../models/profile/user-profile.model';
import { ProfileService } from '../../../services/profile/profile.service';
import { ModalService } from '../../../services/modal.service';

@Component({
  selector: 'app-profile',
  imports: [
    CommonModule,
    FontAwesomeModule,
    EditProfileComponent,
    BookedTicketsComponent,
    ScoreHistoryComponent,
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit {
  public faSearch: IconDefinition = faSearch;
  activeTab: string = 'myProfile';

  profile: UserProfileViewModel | null = null;
  error: string | null = null;

  constructor(
    private readonly profileService: ProfileService,
    private readonly modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.loadProfile();
  }

  loadProfile(): void {
    this.error = null;

    this.profileService.getProfile().subscribe({
      next: (profile) => {
        this.profile = profile;
      },
      error: (err) => {
        this.error = err.message || 'Failed to load profile';
        console.error(err);
      },
    });
  }

  changeTab(tabName: string) {
    this.activeTab = tabName;
  }

  // Show form login
  openLogin() {
    this.modalService.open('login');
  }

  // Show form register
  openRegister() {
    this.modalService.open('register');
  }
}
