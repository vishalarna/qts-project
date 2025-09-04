import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoAddSuggestionComponent } from './flypanel-eo-add-suggestion.component';

describe('FlypanelEoAddSuggestionComponent', () => {
  let component: FlypanelEoAddSuggestionComponent;
  let fixture: ComponentFixture<FlypanelEoAddSuggestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoAddSuggestionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoAddSuggestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
