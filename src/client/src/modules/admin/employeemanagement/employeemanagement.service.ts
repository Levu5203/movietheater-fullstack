import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EmployeeManagementService {
  private currentView = new BehaviorSubject<'list' | 'detail' | 'add' | 'edit'>('list');
  private selectedEmployee = new BehaviorSubject<any>(null);

  currentView$ = this.currentView.asObservable();
  selectedEmployee$ = this.selectedEmployee.asObservable();

  constructor() {}

  setCurrentView(view: 'list' | 'detail' | 'add' | 'edit') {
    this.currentView.next(view);
  }

  getCurrentView() {
    return this.currentView.value;
  }

  setSelectedEmployee(employee: any) {
    this.selectedEmployee.next(employee);
  }

  getSelectedEmployee() {
    return this.selectedEmployee.asObservable();
  }

  goToList() {
    this.setCurrentView('list');
    this.setSelectedEmployee(null);
  }

  goToDetail(employee: any) {
    this.setCurrentView('detail');
    this.setSelectedEmployee(employee);
  }

  goToAdd() {
    this.setCurrentView('add');
    this.setSelectedEmployee(null);
  }

  goToEdit(employee: any) {
    this.setCurrentView('edit');
    this.setSelectedEmployee(employee);
  }
}
