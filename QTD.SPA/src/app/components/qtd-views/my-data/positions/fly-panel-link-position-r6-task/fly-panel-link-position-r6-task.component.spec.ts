import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkPositionR6TaskComponent } from './fly-panel-link-position-r6-task.component';

describe('FlyPanelLinkPositionR6TaskComponent', () => {
  let component: FlyPanelLinkPositionR6TaskComponent;
  let fixture: ComponentFixture<FlyPanelLinkPositionR6TaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkPositionR6TaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkPositionR6TaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
