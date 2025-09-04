import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskPosLinkComponent } from './fly-panel-task-pos-link.component';

describe('FlyPanelTaskPosLinkComponent', () => {
  let component: FlyPanelTaskPosLinkComponent;
  let fixture: ComponentFixture<FlyPanelTaskPosLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskPosLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskPosLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
