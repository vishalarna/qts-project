import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelFilterDifSurveyComponent } from './fly-panel-filter-dif-survey.component';

describe('FlyPanelFilterDifSurveyComponent', () => {
  let component: FlyPanelFilterDifSurveyComponent;
  let fixture: ComponentFixture<FlyPanelFilterDifSurveyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelFilterDifSurveyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelFilterDifSurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
