import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskReQualificationSuggestionsComponent } from './fly-panel-task-re-qualification-suggestions.component';

describe('FlyPanelTaskReQualificationSuggestionsComponent', () => {
  let component: FlyPanelTaskReQualificationSuggestionsComponent;
  let fixture: ComponentFixture<FlyPanelTaskReQualificationSuggestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskReQualificationSuggestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskReQualificationSuggestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
