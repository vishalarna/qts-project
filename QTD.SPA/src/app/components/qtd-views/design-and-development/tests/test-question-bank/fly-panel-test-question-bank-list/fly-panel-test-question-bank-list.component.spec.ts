import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTestQuestionBankListComponent } from './fly-panel-test-question-bank-list.component';

describe('FlyPanelTestQuestionBankListComponent', () => {
  let component: FlyPanelTestQuestionBankListComponent;
  let fixture: ComponentFixture<FlyPanelTestQuestionBankListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTestQuestionBankListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTestQuestionBankListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
