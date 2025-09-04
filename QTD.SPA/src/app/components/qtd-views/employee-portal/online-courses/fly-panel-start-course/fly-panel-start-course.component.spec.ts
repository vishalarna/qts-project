import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelStartCourseComponent } from './fly-panel-start-course.component';

describe('FlyPanelStartCourseComponent', () => {
  let component: FlyPanelStartCourseComponent;
  let fixture: ComponentFixture<FlyPanelStartCourseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelStartCourseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelStartCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
