import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketsellingComponent } from './ticketselling.component';

describe('TicketsellingComponent', () => {
  let component: TicketsellingComponent;
  let fixture: ComponentFixture<TicketsellingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketsellingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketsellingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
