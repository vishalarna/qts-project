import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelProcedureComponent } from './flypanel-procedure.component';

describe('FlypanelProcedureComponent', () => {
  let component: FlypanelProcedureComponent;
  let fixture: ComponentFixture<FlypanelProcedureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelProcedureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelProcedureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
