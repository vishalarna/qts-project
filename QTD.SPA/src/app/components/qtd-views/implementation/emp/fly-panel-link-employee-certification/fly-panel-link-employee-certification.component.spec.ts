import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkEmployeeCertificationComponent } from './fly-panel-link-employee-certification.component';

describe('FlyPanelLinkEmployeeCertificationComponent', () => {
  let component: FlyPanelLinkEmployeeCertificationComponent;
  let fixture: ComponentFixture<FlyPanelLinkEmployeeCertificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkEmployeeCertificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkEmployeeCertificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
