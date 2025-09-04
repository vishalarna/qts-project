import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoEmployeeLinkComponent } from './flypanel-eo-employee-link.component';

describe('FlypanelEoEmployeeLinkComponent', () => {
  let component: FlypanelEoEmployeeLinkComponent;
  let fixture: ComponentFixture<FlypanelEoEmployeeLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoEmployeeLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoEmployeeLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
