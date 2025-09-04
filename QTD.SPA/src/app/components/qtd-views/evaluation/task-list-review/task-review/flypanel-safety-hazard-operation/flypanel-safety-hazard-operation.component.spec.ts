import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelSafetyHazardOperationComponent } from './flypanel-safety-hazard-operation.component';

describe('FlypanelSafetyHazardOperationComponent', () => {
  let component: FlypanelSafetyHazardOperationComponent;
  let fixture: ComponentFixture<FlypanelSafetyHazardOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelSafetyHazardOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelSafetyHazardOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
