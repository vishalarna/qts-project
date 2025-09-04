import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelFilterAddDifSurveyEmpsComponent } from './fly-in-filter-add-dif-survey-employees.component';

describe('FlypanelFilterAddDifSurveyEmpsComponent', () => {
  let component: FlypanelFilterAddDifSurveyEmpsComponent;
  let fixture: ComponentFixture<FlypanelFilterAddDifSurveyEmpsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelFilterAddDifSurveyEmpsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelFilterAddDifSurveyEmpsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
