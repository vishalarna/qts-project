import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelViewLocationHistoryComponent } from './fly-panel-view-location-history.component';

describe('FlyPanelViewLocationHistoryComponent', () => {
  let component: FlyPanelViewLocationHistoryComponent;
  let fixture: ComponentFixture<FlyPanelViewLocationHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelViewLocationHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelViewLocationHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
