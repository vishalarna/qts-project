import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRegulatoryRequirementsComponent } from './task-regulatory-requirements.component';

describe('TaskRegulatoryRequirementsComponent', () => {
  let component: TaskRegulatoryRequirementsComponent;
  let fixture: ComponentFixture<TaskRegulatoryRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRegulatoryRequirementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRegulatoryRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
