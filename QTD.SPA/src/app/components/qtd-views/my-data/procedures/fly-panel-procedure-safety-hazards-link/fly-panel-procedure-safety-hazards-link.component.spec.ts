import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProcedureSafetyHazardsLinkComponent } from './fly-panel-procedure-safety-hazards-link.component';

describe('FlyPanelProcedureSafetyHazardsLinkComponent', () => {
  let component: FlyPanelProcedureSafetyHazardsLinkComponent;
  let fixture: ComponentFixture<FlyPanelProcedureSafetyHazardsLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProcedureSafetyHazardsLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProcedureSafetyHazardsLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
