import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifSurveyOverviewComponent } from './dif-survey-overview.component';

describe('DifSurveyOverviewComponent', () => {
  let component: DifSurveyOverviewComponent;
  let fixture: ComponentFixture<DifSurveyOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifSurveyOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifSurveyOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
