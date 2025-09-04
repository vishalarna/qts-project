import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssuesDriversAndTrainingComponent } from './training-issues-drivers-and-training.component';

describe('TrainingIssuesDriversAndTrainingComponent', () => {
  let component: TrainingIssuesDriversAndTrainingComponent;
  let fixture: ComponentFixture<TrainingIssuesDriversAndTrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssuesDriversAndTrainingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssuesDriversAndTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
