import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPositionsHistoryComponent } from './fly-panel-positions-history.component';

describe('FlyPanelPositionsHistoryComponent', () => {
  let component: FlyPanelPositionsHistoryComponent;
  let fixture: ComponentFixture<FlyPanelPositionsHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPositionsHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPositionsHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
