import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddTrainingProgramComponent } from './fly-panel-add-training-program.component';

describe('FlyPanelAddTrainingProgramComponent', () => {
  let component: FlyPanelAddTrainingProgramComponent;
  let fixture: ComponentFixture<FlyPanelAddTrainingProgramComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddTrainingProgramComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddTrainingProgramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
