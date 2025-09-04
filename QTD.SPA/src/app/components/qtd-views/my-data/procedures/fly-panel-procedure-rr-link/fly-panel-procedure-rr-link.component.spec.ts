import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProcedureRrLinkComponent } from './fly-panel-procedure-rr-link.component';

describe('FlyPanelProcedureRrLinkComponent', () => {
  let component: FlyPanelProcedureRrLinkComponent;
  let fixture: ComponentFixture<FlyPanelProcedureRrLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProcedureRrLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProcedureRrLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
