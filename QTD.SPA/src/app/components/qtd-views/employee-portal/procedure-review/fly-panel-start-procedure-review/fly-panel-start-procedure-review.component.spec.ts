import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelStartProcedureReviewComponent } from './fly-panel-start-procedure-review.component';

describe('FlyPanelStartProcedureReviewComponent', () => {
  let component: FlyPanelStartProcedureReviewComponent;
  let fixture: ComponentFixture<FlyPanelStartProcedureReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelStartProcedureReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelStartProcedureReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
