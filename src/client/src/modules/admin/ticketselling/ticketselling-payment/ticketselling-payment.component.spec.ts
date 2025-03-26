import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketsellingPaymentComponent } from './ticketselling-payment.component';

describe('TicketsellingPaymentComponent', () => {
  let component: TicketsellingPaymentComponent;
  let fixture: ComponentFixture<TicketsellingPaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketsellingPaymentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketsellingPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
