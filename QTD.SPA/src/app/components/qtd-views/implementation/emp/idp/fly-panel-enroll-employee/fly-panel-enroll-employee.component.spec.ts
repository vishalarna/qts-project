import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEnrollEmployeeComponent } from './fly-panel-enroll-employee.component';

describe('FlyPanelEnrollEmployeeComponent', () => {
  let component: FlyPanelEnrollEmployeeComponent;
  let fixture: ComponentFixture<FlyPanelEnrollEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEnrollEmployeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEnrollEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
