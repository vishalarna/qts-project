import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelFilterTrainingIssuesComponent } from './fly-panel-filter-training-issues.component';

describe('FlyPanelFilterTrainingIssuesComponent', () => {
  let component: FlyPanelFilterTrainingIssuesComponent;
  let fixture: ComponentFixture<FlyPanelFilterTrainingIssuesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelFilterTrainingIssuesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelFilterTrainingIssuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
