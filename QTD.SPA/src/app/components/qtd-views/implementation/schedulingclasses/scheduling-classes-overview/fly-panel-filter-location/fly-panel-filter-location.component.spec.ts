import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelFilterLocationComponent } from './fly-panel-filter-location.component';

describe('FlyPanelFilterLocationComponent', () => {
  let component: FlyPanelFilterLocationComponent;
  let fixture: ComponentFixture<FlyPanelFilterLocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelFilterLocationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelFilterLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
