import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPublishTrainingProgramComponent } from './fly-panel-publish-training-program.component';

describe('FlyPanelPublishTrainingProgramComponent', () => {
  let component: FlyPanelPublishTrainingProgramComponent;
  let fixture: ComponentFixture<FlyPanelPublishTrainingProgramComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPublishTrainingProgramComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPublishTrainingProgramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
