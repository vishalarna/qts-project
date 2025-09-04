import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CertificationOverviewComponent } from './certification-overview.component';

describe('CertificationOverviewComponent', () => {
  let component: CertificationOverviewComponent;
  let fixture: ComponentFixture<CertificationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CertificationOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CertificationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
