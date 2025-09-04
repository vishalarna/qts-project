import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkPositionR5TaskComponent } from './fly-panel-link-position-r5-task.component';

describe('FlyPanelLinkPositionR5TaskComponent', () => {
  let component: FlyPanelLinkPositionR5TaskComponent;
  let fixture: ComponentFixture<FlyPanelLinkPositionR5TaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkPositionR5TaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkPositionR5TaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
