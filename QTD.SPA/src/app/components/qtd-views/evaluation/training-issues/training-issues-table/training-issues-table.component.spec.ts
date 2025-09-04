import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssuesTableComponent } from './training-issues-table.component';

describe('TrainingIssuesTableComponent', () => {
  let component: TrainingIssuesTableComponent;
  let fixture: ComponentFixture<TrainingIssuesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssuesTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssuesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
