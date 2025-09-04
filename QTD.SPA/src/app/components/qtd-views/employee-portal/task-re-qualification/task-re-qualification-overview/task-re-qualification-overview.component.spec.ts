import { ComponentFixture, TestBed } from '@angular/core/testing';

import { qualification } from './task-re-qualification-overview.component';

describe('TaskReQualificationOverviewComponent', () => {
  let component: qualification;
  let fixture: ComponentFixture<qualification>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ qualification ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(qualification);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
