import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelTaskRequalNeededComponent } from './flypanel-task-requal-needed.component';

describe('FlypanelTaskRequalNeededComponent', () => {
  let component: FlypanelTaskRequalNeededComponent;
  let fixture: ComponentFixture<FlypanelTaskRequalNeededComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelTaskRequalNeededComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelTaskRequalNeededComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
