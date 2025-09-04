import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifAssignEmployeesComponent } from './dif-assign-employees.component';

describe('DifAssignEmployeesComponent', () => {
  let component: DifAssignEmployeesComponent;
  let fixture: ComponentFixture<DifAssignEmployeesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifAssignEmployeesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifAssignEmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
