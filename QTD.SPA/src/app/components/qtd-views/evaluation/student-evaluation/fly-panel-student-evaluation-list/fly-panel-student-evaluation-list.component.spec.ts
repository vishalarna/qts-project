import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelStudentEvaluationListComponent } from './fly-panel-student-evaluation-list.component';

describe('FlyPanelStudentEvaluationListComponent', () => {
  let component: FlyPanelStudentEvaluationListComponent;
  let fixture: ComponentFixture<FlyPanelStudentEvaluationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelStudentEvaluationListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelStudentEvaluationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
