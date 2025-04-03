import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faUser } from '@fortawesome/free-solid-svg-icons';
import { EmployeeManagementService } from '../employeemanagement.service';
import { EmployeeModel } from '../../../../models/user/employee.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee-detail',
  imports: [FontAwesomeModule, DatePipe],
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.css',
})
export class EmployeeDetailComponent implements OnInit {
  //#region Font Awesome Icons
  public faUser: IconDefinition = faUser;
  public faArrowLeft: IconDefinition = faArrowLeft;

  //#endregion
  @Input() public selectedItem!: EmployeeModel | undefined | null;
  @Output() close = new EventEmitter<void>();

  onClose() {
    this.close.emit();
  }
  employee: any;

  constructor(private employeeManagementService: EmployeeManagementService) {}

  ngOnInit(): void {
    this.employeeManagementService.selectedEmployee$.subscribe((employee) => {
      this.employee = employee;
    });
  }

  goBack() {
    this.employeeManagementService.goToList();
  }

  onEdit(employee: any) {
    this.employeeManagementService.goToEdit(employee);
  }

  onDelete(employee: any) {
    this.employeeManagementService.goToList();
  }
}
