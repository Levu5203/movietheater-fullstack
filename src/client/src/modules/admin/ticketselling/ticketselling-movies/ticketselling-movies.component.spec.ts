import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketsellingMoviesComponent } from './ticketselling-movies.component';

describe('TicketsellingMoviesComponent', () => {
  let component: TicketsellingMoviesComponent;
  let fixture: ComponentFixture<TicketsellingMoviesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketsellingMoviesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketsellingMoviesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
