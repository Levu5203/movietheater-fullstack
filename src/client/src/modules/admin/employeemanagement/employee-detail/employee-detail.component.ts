import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faUser } from '@fortawesome/free-solid-svg-icons';
import { EmployeeModel } from '../../../../models/user/employee.model';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee-detail',
  imports: [FontAwesomeModule, DatePipe, CommonModule],
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.css',
})
export class EmployeeDetailComponent {
  //#region Font Awesome Icons
  public faUser: IconDefinition = faUser;
  public faArrowLeft: IconDefinition = faArrowLeft;

  //#endregion
  @Input() public selectedItem!: EmployeeModel | undefined | null;
  @Output() close = new EventEmitter<void>();

  onClose() {
    this.close.emit();
  }
}
