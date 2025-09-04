import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConclusionAndTrainingComponent } from './conclusion-and-training.component';

describe('ConclusionAndTrainingComponent', () => {
  let component: ConclusionAndTrainingComponent;
  let fixture: ComponentFixture<ConclusionAndTrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConclusionAndTrainingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConclusionAndTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
