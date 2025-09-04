import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTrainingprogramPositionLinkComponent } from './fly-panel-trainingprogram-position-link.component';

describe('FlyPanelTrainingprogramPositionLinkComponent', () => {
  let component: FlyPanelTrainingprogramPositionLinkComponent;
  let fixture: ComponentFixture<FlyPanelTrainingprogramPositionLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTrainingprogramPositionLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTrainingprogramPositionLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
