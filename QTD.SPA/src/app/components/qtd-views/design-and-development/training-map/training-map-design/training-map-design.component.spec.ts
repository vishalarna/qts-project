import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingMapDesignComponent } from './training-map-design.component';

describe('TrainingMapDesignComponent', () => {
  let component: TrainingMapDesignComponent;
  let fixture: ComponentFixture<TrainingMapDesignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingMapDesignComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingMapDesignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
