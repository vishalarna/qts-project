import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelRetakeStatusComponent } from './flypanel-retake-status.component';

describe('FlypanelRetakeStatusComponent', () => {
  let component: FlypanelRetakeStatusComponent;
  let fixture: ComponentFixture<FlypanelRetakeStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelRetakeStatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelRetakeStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
