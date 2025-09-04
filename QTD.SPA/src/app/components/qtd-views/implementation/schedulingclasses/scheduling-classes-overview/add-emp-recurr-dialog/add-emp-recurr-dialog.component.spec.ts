import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEmpRecurrDialogComponent } from './add-emp-recurr-dialog.component';

describe('AddEmpRecurrDialogComponent', () => {
  let component: AddEmpRecurrDialogComponent;
  let fixture: ComponentFixture<AddEmpRecurrDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEmpRecurrDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEmpRecurrDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
