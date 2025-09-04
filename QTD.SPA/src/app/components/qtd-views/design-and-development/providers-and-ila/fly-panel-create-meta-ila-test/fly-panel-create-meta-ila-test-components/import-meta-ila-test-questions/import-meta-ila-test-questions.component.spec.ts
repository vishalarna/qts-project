import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportMetaILATestQuestionsComponent } from './import-meta-ila-test-questions.component';

describe('ImportMetaILATestQuestionsComponent', () => {
  let component: ImportMetaILATestQuestionsComponent;
  let fixture: ComponentFixture<ImportMetaILATestQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportMetaILATestQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportMetaILATestQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
