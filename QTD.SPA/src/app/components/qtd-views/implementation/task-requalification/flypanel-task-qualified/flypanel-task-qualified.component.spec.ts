import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelTaskQualifiedComponent } from './flypanel-task-qualified.component';

describe('FlypanelTaskQualifiedComponent', () => {
  let component: FlypanelTaskQualifiedComponent;
  let fixture: ComponentFixture<FlypanelTaskQualifiedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelTaskQualifiedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelTaskQualifiedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
