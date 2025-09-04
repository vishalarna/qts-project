import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddObjectivesLinkagesComponent } from './fly-panel-add-objectives-linkages.component';

describe('FlyPanelAddObjectivesLinkagesComponent', () => {
  let component: FlyPanelAddObjectivesLinkagesComponent;
  let fixture: ComponentFixture<FlyPanelAddObjectivesLinkagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddObjectivesLinkagesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddObjectivesLinkagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
