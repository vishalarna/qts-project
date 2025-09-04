import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskMetaOjtGuideComponent } from './task-meta-ojt-guide.component';

describe('TaskMetaOjtGuideComponent', () => {
  let component: TaskMetaOjtGuideComponent;
  let fixture: ComponentFixture<TaskMetaOjtGuideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskMetaOjtGuideComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskMetaOjtGuideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
