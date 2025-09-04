import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoToolLinkComponent } from './flypanel-eo-tool-link.component';

describe('FlypanelEoToolLinkComponent', () => {
  let component: FlypanelEoToolLinkComponent;
  let fixture: ComponentFixture<FlypanelEoToolLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoToolLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoToolLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
