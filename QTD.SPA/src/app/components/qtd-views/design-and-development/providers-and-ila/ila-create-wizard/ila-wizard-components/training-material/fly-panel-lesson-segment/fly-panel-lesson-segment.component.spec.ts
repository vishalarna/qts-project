import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLessonSegmentComponent } from './fly-panel-lesson-segment.component';

describe('FlyPanelLessonSegmentComponent', () => {
  let component: FlyPanelLessonSegmentComponent;
  let fixture: ComponentFixture<FlyPanelLessonSegmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLessonSegmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLessonSegmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
