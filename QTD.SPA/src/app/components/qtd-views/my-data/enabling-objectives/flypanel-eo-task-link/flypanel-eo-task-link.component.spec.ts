import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoTaskLinkComponent } from './flypanel-eo-task-link.component';

describe('FlypanelEoTaskLinkComponent', () => {
  let component: FlypanelEoTaskLinkComponent;
  let fixture: ComponentFixture<FlypanelEoTaskLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoTaskLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoTaskLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
