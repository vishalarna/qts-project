import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelTaskRequalFilterComponent } from './flypanel-task-requal-filter.component';

describe('FlypanelTaskRequalFilterComponent', () => {
  let component: FlypanelTaskRequalFilterComponent;
  let fixture: ComponentFixture<FlypanelTaskRequalFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelTaskRequalFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelTaskRequalFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
