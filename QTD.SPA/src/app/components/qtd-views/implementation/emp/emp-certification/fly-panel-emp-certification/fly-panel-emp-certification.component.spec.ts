import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEmpCertificationComponent } from './fly-panel-emp-certification.component';

describe('FlyPanelEmpCertificationComponent', () => {
  let component: FlyPanelEmpCertificationComponent;
  let fixture: ComponentFixture<FlyPanelEmpCertificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEmpCertificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEmpCertificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
