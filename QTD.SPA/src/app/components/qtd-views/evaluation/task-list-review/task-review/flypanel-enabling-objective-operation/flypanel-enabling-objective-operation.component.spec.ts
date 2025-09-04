import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEnablingObjectiveOperationComponent } from './flypanel-enabling-objective-operation.component';

describe('FlypanelEnablingObjectiveOperationComponent', () => {
  let component: FlypanelEnablingObjectiveOperationComponent;
  let fixture: ComponentFixture<FlypanelEnablingObjectiveOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEnablingObjectiveOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEnablingObjectiveOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
