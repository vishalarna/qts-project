import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelWithoutTasksQualificationsComponent } from './flypanel-without-tasks-qualifications.component';

describe('FlypanelWithoutTasksQualificationsComponent', () => {
  let component: FlypanelWithoutTasksQualificationsComponent;
  let fixture: ComponentFixture<FlypanelWithoutTasksQualificationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelWithoutTasksQualificationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelWithoutTasksQualificationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
