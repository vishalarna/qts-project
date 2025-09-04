import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProceduresHistoryComponent } from './fly-panel-procedures-history.component';

describe('FlyPanelProceduresHistoryComponent', () => {
  let component: FlyPanelProceduresHistoryComponent;
  let fixture: ComponentFixture<FlyPanelProceduresHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProceduresHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProceduresHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
