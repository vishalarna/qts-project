import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchedulingClassesOverviewComponent } from './scheduling-classes-overview.component';

describe('SchedulingClassesOverviewComponent', () => {
  let component: SchedulingClassesOverviewComponent;
  let fixture: ComponentFixture<SchedulingClassesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchedulingClassesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchedulingClassesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
