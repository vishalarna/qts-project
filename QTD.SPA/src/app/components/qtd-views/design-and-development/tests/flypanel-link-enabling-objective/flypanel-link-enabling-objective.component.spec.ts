import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelLinkEnablingObjectiveComponent } from './flypanel-link-enabling-objective.component';

describe('FlypanelLinkEnablingObjectiveComponent', () => {
  let component: FlypanelLinkEnablingObjectiveComponent;
  let fixture: ComponentFixture<FlypanelLinkEnablingObjectiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelLinkEnablingObjectiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelLinkEnablingObjectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
