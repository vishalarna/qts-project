import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifReviewAndPublishComponent } from './dif-review-and-publish.component';

describe('DifReviewAndPublishComponent', () => {
  let component: DifReviewAndPublishComponent;
  let fixture: ComponentFixture<DifReviewAndPublishComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifReviewAndPublishComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifReviewAndPublishComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
