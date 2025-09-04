import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskCollaborateModalComponent } from './task-collaborate-modal.component';

describe('TaskCollaborateModalComponent', () => {
  let component: TaskCollaborateModalComponent;
  let fixture: ComponentFixture<TaskCollaborateModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskCollaborateModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskCollaborateModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
