import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RRProcedureComponent } from './rr-procedure.component';

describe('RRProcedureComponent', () => {
  let component: RRProcedureComponent;
  let fixture: ComponentFixture<RRProcedureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RRProcedureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RRProcedureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
