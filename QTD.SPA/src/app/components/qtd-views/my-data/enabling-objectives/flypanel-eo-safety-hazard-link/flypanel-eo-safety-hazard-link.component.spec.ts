import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoSafetyHazardLinkComponent } from './flypanel-eo-safety-hazard-link.component';

describe('FlypanelEoSafetyHazardLinkComponent', () => {
  let component: FlypanelEoSafetyHazardLinkComponent;
  let fixture: ComponentFixture<FlypanelEoSafetyHazardLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoSafetyHazardLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoSafetyHazardLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
