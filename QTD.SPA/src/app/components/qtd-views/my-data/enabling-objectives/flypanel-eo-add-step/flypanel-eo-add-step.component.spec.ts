import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoAddStepComponent } from './flypanel-eo-add-step.component';

describe('FlypanelEoAddStepComponent', () => {
  let component: FlypanelEoAddStepComponent;
  let fixture: ComponentFixture<FlypanelEoAddStepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoAddStepComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoAddStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
