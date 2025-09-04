import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelOverviewFilterComponent } from './fly-panel-overview-filter.component';

describe('FlyPanelOverviewFilterComponent', () => {
  let component: FlyPanelOverviewFilterComponent;
  let fixture: ComponentFixture<FlyPanelOverviewFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelOverviewFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelOverviewFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
