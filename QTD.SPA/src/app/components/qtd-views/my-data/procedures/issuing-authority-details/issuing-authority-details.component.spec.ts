import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IssuingAuthorityDetailsComponent } from './issuing-authority-details.component';

describe('IssuingAuthorityDetailsComponent', () => {
  let component: IssuingAuthorityDetailsComponent;
  let fixture: ComponentFixture<IssuingAuthorityDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IssuingAuthorityDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IssuingAuthorityDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
