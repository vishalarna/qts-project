import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveInactiveDialogueComponent } from './active-inactive-dialogue.component';

describe('ActiveInactiveDialogueComponent', () => {
  let component: ActiveInactiveDialogueComponent;
  let fixture: ComponentFixture<ActiveInactiveDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActiveInactiveDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ActiveInactiveDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
