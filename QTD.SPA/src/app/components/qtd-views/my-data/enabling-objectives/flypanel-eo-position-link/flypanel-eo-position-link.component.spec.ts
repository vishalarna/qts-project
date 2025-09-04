import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoPositionLinkComponent } from './flypanel-eo-position-link.component';

describe('FlypanelEoPositionLinkComponent', () => {
  let component: FlypanelEoPositionLinkComponent;
  let fixture: ComponentFixture<FlypanelEoPositionLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoPositionLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoPositionLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
