import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskProcedureLinkComponent } from './fly-panel-task-procedure-link.component';

describe('FlyPanelTaskProcedureLinkComponent', () => {
  let component: FlyPanelTaskProcedureLinkComponent;
  let fixture: ComponentFixture<FlyPanelTaskProcedureLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskProcedureLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskProcedureLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
