import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelMetaEnablingObjectiveDataElementComponent } from './fly-panel-meta-enabling-objective-data-element.component';

describe('FlyPanelMetaEnablingObjectiveDataElementComponent', () => {
  let component: FlyPanelMetaEnablingObjectiveDataElementComponent;
  let fixture: ComponentFixture<FlyPanelMetaEnablingObjectiveDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelMetaEnablingObjectiveDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelMetaEnablingObjectiveDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
