import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkPositionFilterComponent } from './fly-panel-link-position-filter.component';

describe('FlyPanelLinkPositionFilterComponent', () => {
  let component: FlyPanelLinkPositionFilterComponent;
  let fixture: ComponentFixture<FlyPanelLinkPositionFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkPositionFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkPositionFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
