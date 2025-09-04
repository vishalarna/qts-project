import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelprocedureReviewDraftComponent } from './fly-panelprocedure-review-draft.component';

describe('FlyPanelprocedureReviewDraftComponent', () => {
  let component: FlyPanelprocedureReviewDraftComponent;
  let fixture: ComponentFixture<FlyPanelprocedureReviewDraftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelprocedureReviewDraftComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelprocedureReviewDraftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
