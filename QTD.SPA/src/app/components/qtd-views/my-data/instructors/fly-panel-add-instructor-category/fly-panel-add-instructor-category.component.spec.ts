import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddInstructorCategoryComponent } from './fly-panel-add-instructor-category.component';

describe('FlyPanelAddInstructorCategoryComponent', () => {
  let component: FlyPanelAddInstructorCategoryComponent;
  let fixture: ComponentFixture<FlyPanelAddInstructorCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddInstructorCategoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddInstructorCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
