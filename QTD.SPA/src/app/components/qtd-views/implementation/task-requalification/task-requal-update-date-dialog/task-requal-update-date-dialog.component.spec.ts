import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRequalUpdateDateDialogComponent } from './task-requal-update-date-dialog.component';

describe('TaskRequalUpdateDateDialogComponent', () => {
  let component: TaskRequalUpdateDateDialogComponent;
  let fixture: ComponentFixture<TaskRequalUpdateDateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRequalUpdateDateDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRequalUpdateDateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
