import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoommanagementComponent } from './room-list.component';

describe('RoommanagementComponent', () => {
  let component: RoommanagementComponent;
  let fixture: ComponentFixture<RoommanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoommanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoommanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
