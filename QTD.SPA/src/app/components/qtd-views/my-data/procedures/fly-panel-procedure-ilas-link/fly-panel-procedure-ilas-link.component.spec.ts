import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProcedureIlasLinkComponent } from './fly-panel-procedure-ilas-link.component';

describe('FlyPanelProcedureIlasLinkComponent', () => {
  let component: FlyPanelProcedureIlasLinkComponent;
  let fixture: ComponentFixture<FlyPanelProcedureIlasLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProcedureIlasLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProcedureIlasLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
