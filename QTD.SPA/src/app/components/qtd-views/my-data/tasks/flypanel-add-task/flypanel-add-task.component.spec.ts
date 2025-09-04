import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAddTaskComponent } from './flypanel-add-task.component';

describe('FlypanelAddTaskComponent', () => {
  let component: FlypanelAddTaskComponent;
  let fixture: ComponentFixture<FlypanelAddTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAddTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAddTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
