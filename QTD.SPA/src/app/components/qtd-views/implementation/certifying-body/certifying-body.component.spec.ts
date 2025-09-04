import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CertifyingBodyComponent } from './certifying-body.component';

describe('CertifyingBodyComponent', () => {
  let component: CertifyingBodyComponent;
  let fixture: ComponentFixture<CertifyingBodyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CertifyingBodyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CertifyingBodyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
