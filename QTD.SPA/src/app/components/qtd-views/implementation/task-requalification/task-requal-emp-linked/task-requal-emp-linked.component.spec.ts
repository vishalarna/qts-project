import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRequalEmpLinkedComponent } from './task-requal-emp-linked.component';

describe('TaskRequalEmpLinkedComponent', () => {
  let component: TaskRequalEmpLinkedComponent;
  let fixture: ComponentFixture<TaskRequalEmpLinkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRequalEmpLinkedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRequalEmpLinkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
