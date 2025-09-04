import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAddSafetyHazardsComponent } from './flypanel-add-safety-hazards.component';

describe('FlypanelAddSafetyHazardsComponent', () => {
  let component: FlypanelAddSafetyHazardsComponent;
  let fixture: ComponentFixture<FlypanelAddSafetyHazardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAddSafetyHazardsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAddSafetyHazardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
