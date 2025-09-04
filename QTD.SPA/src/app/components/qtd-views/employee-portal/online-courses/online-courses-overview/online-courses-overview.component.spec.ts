import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnlineCoursesOverviewComponent } from './online-courses-overview.component';

describe('OnlineCoursesOverviewComponent', () => {
  let component: OnlineCoursesOverviewComponent;
  let fixture: ComponentFixture<OnlineCoursesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OnlineCoursesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OnlineCoursesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
