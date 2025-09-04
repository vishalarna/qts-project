import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifSurveysComponent } from './dif-surveys.component';

describe('DifSurveysComponent', () => {
  let component: DifSurveysComponent;
  let fixture: ComponentFixture<DifSurveysComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifSurveysComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifSurveysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
