import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectUnlinkedQuestionsComponent } from './select-unlinked-questions.component';

describe('SelectUnlinkedQuestionsComponent', () => {
  let component: SelectUnlinkedQuestionsComponent;
  let fixture: ComponentFixture<SelectUnlinkedQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectUnlinkedQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectUnlinkedQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
