import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelResultProcedureReviewComponent } from './fly-panel-result-procedure-review.component';

describe('FlyPanelResultProcedureReviewComponent', () => {
  let component: FlyPanelResultProcedureReviewComponent;
  let fixture: ComponentFixture<FlyPanelResultProcedureReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelResultProcedureReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelResultProcedureReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
