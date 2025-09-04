import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelRegulatoryRequirementDataElementComponent } from './fly-panel-regulatory-requirement-data-element.component';

describe('FlyPanelRegulatoryRequirementDataElementComponent', () => {
  let component: FlyPanelRegulatoryRequirementDataElementComponent;
  let fixture: ComponentFixture<FlyPanelRegulatoryRequirementDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FlyPanelRegulatoryRequirementDataElementComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelRegulatoryRequirementDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
