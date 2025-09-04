import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollEmployeesDateDialogueComponent } from './enroll-employees-date-dialogue.component';

describe('EnrollEmployeesDateDialogueComponent', () => {
  let component: EnrollEmployeesDateDialogueComponent;
  let fixture: ComponentFixture<EnrollEmployeesDateDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrollEmployeesDateDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrollEmployeesDateDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
