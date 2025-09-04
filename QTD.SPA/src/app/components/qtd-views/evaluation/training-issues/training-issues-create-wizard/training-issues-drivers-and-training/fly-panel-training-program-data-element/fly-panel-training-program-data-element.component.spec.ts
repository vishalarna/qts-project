import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTrainingProgramDataElementComponent } from './fly-panel-training-program-data-element.component';

describe('FlyPanelTrainingProgramDataElementComponent', () => {
  let component: FlyPanelTrainingProgramDataElementComponent;
  let fixture: ComponentFixture<FlyPanelTrainingProgramDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTrainingProgramDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTrainingProgramDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
