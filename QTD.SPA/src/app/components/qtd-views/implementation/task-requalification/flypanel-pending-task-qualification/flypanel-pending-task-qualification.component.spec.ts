import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelPendingTaskQualificationComponent } from './flypanel-pending-task-qualification.component';

describe('FlypanelPendingTaskQualificationComponent', () => {
  let component: FlypanelPendingTaskQualificationComponent;
  let fixture: ComponentFixture<FlypanelPendingTaskQualificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelPendingTaskQualificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelPendingTaskQualificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
