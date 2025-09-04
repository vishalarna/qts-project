import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskShLinkComponent } from './fly-panel-task-sh-link.component';

describe('FlyPanelTaskShLinkComponent', () => {
  let component: FlyPanelTaskShLinkComponent;
  let fixture: ComponentFixture<FlyPanelTaskShLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskShLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskShLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
