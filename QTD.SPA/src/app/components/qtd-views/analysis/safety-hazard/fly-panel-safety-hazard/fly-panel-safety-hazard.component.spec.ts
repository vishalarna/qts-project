import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelSafetyHazardComponent } from './fly-panel-safety-hazard.component';

describe('FlyPanelSafetyHazardComponent', () => {
  let component: FlyPanelSafetyHazardComponent;
  let fixture: ComponentFixture<FlyPanelSafetyHazardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelSafetyHazardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelSafetyHazardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
