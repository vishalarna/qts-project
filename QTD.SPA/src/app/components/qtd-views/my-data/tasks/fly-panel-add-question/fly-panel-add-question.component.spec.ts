import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddQuestionComponent } from './fly-panel-add-question.component';

describe('FlyPanelAddQuestionComponent', () => {
  let component: FlyPanelAddQuestionComponent;
  let fixture: ComponentFixture<FlyPanelAddQuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddQuestionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
