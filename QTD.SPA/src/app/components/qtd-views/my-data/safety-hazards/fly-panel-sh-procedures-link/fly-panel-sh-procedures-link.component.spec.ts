import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelShProceduresLinkComponent } from './fly-panel-sh-procedures-link.component';

describe('FlyPanelShProceduresLinkComponent', () => {
  let component: FlyPanelShProceduresLinkComponent;
  let fixture: ComponentFixture<FlyPanelShProceduresLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelShProceduresLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelShProceduresLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
