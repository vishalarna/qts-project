import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IssuingauthorityDetailsComponent } from './issuingauthority-details.component';

describe('IssuingauthorityDetailsComponent', () => {
  let component: IssuingauthorityDetailsComponent;
  let fixture: ComponentFixture<IssuingauthorityDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IssuingauthorityDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IssuingauthorityDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
