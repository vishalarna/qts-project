import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelViewInstructorHistoryComponent } from './fly-panel-view-instructor-history.component';

describe('FlyPanelViewInstructorHistoryComponent', () => {
  let component: FlyPanelViewInstructorHistoryComponent;
  let fixture: ComponentFixture<FlyPanelViewInstructorHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelViewInstructorHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelViewInstructorHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
