import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTrainingIssuesPendingActionItemComponent } from './fly-panel-training-issues-pending-action-item.component';

describe('FlyPanelTrainingIssuesPendingActionItemComponent', () => {
  let component: FlyPanelTrainingIssuesPendingActionItemComponent;
  let fixture: ComponentFixture<FlyPanelTrainingIssuesPendingActionItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTrainingIssuesPendingActionItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTrainingIssuesPendingActionItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
