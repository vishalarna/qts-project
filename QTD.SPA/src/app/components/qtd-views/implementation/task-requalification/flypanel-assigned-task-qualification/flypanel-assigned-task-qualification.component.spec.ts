import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAssignedTaskQualificationComponent } from './flypanel-assigned-task-qualification.component';

describe('FlypanelAssignedTaskQualificationComponent', () => {
  let component: FlypanelAssignedTaskQualificationComponent;
  let fixture: ComponentFixture<FlypanelAssignedTaskQualificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAssignedTaskQualificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAssignedTaskQualificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
