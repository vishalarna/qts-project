import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelFilterEmployeeTaskQualificationComponent } from './flypanel-filter-employee-task-qualification.component';

describe('FlypanelFilterEmployeeTaskQualificationComponent', () => {
  let component: FlypanelFilterEmployeeTaskQualificationComponent;
  let fixture: ComponentFixture<FlypanelFilterEmployeeTaskQualificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelFilterEmployeeTaskQualificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelFilterEmployeeTaskQualificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
