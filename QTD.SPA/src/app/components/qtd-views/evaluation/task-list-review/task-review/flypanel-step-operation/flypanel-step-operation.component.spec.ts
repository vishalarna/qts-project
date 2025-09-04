import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelStepOperationComponent } from './flypanel-step-operation.component';

describe('FlypanelStepOperationComponent', () => {
  let component: FlypanelStepOperationComponent;
  let fixture: ComponentFixture<FlypanelStepOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelStepOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelStepOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
