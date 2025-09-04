import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollmentDateDialogueComponent } from './enrollment-date-dialogue.component';

describe('EnrollmentDateDialogueComponent', () => {
  let component: EnrollmentDateDialogueComponent;
  let fixture: ComponentFixture<EnrollmentDateDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrollmentDateDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrollmentDateDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
