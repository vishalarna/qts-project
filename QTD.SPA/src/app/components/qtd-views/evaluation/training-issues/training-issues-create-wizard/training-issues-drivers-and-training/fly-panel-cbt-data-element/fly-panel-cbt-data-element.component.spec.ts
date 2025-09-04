import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCbtDataElementComponent } from './fly-panel-cbt-data-element.component';

describe('FlyPanelCbtDataElementComponent', () => {
  let component: FlyPanelCbtDataElementComponent;
  let fixture: ComponentFixture<FlyPanelCbtDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCbtDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCbtDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
