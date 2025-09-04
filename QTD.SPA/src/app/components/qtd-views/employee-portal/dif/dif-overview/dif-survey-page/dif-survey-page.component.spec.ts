import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifSurveyPageComponent } from './dif-survey-page.component';

describe('DifSurveyPageComponent', () => {
  let component: DifSurveyPageComponent;
  let fixture: ComponentFixture<DifSurveyPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifSurveyPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifSurveyPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
