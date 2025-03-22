import { Component } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-booked-tickets',
  imports: [FontAwesomeModule],
  templateUrl: './booked-tickets.component.html',
  styleUrl: './booked-tickets.component.css',
})
export class BookedTicketsComponent {
  public faSearch: IconDefinition = faSearch;
}
