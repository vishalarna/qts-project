import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureReviewComponent } from './procedure-review.component';

describe('ProcedureReviewComponent', () => {
  let component: ProcedureReviewComponent;
  let fixture: ComponentFixture<ProcedureReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
