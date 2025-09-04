import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelMetaTaskLinkComponent } from './fly-panel-meta-task-link.component';

describe('FlyPanelMetaTaskLinkComponent', () => {
  let component: FlyPanelMetaTaskLinkComponent;
  let fixture: ComponentFixture<FlyPanelMetaTaskLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelMetaTaskLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelMetaTaskLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
