import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelMetaEoLinkComponent } from './flypanel-meta-eo-link.component';

describe('FlypanelMetaEoLinkComponent', () => {
  let component: FlypanelMetaEoLinkComponent;
  let fixture: ComponentFixture<FlypanelMetaEoLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelMetaEoLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelMetaEoLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
