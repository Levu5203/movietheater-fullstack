import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatepromotionComponent } from './updatepromotion.component';

describe('UpdatepromotionComponent', () => {
  let component: UpdatepromotionComponent;
  let fixture: ComponentFixture<UpdatepromotionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdatepromotionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdatepromotionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
