import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssueActionItemsTableComponent } from './training-issue-action-items-table.component';

describe('TrainingIssueActionItemsTableComponent', () => {
  let component: TrainingIssueActionItemsTableComponent;
  let fixture: ComponentFixture<TrainingIssueActionItemsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssueActionItemsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssueActionItemsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
