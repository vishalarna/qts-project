import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentEvaluationOverviewComponent } from './student-evaluation-overview.component';

describe('StudentEvaluationOverviewComponent', () => {
  let component: StudentEvaluationOverviewComponent;
  let fixture: ComponentFixture<StudentEvaluationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentEvaluationOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentEvaluationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
