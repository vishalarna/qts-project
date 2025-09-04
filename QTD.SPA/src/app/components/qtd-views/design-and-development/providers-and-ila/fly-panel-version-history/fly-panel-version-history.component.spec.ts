import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelVersionHistoryComponent } from './fly-panel-version-history.component';

describe('FlyPanelVersionHistoryComponent', () => {
  let component: FlyPanelVersionHistoryComponent;
  let fixture: ComponentFixture<FlyPanelVersionHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelVersionHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelVersionHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
