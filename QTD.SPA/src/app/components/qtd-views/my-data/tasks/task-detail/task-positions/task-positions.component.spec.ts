import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskPositionsComponent } from './task-positions.component';

describe('TaskPositionsComponent', () => {
  let component: TaskPositionsComponent;
  let fixture: ComponentFixture<TaskPositionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskPositionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskPositionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
