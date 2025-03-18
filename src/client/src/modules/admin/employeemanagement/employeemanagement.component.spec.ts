import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeemanagementComponent } from './employeemanagement.component';

describe('EmployeemanagementComponent', () => {
  let component: EmployeemanagementComponent;
  let fixture: ComponentFixture<EmployeemanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeemanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeemanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
