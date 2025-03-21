import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { BookedTicketsComponent } from './booked-tickets/booked-tickets.component';
import { ScoreHistoryComponent } from './score-history/score-history.component';

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
export class ProfileComponent {
  public faSearch: IconDefinition = faSearch;
  activeTab: string = 'myProfile';

  changeTab(tabName: string) {
    this.activeTab = tabName;
  }
}
