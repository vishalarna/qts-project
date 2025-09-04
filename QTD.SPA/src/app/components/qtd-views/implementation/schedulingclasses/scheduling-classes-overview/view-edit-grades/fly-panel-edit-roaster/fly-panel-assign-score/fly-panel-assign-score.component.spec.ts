import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAssignScoreComponent } from './fly-panel-assign-score.component';

describe('FlyPanelAssignScoreComponent', () => {
  let component: FlyPanelAssignScoreComponent;
  let fixture: ComponentFixture<FlyPanelAssignScoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAssignScoreComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAssignScoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
