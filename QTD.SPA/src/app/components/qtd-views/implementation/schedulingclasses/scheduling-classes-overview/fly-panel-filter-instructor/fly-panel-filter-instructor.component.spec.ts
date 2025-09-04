import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelFilterInstructorComponent } from './fly-panel-filter-instructor.component';

describe('FlyPanelFilterInstructorComponent', () => {
  let component: FlyPanelFilterInstructorComponent;
  let fixture: ComponentFixture<FlyPanelFilterInstructorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelFilterInstructorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelFilterInstructorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
