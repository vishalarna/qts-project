import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEnrollmentComponent } from './view-enrollment.component';

describe('ViewEnrollmentComponent', () => {
  let component: ViewEnrollmentComponent;
  let fixture: ComponentFixture<ViewEnrollmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewEnrollmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewEnrollmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
