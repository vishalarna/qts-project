import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IdpReviewComponent } from './idp-review.component';

describe('IdpReviewComponent', () => {
  let component: IdpReviewComponent;
  let fixture: ComponentFixture<IdpReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IdpReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IdpReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
