import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkTaskComponent } from './fly-panel-link-task.component';

describe('FlyPanelLinkTaskComponent', () => {
  let component: FlyPanelLinkTaskComponent;
  let fixture: ComponentFixture<FlyPanelLinkTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
