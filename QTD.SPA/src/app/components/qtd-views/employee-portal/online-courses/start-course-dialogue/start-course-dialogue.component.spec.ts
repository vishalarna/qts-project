import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartCourseDialogueComponent } from './start-course-dialogue.component';

describe('StartCourseDialogueComponent', () => {
  let component: StartCourseDialogueComponent;
  let fixture: ComponentFixture<StartCourseDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartCourseDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StartCourseDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
