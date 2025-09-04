import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifCreateSurveyComponent } from './dif-create-survey.component';

describe('DifCreateSurveyComponent', () => {
  let component: DifCreateSurveyComponent;
  let fixture: ComponentFixture<DifCreateSurveyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifCreateSurveyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifCreateSurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
