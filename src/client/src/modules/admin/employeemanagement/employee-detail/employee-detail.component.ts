import { Component } from '@angular/core';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faUser } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-employee-detail',
  imports: [FontAwesomeModule],
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.css',
})
export class EmployeeDetailComponent {
  //#region Font Awesome Icons
  public faUser: IconDefinition = faUser;
  //#endregion
}
