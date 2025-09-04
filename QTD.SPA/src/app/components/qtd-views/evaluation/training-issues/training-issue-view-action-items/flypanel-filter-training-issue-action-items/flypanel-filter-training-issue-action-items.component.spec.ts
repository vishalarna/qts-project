import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelFilterTrainingIssueActionItemsComponent } from './flypanel-filter-training-issue-action-items.component';

describe('FlypanelFilterTrainingIssueActionItemsComponent', () => {
  let component: FlypanelFilterTrainingIssueActionItemsComponent;
  let fixture: ComponentFixture<FlypanelFilterTrainingIssueActionItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelFilterTrainingIssueActionItemsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelFilterTrainingIssueActionItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
