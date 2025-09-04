import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitTestDialogueComponent } from './submit-test-dialogue.component';

describe('SubmitTestDialogueComponent', () => {
  let component: SubmitTestDialogueComponent;
  let fixture: ComponentFixture<SubmitTestDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmitTestDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitTestDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
