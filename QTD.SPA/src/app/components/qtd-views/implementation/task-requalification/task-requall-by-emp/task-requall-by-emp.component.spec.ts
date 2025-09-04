import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRequallByEmpComponent } from './task-requall-by-emp.component';

describe('TaskRequallByEmpComponent', () => {
  let component: TaskRequallByEmpComponent;
  let fixture: ComponentFixture<TaskRequallByEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRequallByEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRequallByEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
