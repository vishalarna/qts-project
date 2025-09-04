import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAddTaskQualificationComponent } from './flypanel-add-task-qualification.component';

describe('FlypanelAddTaskQualificationComponent', () => {
  let component: FlypanelAddTaskQualificationComponent;
  let fixture: ComponentFixture<FlypanelAddTaskQualificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAddTaskQualificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAddTaskQualificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
