import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRrTasksLinkComponent } from './fly-panel-rr-tasks-link.component';

describe('FlyPanelRrTasksLinkComponent', () => {
  let component: FlyPanelRrTasksLinkComponent;
  let fixture: ComponentFixture<FlyPanelRrTasksLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRrTasksLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRrTasksLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
