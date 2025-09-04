import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddProcedureReviewComponent } from './fly-panel-add-procedure-review.component';

describe('FlyPanelAddProcedureReviewComponent', () => {
  let component: FlyPanelAddProcedureReviewComponent;
  let fixture: ComponentFixture<FlyPanelAddProcedureReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddProcedureReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddProcedureReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
