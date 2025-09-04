import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RosterRetakeComponent } from './roster-retake.component';

describe('RosterRetakeComponent', () => {
  let component: RosterRetakeComponent;
  let fixture: ComponentFixture<RosterRetakeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RosterRetakeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RosterRetakeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
