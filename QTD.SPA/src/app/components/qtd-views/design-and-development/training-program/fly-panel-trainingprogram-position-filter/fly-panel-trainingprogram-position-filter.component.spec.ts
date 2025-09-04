import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTrainingprogramPositionFilterComponent } from './fly-panel-trainingprogram-position-filter.component';

describe('FlyPanelTrainingprogramPositionFilterComponent', () => {
  let component: FlyPanelTrainingprogramPositionFilterComponent;
  let fixture: ComponentFixture<FlyPanelTrainingprogramPositionFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTrainingprogramPositionFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTrainingprogramPositionFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
