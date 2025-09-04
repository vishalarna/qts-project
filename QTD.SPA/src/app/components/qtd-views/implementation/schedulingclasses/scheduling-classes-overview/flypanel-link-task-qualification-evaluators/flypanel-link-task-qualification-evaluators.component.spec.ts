import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelLinkTaskQualificationEvaluatorsComponent } from './flypanel-link-task-qualification-evaluators.component';

describe('FlypanelLinkTaskQualificationEvaluatorsComponent', () => {
  let component: FlypanelLinkTaskQualificationEvaluatorsComponent;
  let fixture: ComponentFixture<FlypanelLinkTaskQualificationEvaluatorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelLinkTaskQualificationEvaluatorsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelLinkTaskQualificationEvaluatorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
