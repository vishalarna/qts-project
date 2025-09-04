import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssuesActionItemsComponent } from './training-issues-action-items.component';

describe('TrainingIssuesActionItemsComponent', () => {
  let component: TrainingIssuesActionItemsComponent;
  let fixture: ComponentFixture<TrainingIssuesActionItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssuesActionItemsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssuesActionItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
