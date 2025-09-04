import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddStepComponent } from './fly-panel-add-step.component';

describe('FlyPanelAddStepComponent', () => {
  let component: FlyPanelAddStepComponent;
  let fixture: ComponentFixture<FlyPanelAddStepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddStepComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
