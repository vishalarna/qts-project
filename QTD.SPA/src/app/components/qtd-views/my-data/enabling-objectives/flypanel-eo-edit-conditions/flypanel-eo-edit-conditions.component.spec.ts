import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoEditConditionsComponent } from './flypanel-eo-edit-conditions.component';

describe('FlypanelEoEditConditionsComponent', () => {
  let component: FlypanelEoEditConditionsComponent;
  let fixture: ComponentFixture<FlypanelEoEditConditionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoEditConditionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoEditConditionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
