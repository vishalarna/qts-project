import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelImportExistingQuestionsComponent } from './fly-panel-import-existing-questions.component';

describe('FlyPanelImportExistingQuestionsComponent', () => {
  let component: FlyPanelImportExistingQuestionsComponent;
  let fixture: ComponentFixture<FlyPanelImportExistingQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelImportExistingQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelImportExistingQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
