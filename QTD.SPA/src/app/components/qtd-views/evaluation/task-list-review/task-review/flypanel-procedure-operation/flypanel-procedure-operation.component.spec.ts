import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelProcedureOperationComponent } from './flypanel-procedure-operation.component';

describe('FlypanelProcedureOperationComponent', () => {
  let component: FlypanelProcedureOperationComponent;
  let fixture: ComponentFixture<FlypanelProcedureOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelProcedureOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelProcedureOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
