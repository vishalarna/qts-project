import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelTaskRequalFilterByEmpComponent } from './flypanel-task-requal-filter-by-emp.component';

describe('FlypanelTaskRequalFilterByEmpComponent', () => {
  let component: FlypanelTaskRequalFilterByEmpComponent;
  let fixture: ComponentFixture<FlypanelTaskRequalFilterByEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelTaskRequalFilterByEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelTaskRequalFilterByEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
