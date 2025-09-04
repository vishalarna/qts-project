import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssuesComponent } from './training-issues.component';

describe('TrainingIssuesComponent', () => {
  let component: TrainingIssuesComponent;
  let fixture: ComponentFixture<TrainingIssuesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssuesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
