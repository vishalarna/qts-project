import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddSuggestionComponent } from './fly-panel-add-suggestion.component';

describe('FlyPanelAddSuggestionComponent', () => {
  let component: FlyPanelAddSuggestionComponent;
  let fixture: ComponentFixture<FlyPanelAddSuggestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddSuggestionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddSuggestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
