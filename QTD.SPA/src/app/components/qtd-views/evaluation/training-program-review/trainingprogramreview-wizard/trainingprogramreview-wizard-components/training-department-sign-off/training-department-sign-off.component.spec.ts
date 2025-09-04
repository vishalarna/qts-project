import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingDepartmentSignOffComponent } from './training-department-sign-off.component';

describe('TrainingDepartmentSignOffComponent', () => {
  let component: TrainingDepartmentSignOffComponent;
  let fixture: ComponentFixture<TrainingDepartmentSignOffComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingDepartmentSignOffComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingDepartmentSignOffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
