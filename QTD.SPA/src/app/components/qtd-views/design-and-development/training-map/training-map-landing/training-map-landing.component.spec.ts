import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingMapLandingComponent } from './training-map-landing.component';

describe('TrainingMapLandingComponent', () => {
  let component: TrainingMapLandingComponent;
  let fixture: ComponentFixture<TrainingMapLandingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingMapLandingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingMapLandingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
