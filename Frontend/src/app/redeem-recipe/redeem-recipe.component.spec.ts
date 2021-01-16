import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RedeemRecipeComponent } from './redeem-recipe.component';

describe('RedeemRecipeComponent', () => {
  let component: RedeemRecipeComponent;
  let fixture: ComponentFixture<RedeemRecipeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RedeemRecipeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RedeemRecipeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
