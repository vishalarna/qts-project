import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRenewCertificationComponent } from './fly-panel-renew-certification.component';

describe('FlyPanelRenewCertificationComponent', () => {
  let component: FlyPanelRenewCertificationComponent;
  let fixture: ComponentFixture<FlyPanelRenewCertificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRenewCertificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRenewCertificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
