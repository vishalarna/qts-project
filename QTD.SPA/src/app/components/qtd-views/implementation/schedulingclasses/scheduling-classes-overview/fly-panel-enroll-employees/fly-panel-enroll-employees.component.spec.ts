import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEnrollEmployeesComponent } from './fly-panel-enroll-employees.component';

describe('FlyPanelEnrollEmployeesComponent', () => {
  let component: FlyPanelEnrollEmployeesComponent;
  let fixture: ComponentFixture<FlyPanelEnrollEmployeesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEnrollEmployeesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEnrollEmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
