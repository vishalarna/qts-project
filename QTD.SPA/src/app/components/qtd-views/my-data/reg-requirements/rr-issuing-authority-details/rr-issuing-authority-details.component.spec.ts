import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RrIssuingAuthorityDetailsComponent } from './rr-issuing-authority-details.component';

describe('RrIssuingAuthorityDetailsComponent', () => {
  let component: RrIssuingAuthorityDetailsComponent;
  let fixture: ComponentFixture<RrIssuingAuthorityDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RrIssuingAuthorityDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RrIssuingAuthorityDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
