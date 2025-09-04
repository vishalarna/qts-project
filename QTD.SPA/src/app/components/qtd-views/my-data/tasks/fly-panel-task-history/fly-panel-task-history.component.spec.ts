import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskHistoryComponent } from './fly-panel-task-history.component';

describe('FlyPanelTaskHistoryComponent', () => {
  let component: FlyPanelTaskHistoryComponent;
  let fixture: ComponentFixture<FlyPanelTaskHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
