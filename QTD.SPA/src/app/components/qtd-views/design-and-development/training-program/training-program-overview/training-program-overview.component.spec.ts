import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingProgramOverviewComponent } from './training-program-overview.component';

describe('TrainingProgramOverviewComponent', () => {
  let component: TrainingProgramOverviewComponent;
  let fixture: ComponentFixture<TrainingProgramOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingProgramOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingProgramOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
