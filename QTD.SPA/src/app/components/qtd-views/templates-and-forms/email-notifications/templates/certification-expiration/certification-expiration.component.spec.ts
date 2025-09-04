import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CertificationExpirationComponent } from './certification-expiration.component';

describe('CertificationExpirationComponent', () => {
  let component: CertificationExpirationComponent;
  let fixture: ComponentFixture<CertificationExpirationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CertificationExpirationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CertificationExpirationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
