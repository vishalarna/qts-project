import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditEvaluationComponent } from './fly-panel-edit-evaluation.component';

describe('FlyPanelEditEvaluationComponent', () => {
  let component: FlyPanelEditEvaluationComponent;
  let fixture: ComponentFixture<FlyPanelEditEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
