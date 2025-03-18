import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotionmanagementComponent } from './promotionmanagement.component';

describe('PromotionmanagementComponent', () => {
  let component: PromotionmanagementComponent;
  let fixture: ComponentFixture<PromotionmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PromotionmanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PromotionmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
