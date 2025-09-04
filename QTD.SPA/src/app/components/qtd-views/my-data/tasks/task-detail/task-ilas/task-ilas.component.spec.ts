import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskIlasComponent } from './task-ilas.component';

describe('TaskIlasComponent', () => {
  let component: TaskIlasComponent;
  let fixture: ComponentFixture<TaskIlasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskIlasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskIlasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
