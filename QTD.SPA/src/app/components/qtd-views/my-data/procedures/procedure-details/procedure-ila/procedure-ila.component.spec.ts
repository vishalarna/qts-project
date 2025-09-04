import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureIlaComponent } from './procedure-ila.component';

describe('ProcedureIlaComponent', () => {
  let component: ProcedureIlaComponent;
  let fixture: ComponentFixture<ProcedureIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
