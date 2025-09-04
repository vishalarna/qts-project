import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelDifSurveyEmployeesComponent } from './fly-panel-dif-survey-employees.component';

describe('FlyPanelDifSurveyEmployeesComponent', () => {
  let component: FlyPanelDifSurveyEmployeesComponent;
  let fixture: ComponentFixture<FlyPanelDifSurveyEmployeesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelDifSurveyEmployeesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelDifSurveyEmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
