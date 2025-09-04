import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelOverviewTpNoreviewsComponent } from './fly-panel-overview-tp-noreviews.component';

describe('FlyPanelOverviewTpNoreviewsComponent', () => {
  let component: FlyPanelOverviewTpNoreviewsComponent;
  let fixture: ComponentFixture<FlyPanelOverviewTpNoreviewsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelOverviewTpNoreviewsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelOverviewTpNoreviewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
