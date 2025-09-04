import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTrainingMaterialComponent } from './fly-panel-training-material.component';

describe('FlyPanelTrainingMaterialComponent', () => {
  let component: FlyPanelTrainingMaterialComponent;
  let fixture: ComponentFixture<FlyPanelTrainingMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTrainingMaterialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTrainingMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
