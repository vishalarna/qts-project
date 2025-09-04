import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRrHistoryComponent } from './fly-panel-rr-history.component';

describe('FlyPanelRrHistoryComponent', () => {
  let component: FlyPanelRrHistoryComponent;
  let fixture: ComponentFixture<FlyPanelRrHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelRrHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRrHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
