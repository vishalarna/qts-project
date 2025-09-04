import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProcedureComponent } from './fly-panel-add-procedure.component';

describe('FlyPanelProcedureComponent', () => {
  let component: FlyPanelProcedureComponent;
  let fixture: ComponentFixture<FlyPanelProcedureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProcedureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProcedureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
