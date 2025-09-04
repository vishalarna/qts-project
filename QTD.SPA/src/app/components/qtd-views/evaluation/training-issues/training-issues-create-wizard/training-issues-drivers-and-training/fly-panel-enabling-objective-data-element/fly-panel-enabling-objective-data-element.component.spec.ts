import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEnablingObjectiveDataElementComponent } from './fly-panel-enabling-objective-data-element.component';

describe('FlyPanelEnablingObjectiveDataElementComponent', () => {
  let component: FlyPanelEnablingObjectiveDataElementComponent;
  let fixture: ComponentFixture<FlyPanelEnablingObjectiveDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEnablingObjectiveDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEnablingObjectiveDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
