import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCreateMetaIlaStudentEvaluationComponent } from './fly-panel-create-meta-ila-student-evaluation.component';

describe('FlyPanelCreateMetaIlaStudentEvaluationComponent', () => {
  let component: FlyPanelCreateMetaIlaStudentEvaluationComponent;
  let fixture: ComponentFixture<FlyPanelCreateMetaIlaStudentEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCreateMetaIlaStudentEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCreateMetaIlaStudentEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
