import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskNavBarComponent } from './task-nav-bar.component';

describe('TaskNavBarComponent', () => {
  let component: TaskNavBarComponent;
  let fixture: ComponentFixture<TaskNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
