import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureReviewDetailComponent } from './procedure-review-detail.component';

describe('ProcedureReviewDetailComponent', () => {
  let component: ProcedureReviewDetailComponent;
  let fixture: ComponentFixture<ProcedureReviewDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureReviewDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureReviewDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
