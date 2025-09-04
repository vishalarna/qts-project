import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditTaskTrainingGroupComponent } from './fly-panel-edit-task-training-group.component';

describe('FlyPanelEditTaskTrainingGroupComponent', () => {
  let component: FlyPanelEditTaskTrainingGroupComponent;
  let fixture: ComponentFixture<FlyPanelEditTaskTrainingGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditTaskTrainingGroupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditTaskTrainingGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
