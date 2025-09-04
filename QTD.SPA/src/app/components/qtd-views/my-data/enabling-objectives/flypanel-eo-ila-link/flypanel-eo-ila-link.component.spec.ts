import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoIlaLinkComponent } from './flypanel-eo-ila-link.component';

describe('FlypanelEoIlaLinkComponent', () => {
  let component: FlypanelEoIlaLinkComponent;
  let fixture: ComponentFixture<FlypanelEoIlaLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoIlaLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoIlaLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
