import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchedulingclassesComponent } from './schedulingclasses.component';

describe('SchedulingclassesComponent', () => {
  let component: SchedulingclassesComponent;
  let fixture: ComponentFixture<SchedulingclassesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchedulingclassesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchedulingclassesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
