import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelViewCourseInfoComponent } from './flypanel-view-course-info.component';

describe('FlypanelViewCourseInfoComponent', () => {
  let component: FlypanelViewCourseInfoComponent;
  let fixture: ComponentFixture<FlypanelViewCourseInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelViewCourseInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelViewCourseInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
