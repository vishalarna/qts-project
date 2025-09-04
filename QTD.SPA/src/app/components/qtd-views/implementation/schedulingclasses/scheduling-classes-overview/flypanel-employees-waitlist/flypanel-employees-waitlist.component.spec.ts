import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEmployeesWaitlistComponent } from './flypanel-employees-waitlist.component';

describe('FlypanelEmployeesWaitlistComponent', () => {
  let component: FlypanelEmployeesWaitlistComponent;
  let fixture: ComponentFixture<FlypanelEmployeesWaitlistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEmployeesWaitlistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEmployeesWaitlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
