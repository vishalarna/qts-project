import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelSafetyHazardDataElementComponent } from './fly-panel-safety-hazard-data-element.component';

describe('FlyPanelSafetyHazardDataElementComponent', () => {
  let component: FlyPanelSafetyHazardDataElementComponent;
  let fixture: ComponentFixture<FlyPanelSafetyHazardDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelSafetyHazardDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelSafetyHazardDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
