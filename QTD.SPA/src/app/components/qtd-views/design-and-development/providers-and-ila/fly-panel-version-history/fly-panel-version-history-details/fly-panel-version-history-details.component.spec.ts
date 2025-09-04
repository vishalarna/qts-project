import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelVersionHistoryDetailsComponent } from './fly-panel-version-history-details.component';

describe('FlyPanelVersionHistoryDetailsComponent', () => {
  let component: FlyPanelVersionHistoryDetailsComponent;
  let fixture: ComponentFixture<FlyPanelVersionHistoryDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelVersionHistoryDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelVersionHistoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
