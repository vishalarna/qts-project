import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskEosLinkComponent } from './fly-panel-task-eos-link.component';

describe('FlyPanelTaskEosLinkComponent', () => {
  let component: FlyPanelTaskEosLinkComponent;
  let fixture: ComponentFixture<FlyPanelTaskEosLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskEosLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskEosLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
