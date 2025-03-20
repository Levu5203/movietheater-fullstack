import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeatshowtimeComponent } from './seatshowtime.component';

describe('SeatshowtimeComponent', () => {
  let component: SeatshowtimeComponent;
  let fixture: ComponentFixture<SeatshowtimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SeatshowtimeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SeatshowtimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
