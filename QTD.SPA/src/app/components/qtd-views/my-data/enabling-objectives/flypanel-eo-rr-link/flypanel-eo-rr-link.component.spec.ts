import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoRrLinkComponent } from './flypanel-eo-rr-link.component';

describe('FlypanelEoRrLinkComponent', () => {
  let component: FlypanelEoRrLinkComponent;
  let fixture: ComponentFixture<FlypanelEoRrLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoRrLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoRrLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
