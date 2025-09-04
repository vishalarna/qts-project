import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssueViewActionItemsComponent } from './training-issue-view-action-items.component';

describe('TrainingIssueViewActionItemsComponent', () => {
  let component: TrainingIssueViewActionItemsComponent;
  let fixture: ComponentFixture<TrainingIssueViewActionItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssueViewActionItemsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssueViewActionItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
