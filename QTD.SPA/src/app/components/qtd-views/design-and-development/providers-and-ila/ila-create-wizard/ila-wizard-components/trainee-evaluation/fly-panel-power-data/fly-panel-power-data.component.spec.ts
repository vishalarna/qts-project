import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPowerDataComponent } from './fly-panel-power-data.component';

describe('FlyPanelPowerDataComponent', () => {
  let component: FlyPanelPowerDataComponent;
  let fixture: ComponentFixture<FlyPanelPowerDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPowerDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPowerDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
