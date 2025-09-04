import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelShHistoryComponent } from './fly-panel-sh-history.component';

describe('FlyPanelShHistoryComponent', () => {
  let component: FlyPanelShHistoryComponent;
  let fixture: ComponentFixture<FlyPanelShHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelShHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelShHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
