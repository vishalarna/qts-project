import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoEmployeesComponent } from './eo-employees.component';

describe('EoEmployeesComponent', () => {
  let component: EoEmployeesComponent;
  let fixture: ComponentFixture<EoEmployeesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoEmployeesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoEmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
