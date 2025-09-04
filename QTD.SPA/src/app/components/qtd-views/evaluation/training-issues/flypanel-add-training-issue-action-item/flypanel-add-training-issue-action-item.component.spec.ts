import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAddTrainingIssueActionItemComponent } from './flypanel-add-training-issue-action-item.component';

describe('FlypanelAddTrainingIssueActionItemComponent', () => {
  let component: FlypanelAddTrainingIssueActionItemComponent;
  let fixture: ComponentFixture<FlypanelAddTrainingIssueActionItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAddTrainingIssueActionItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAddTrainingIssueActionItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
