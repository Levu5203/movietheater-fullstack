import { Component, OnInit } from '@angular/core';
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
  faFilter,
  faAnglesLeft,
  faAnglesRight,
  faRotateLeft,
} from '@fortawesome/free-solid-svg-icons';
import { EmployeeDetailComponent } from '../employee-detail/employee-detail.component';
import { EmployeeAddeditComponent } from '../employee-addedit/employee-addedit.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeManagementService } from '../employeemanagement.service';

@Component({
  selector: 'app-employeemanagement',
  imports: [
    FontAwesomeModule,
    EmployeeDetailComponent,
    EmployeeAddeditComponent,
    CommonModule,
    FormsModule
  ],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css',
})
export class EmployeemanagementComponent implements OnInit {
  //#region Font Awesome Icons
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faRotateLeft: IconDefinition = faRotateLeft;
  public faFilter: IconDefinition = faFilter;
  public faEdit: IconDefinition = faEdit;
  public faTrash: IconDefinition = faTrash;
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  public faAnglesRight: IconDefinition = faAnglesRight;
  //#endregion

  currentView: 'list' | 'detail' | 'add' | 'edit' = 'list';

  constructor(public employeeManagementService: EmployeeManagementService) { }

  ngOnInit(): void {
    this.employeeManagementService.currentView$.subscribe((view) => {
      this.currentView = view;
    });
  }

  public isDropdownOpen: boolean = false;
  public currentPage: number = 1;
  public totalPages: number = 10;

  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  onViewDetail(employee: any): void {
    this.employeeManagementService.goToDetail(employee);
  }

  onAdd(): void {
    this.employeeManagementService.goToAdd();
  }

  onEdit(employee: any): void {
    this.employeeManagementService.goToEdit(employee);
  }
  onDelete(employee: any): void {
    this.employeeManagementService.goToList();
  }
}
