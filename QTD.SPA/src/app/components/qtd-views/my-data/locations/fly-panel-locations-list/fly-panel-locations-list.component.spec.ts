import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLocationsListComponent } from './fly-panel-locations-list.component';

describe('FlyPanelLocationsListComponent', () => {
  let component: FlyPanelLocationsListComponent;
  let fixture: ComponentFixture<FlyPanelLocationsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLocationsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLocationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
