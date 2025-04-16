import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotiondetailComponent } from './promotiondetail.component';

describe('PromotiondetailComponent', () => {
  let component: PromotiondetailComponent;
  let fixture: ComponentFixture<PromotiondetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PromotiondetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PromotiondetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
