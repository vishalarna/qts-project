import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureTaskComponent } from './procedure-task.component';

describe('ProcedureTaskComponent', () => {
  let component: ProcedureTaskComponent;
  let fixture: ComponentFixture<ProcedureTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
