import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditPerformanceCriteriaComponent } from './fly-panel-edit-performance-criteria.component';

describe('FlyPanelEditPerformanceCriteriaComponent', () => {
  let component: FlyPanelEditPerformanceCriteriaComponent;
  let fixture: ComponentFixture<FlyPanelEditPerformanceCriteriaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditPerformanceCriteriaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditPerformanceCriteriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
