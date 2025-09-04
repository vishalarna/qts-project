import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProcedureTasksLinkComponent } from './fly-panel-procedure-tasks-link.component';

describe('FlyPanelProcedureTasksLinkComponent', () => {
  let component: FlyPanelProcedureTasksLinkComponent;
  let fixture: ComponentFixture<FlyPanelProcedureTasksLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProcedureTasksLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProcedureTasksLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
