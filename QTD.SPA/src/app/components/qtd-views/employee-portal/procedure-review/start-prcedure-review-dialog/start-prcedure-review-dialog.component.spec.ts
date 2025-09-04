import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartPrcedureReviewDialogComponent } from './start-prcedure-review-dialog.component';

describe('StartPrcedureReviewDialogComponent', () => {
  let component: StartPrcedureReviewDialogComponent;
  let fixture: ComponentFixture<StartPrcedureReviewDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartPrcedureReviewDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StartPrcedureReviewDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
