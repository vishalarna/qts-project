import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskRrLinkComponent } from './fly-panel-task-rr-link.component';

describe('FlyPanelTaskRrLinkComponent', () => {
  let component: FlyPanelTaskRrLinkComponent;
  let fixture: ComponentFixture<FlyPanelTaskRrLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskRrLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskRrLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
