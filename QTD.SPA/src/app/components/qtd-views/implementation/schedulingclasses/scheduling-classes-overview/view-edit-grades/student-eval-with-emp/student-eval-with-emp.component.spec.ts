import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentEvalWithEmpComponent } from './student-eval-with-emp.component';

describe('StudentEvalWithEmpComponent', () => {
  let component: StudentEvalWithEmpComponent;
  let fixture: ComponentFixture<StudentEvalWithEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentEvalWithEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentEvalWithEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
