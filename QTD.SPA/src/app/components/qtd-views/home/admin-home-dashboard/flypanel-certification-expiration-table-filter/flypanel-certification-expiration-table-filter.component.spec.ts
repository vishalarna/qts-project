import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelCertificationExpirationTableFilterComponent } from './flypanel-certification-expiration-table-filter.component';

describe('FlypanelCertificationExpirationTableFilterComponent', () => {
  let component: FlypanelCertificationExpirationTableFilterComponent;
  let fixture: ComponentFixture<FlypanelCertificationExpirationTableFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelCertificationExpirationTableFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelCertificationExpirationTableFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
