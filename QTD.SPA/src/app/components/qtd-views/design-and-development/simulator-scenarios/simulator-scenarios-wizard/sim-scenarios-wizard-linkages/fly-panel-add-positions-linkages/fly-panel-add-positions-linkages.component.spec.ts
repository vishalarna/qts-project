import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddPositionsLinkagesComponent } from './fly-panel-add-positions-linkages.component';

describe('FlyPanelAddPositionLinkagesComponent', () => {
  let component: FlyPanelAddPositionsLinkagesComponent;
  let fixture: ComponentFixture<FlyPanelAddPositionsLinkagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddPositionsLinkagesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddPositionsLinkagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
