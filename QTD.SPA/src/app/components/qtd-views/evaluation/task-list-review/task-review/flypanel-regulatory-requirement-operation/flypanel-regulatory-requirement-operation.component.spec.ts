import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelRegulatoryRequirementOperationComponent } from './flypanel-regulatory-requirement-operation.component';

describe('FlypanelRegulatoryRequirementOperationComponent', () => {
  let component: FlypanelRegulatoryRequirementOperationComponent;
  let fixture: ComponentFixture<FlypanelRegulatoryRequirementOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelRegulatoryRequirementOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelRegulatoryRequirementOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
