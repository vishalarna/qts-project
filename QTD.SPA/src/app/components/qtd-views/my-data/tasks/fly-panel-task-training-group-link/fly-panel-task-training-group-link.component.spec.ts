import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskTrainingGroupLinkComponent } from './fly-panel-task-training-group-link.component';

describe('FlyPanelTaskTrainingGroupLinkComponent', () => {
  let component: FlyPanelTaskTrainingGroupLinkComponent;
  let fixture: ComponentFixture<FlyPanelTaskTrainingGroupLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskTrainingGroupLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskTrainingGroupLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
