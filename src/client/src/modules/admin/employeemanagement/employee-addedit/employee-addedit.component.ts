import { Component } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faCamera } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-employee-addedit',
  imports: [FontAwesomeModule],
  templateUrl: './employee-addedit.component.html',
  styleUrl: './employee-addedit.component.css',
})
export class EmployeeAddeditComponent {
  //#region Font Awesome Icons
  public faCamera: IconDefinition = faCamera;
  //#endregion
}
