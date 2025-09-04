import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddProceduresLinkagesComponent } from './fly-panel-add-procedures-linkages.component';

describe('FlyPanelAddProceduresLinkagesComponent', () => {
  let component: FlyPanelAddProceduresLinkagesComponent;
  let fixture: ComponentFixture<FlyPanelAddProceduresLinkagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddProceduresLinkagesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddProceduresLinkagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
