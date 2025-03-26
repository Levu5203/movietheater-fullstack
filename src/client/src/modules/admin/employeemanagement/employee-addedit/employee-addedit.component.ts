import { Component, OnInit } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faCamera } from '@fortawesome/free-solid-svg-icons';
import { EmployeeManagementService } from '../employeemanagement.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee-addedit',
  imports: [FontAwesomeModule, CommonModule],
  templateUrl: './employee-addedit.component.html',
  styleUrl: './employee-addedit.component.css',
})
export class EmployeeAddeditComponent implements OnInit {
  //#region Font Awesome Icons
  public faCamera: IconDefinition = faCamera;
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

  saveChanges() {
    this.employeeManagementService.goToList();
  }
}
