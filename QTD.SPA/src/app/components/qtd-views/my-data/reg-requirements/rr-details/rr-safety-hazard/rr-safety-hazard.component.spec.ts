import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RRSafetyHazardComponent } from './rr-safety-hazard.component';

describe('RRSafetyHazardComponent', () => {
  let component: RRSafetyHazardComponent;
  let fixture: ComponentFixture<RRSafetyHazardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RRSafetyHazardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RRSafetyHazardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
