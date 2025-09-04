import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProcedureEosLinkComponent } from './fly-panel-procedure-eos-link.component';

describe('FlyPanelProcedureEosLinkComponent', () => {
  let component: FlyPanelProcedureEosLinkComponent;
  let fixture: ComponentFixture<FlyPanelProcedureEosLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProcedureEosLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProcedureEosLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
