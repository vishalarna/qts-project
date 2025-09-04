import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTrainingprogramYearFilterComponent } from './fly-panel-trainingprogram-year-filter.component';

describe('FlyPanelTrainingprogramYearFilterComponent', () => {
  let component: FlyPanelTrainingprogramYearFilterComponent;
  let fixture: ComponentFixture<FlyPanelTrainingprogramYearFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTrainingprogramYearFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTrainingprogramYearFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
