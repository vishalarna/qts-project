import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CertificationNavbarComponent } from './certification-navbar.component';

describe('CertificationNavbarComponent', () => {
  let component: CertificationNavbarComponent;
  let fixture: ComponentFixture<CertificationNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CertificationNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CertificationNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
