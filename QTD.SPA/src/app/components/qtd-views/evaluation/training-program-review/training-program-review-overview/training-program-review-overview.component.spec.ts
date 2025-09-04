import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingProgramReviewOverviewComponent } from './training-program-review-overview.component';

describe('TrainingProgramReviewOverviewComponent', () => {
  let component: TrainingProgramReviewOverviewComponent;
  let fixture: ComponentFixture<TrainingProgramReviewOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingProgramReviewOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingProgramReviewOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
