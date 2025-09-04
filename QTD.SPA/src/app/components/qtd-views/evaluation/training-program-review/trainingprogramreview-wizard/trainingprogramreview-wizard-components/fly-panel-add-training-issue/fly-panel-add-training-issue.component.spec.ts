import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddTrainingIssueComponent } from './fly-panel-add-training-issue.component';

describe('FlyPanelAddTrainingIssueComponent', () => {
  let component: FlyPanelAddTrainingIssueComponent;
  let fixture: ComponentFixture<FlyPanelAddTrainingIssueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddTrainingIssueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddTrainingIssueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
