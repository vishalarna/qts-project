import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurposeOfReviewComponent } from './purpose-of-review.component';

describe('CreateNewProgramReview', () => {
  let component: PurposeOfReviewComponent;
  let fixture: ComponentFixture<PurposeOfReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurposeOfReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PurposeOfReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
