import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelToolTaskLinkComponent } from './fly-panel-tool-task-link.component';

describe('FlyPanelToolTaskLinkComponent', () => {
  let component: FlyPanelToolTaskLinkComponent;
  let fixture: ComponentFixture<FlyPanelToolTaskLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelToolTaskLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelToolTaskLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
