import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faUser, faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { EmployeeModel } from '../../../../models/user/employee.model';
import { CommonModule, DatePipe } from '@angular/common';
import { UserModel } from '../../../../models/user/user.model';

@Component({
  selector: 'app-customer-detail',
  imports: [FontAwesomeModule, DatePipe, CommonModule],
  templateUrl: './customer-detail.component.html',
  styleUrl: './customer-detail.component.css',
})
export class CustomerDetailComponent {
  public faUser: IconDefinition = faUser;
  public faArrowLeft: IconDefinition = faArrowLeft;

  //#endregion
  @Input() public selectedItem!: UserModel | undefined | null;
  @Output() close = new EventEmitter<void>();

  onClose() {
    this.close.emit();
  }
}
