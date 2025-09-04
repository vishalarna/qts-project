import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentEvalWithoutEmpComponent } from './student-eval-without-emp.component';

describe('StudentEvalWithoutEmpComponent', () => {
  let component: StudentEvalWithoutEmpComponent;
  let fixture: ComponentFixture<StudentEvalWithoutEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentEvalWithoutEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentEvalWithoutEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
