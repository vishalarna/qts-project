import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddInstructorComponent } from './fly-panel-add-instructor.component';

describe('FlyPanelAddInstructorComponent', () => {
  let component: FlyPanelAddInstructorComponent;
  let fixture: ComponentFixture<FlyPanelAddInstructorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddInstructorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddInstructorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
