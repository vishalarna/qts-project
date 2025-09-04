import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoProcedureLinkComponent } from './flypanel-eo-procedure-link.component';

describe('FlypanelEoProcedureLinkComponent', () => {
  let component: FlypanelEoProcedureLinkComponent;
  let fixture: ComponentFixture<FlypanelEoProcedureLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoProcedureLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoProcedureLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
