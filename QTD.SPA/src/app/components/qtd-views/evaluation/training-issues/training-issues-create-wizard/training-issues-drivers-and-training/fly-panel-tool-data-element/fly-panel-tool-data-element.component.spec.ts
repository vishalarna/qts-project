import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelToolDataElementComponent } from './fly-panel-tool-data-element.component';

describe('FlyPanelToolDataElementComponent', () => {
  let component: FlyPanelToolDataElementComponent;
  let fixture: ComponentFixture<FlyPanelToolDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelToolDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelToolDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
