import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShProcedureComponent } from './sh-procedure.component';

describe('ShProcedureComponent', () => {
  let component: ShProcedureComponent;
  let fixture: ComponentFixture<ShProcedureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShProcedureComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShProcedureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
