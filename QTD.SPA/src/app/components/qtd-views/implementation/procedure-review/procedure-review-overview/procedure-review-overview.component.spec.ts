import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureReviewOverviewComponent } from './procedure-review-overview.component';

describe('ProcedureReviewOverviewComponent', () => {
  let component: ProcedureReviewOverviewComponent;
  let fixture: ComponentFixture<ProcedureReviewOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureReviewOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureReviewOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
