import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelToolHistoryComponent } from './fly-panel-tool-history.component';

describe('FlyPanelToolHistoryComponent', () => {
  let component: FlyPanelToolHistoryComponent;
  let fixture: ComponentFixture<FlyPanelToolHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelToolHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelToolHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
