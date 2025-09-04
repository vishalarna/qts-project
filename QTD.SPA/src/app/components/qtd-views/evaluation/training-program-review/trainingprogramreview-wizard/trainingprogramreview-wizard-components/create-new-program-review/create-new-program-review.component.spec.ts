import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateNewProgramReviewComponent } from './create-new-program-review.component';

describe('CreateNewProgramReview', () => {
  let component: CreateNewProgramReviewComponent;
  let fixture: ComponentFixture<CreateNewProgramReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateNewProgramReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateNewProgramReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
