import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRrSafetyHazardLinkComponent } from './fly-panel-rr-safety-hazard-link.component';

describe('FlyPanelRrSafetyHazardLinkComponent', () => {
  let component: FlyPanelRrSafetyHazardLinkComponent;
  let fixture: ComponentFixture<FlyPanelRrSafetyHazardLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRrSafetyHazardLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRrSafetyHazardLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
