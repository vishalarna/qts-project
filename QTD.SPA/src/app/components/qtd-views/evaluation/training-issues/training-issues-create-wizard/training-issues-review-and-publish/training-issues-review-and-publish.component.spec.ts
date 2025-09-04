import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssuesReviewAndPublishComponent } from './training-issues-review-and-publish.component';

describe('TrainingIssuesReviewAndPublishComponent', () => {
  let component: TrainingIssuesReviewAndPublishComponent;
  let fixture: ComponentFixture<TrainingIssuesReviewAndPublishComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssuesReviewAndPublishComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssuesReviewAndPublishComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
