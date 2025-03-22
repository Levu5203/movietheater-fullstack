import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TiketsellingSelectseatComponent } from './tiketselling-selectseat.component';

describe('TiketsellingSelectseatComponent', () => {
  let component: TiketsellingSelectseatComponent;
  let fixture: ComponentFixture<TiketsellingSelectseatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TiketsellingSelectseatComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TiketsellingSelectseatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
