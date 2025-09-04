import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPublishStudentEvaluationComponent } from './fly-panel-publish-student-evaluation.component';

describe('FlyPanelPublishStudentEvaluationComponent', () => {
  let component: FlyPanelPublishStudentEvaluationComponent;
  let fixture: ComponentFixture<FlyPanelPublishStudentEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPublishStudentEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPublishStudentEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
