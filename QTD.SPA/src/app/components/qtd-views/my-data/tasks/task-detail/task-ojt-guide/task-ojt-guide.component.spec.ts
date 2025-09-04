import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskOjtGuideComponent } from './task-ojt-guide.component';

describe('TaskOjtGuideComponent', () => {
  let component: TaskOjtGuideComponent;
  let fixture: ComponentFixture<TaskOjtGuideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskOjtGuideComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskOjtGuideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
