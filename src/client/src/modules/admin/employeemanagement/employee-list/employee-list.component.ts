import { Component } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faArrowLeft,
  faEdit,
  faTrash,
  faInfoCircle,
  faAngleRight,
  faAngleLeft,
} from '@fortawesome/free-solid-svg-icons';
import { EmployeeDetailComponent } from "../employee-detail/employee-detail.component";
import { EmployeeAddeditComponent } from "../employee-addedit/employee-addedit.component";

@Component({
  selector: 'app-employeemanagement',
  imports: [FontAwesomeModule, EmployeeDetailComponent, EmployeeAddeditComponent],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css',
})
export class EmployeemanagementComponent {
  //#region Font Awesome Icons
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faEdit: IconDefinition = faEdit;
  public faTrash: IconDefinition = faTrash;
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  //#endregion
}
