import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelStatisticsComponent } from './fly-panel-statistics.component';

describe('FlyPanelStatisticsComponent', () => {
  let component: FlyPanelStatisticsComponent;
  let fixture: ComponentFixture<FlyPanelStatisticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelStatisticsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
