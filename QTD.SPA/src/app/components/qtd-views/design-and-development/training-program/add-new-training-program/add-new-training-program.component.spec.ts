import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewTrainingProgramComponent } from './add-new-training-program.component';

describe('AddNewTrainingProgramComponent', () => {
  let component: AddNewTrainingProgramComponent;
  let fixture: ComponentFixture<AddNewTrainingProgramComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNewTrainingProgramComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNewTrainingProgramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
