import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstructorCategoryDetailsComponent } from './instructor-category-details.component';

describe('InstructorCategoryDetailsComponent', () => {
  let component: InstructorCategoryDetailsComponent;
  let fixture: ComponentFixture<InstructorCategoryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstructorCategoryDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstructorCategoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
