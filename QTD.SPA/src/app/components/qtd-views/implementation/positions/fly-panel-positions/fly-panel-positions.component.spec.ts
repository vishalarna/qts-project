import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPositionsComponent } from './fly-panel-positions.component';

describe('FlyPanelPositionsComponent', () => {
  let component: FlyPanelPositionsComponent;
  let fixture: ComponentFixture<FlyPanelPositionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPositionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPositionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
