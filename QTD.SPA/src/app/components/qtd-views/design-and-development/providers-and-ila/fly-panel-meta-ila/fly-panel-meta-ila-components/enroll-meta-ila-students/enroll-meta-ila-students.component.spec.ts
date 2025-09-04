import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollMetaILAStudentsComponent } from './enroll-meta-ila-students.component';

describe('EnrollMetaILAStudentsComponent', () => {
  let component: EnrollMetaILAStudentsComponent;
  let fixture: ComponentFixture<EnrollMetaILAStudentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrollMetaILAStudentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrollMetaILAStudentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
