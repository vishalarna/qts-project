import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewStudentEvaluationComponent } from './add-new-student-evaluation.component';

describe('AddNewStudentEvaluationComponent', () => {
  let component: AddNewStudentEvaluationComponent;
  let fixture: ComponentFixture<AddNewStudentEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNewStudentEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNewStudentEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
