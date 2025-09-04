import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkToolsComponent } from './fly-panel-link-tools.component';

describe('FlyPanelLinkToolsComponent', () => {
  let component: FlyPanelLinkToolsComponent;
  let fixture: ComponentFixture<FlyPanelLinkToolsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkToolsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkToolsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
