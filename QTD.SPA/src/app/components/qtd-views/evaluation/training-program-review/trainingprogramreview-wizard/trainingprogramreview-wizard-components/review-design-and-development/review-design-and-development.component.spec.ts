import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewDesignAndDevelopmentComponent } from './review-design-and-development.component';

describe('ReviewDesignAndDevelopmentComponent', () => {
  let component: ReviewDesignAndDevelopmentComponent;
  let fixture: ComponentFixture<ReviewDesignAndDevelopmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReviewDesignAndDevelopmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewDesignAndDevelopmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
