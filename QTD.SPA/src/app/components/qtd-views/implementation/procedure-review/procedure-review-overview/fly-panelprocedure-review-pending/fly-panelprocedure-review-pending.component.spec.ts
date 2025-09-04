import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelprocedureReviewPendingComponent } from './fly-panelprocedure-review-pending.component';

describe('FlyPanelprocedureReviewPendingComponent', () => {
  let component: FlyPanelprocedureReviewPendingComponent;
  let fixture: ComponentFixture<FlyPanelprocedureReviewPendingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelprocedureReviewPendingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelprocedureReviewPendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
