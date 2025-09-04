import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelSuggestionOperationComponent } from './flypanel-suggestion-operation.component';

describe('FlypanelSuggestionOperationComponent', () => {
  let component: FlypanelSuggestionOperationComponent;
  let fixture: ComponentFixture<FlypanelSuggestionOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelSuggestionOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelSuggestionOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
