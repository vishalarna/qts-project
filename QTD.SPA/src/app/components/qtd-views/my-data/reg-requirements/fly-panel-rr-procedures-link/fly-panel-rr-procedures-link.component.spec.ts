import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRrProceduresLinkComponent } from './fly-panel-rr-procedures-link.component';

describe('FlyPanelRrProceduresLinkComponent', () => {
  let component: FlyPanelRrProceduresLinkComponent;
  let fixture: ComponentFixture<FlyPanelRrProceduresLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRrProceduresLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRrProceduresLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
