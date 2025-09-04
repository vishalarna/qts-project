import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssuesDetailsComponent } from './training-issues-details.component';

describe('TrainingIssuesDetailsComponent', () => {
  let component: TrainingIssuesDetailsComponent;
  let fixture: ComponentFixture<TrainingIssuesDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssuesDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssuesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
