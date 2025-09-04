import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingProgramReviewComponent } from './training-program-review.component';

describe('TrainingProgramReviewComponent', () => {
  let component: TrainingProgramReviewComponent;
  let fixture: ComponentFixture<TrainingProgramReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingProgramReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingProgramReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
