import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFillInTheBlankComponent } from './add-fill-in-the-blank.component';

describe('AddFillInTheBlankComponent', () => {
  let component: AddFillInTheBlankComponent;
  let fixture: ComponentFixture<AddFillInTheBlankComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddFillInTheBlankComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddFillInTheBlankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
