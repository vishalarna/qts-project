import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelToolOperationComponent } from './flypanel-tool-operation.component';

describe('FlypanelToolOperationComponent', () => {
  let component: FlypanelToolOperationComponent;
  let fixture: ComponentFixture<FlypanelToolOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelToolOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelToolOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
