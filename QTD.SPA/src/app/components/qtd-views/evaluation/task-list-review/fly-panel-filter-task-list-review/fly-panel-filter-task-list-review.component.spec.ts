import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelFilterTaskListReviewComponent } from './fly-panel-filter-task-list-review.component';

describe('FlyPanelFilterTaskListReviewComponent', () => {
  let component: FlyPanelFilterTaskListReviewComponent;
  let fixture: ComponentFixture<FlyPanelFilterTaskListReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelFilterTaskListReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelFilterTaskListReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
