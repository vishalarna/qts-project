import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkPositionTaskComponent } from './fly-panel-link-position-task.component';

describe('FlyPanelLinkPositionTaskComponent', () => {
  let component: FlyPanelLinkPositionTaskComponent;
  let fixture: ComponentFixture<FlyPanelLinkPositionTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkPositionTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkPositionTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
