import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPublishedProcedureReviewComponent } from './fly-panel-published-procedure-review.component';

describe('FlyPanelPublishedProcedureReviewComponent', () => {
  let component: FlyPanelPublishedProcedureReviewComponent;
  let fixture: ComponentFixture<FlyPanelPublishedProcedureReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPublishedProcedureReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPublishedProcedureReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
