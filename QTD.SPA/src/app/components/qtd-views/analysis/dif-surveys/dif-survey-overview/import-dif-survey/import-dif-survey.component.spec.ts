import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportDifSurveyComponent } from './import-dif-survey.component';

describe('ImportDifSurveyComponent', () => {
  let component: ImportDifSurveyComponent;
  let fixture: ComponentFixture<ImportDifSurveyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportDifSurveyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportDifSurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
