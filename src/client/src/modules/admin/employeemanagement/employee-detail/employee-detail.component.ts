import { Component, OnInit } from '@angular/core';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { EmployeeManagementService } from '../employeemanagement.service';

@Component({
  selector: 'app-employee-detail',
  imports: [FontAwesomeModule],
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.css',
})
export class EmployeeDetailComponent implements OnInit {
  //#region Font Awesome Icons
  public faUser: IconDefinition = faUser;
  //#endregion

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
