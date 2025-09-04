import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionEmployeesComponent } from './position-employees.component';

describe('PositionEmployeesComponent', () => {
  let component: PositionEmployeesComponent;
  let fixture: ComponentFixture<PositionEmployeesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PositionEmployeesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionEmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
