import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTrueFalseComponent } from './add-true-false.component';

describe('AddTrueFalseComponent', () => {
  let component: AddTrueFalseComponent;
  let fixture: ComponentFixture<AddTrueFalseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTrueFalseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTrueFalseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
