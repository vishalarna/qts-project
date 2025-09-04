import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelSubdutyOperationComponent } from './flypanel-subduty-operation.component';

describe('FlypanelSubdutyOperationComponent', () => {
  let component: FlypanelSubdutyOperationComponent;
  let fixture: ComponentFixture<FlypanelSubdutyOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelSubdutyOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelSubdutyOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
