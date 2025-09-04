import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssuesOverviewComponent } from './training-issues-overview.component';

describe('TrainingIssuesOverviewComponent', () => {
  let component: TrainingIssuesOverviewComponent;
  let fixture: ComponentFixture<TrainingIssuesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssuesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssuesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
