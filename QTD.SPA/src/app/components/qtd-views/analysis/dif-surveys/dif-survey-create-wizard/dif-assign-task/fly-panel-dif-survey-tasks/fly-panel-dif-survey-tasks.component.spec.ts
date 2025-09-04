import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelDifSurveyTasksComponent } from './fly-panel-dif-survey-tasks.component';

describe('FlyPanelDifSurveyTasksComponent', () => {
  let component: FlyPanelDifSurveyTasksComponent;
  let fixture: ComponentFixture<FlyPanelDifSurveyTasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelDifSurveyTasksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelDifSurveyTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
