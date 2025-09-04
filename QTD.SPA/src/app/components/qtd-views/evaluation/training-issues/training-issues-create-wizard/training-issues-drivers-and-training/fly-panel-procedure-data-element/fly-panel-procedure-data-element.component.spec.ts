import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProcedureDataElementComponent } from './fly-panel-procedure-data-element.component';

describe('FlyPanelProcedureDataElementComponent', () => {
  let component: FlyPanelProcedureDataElementComponent;
  let fixture: ComponentFixture<FlyPanelProcedureDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProcedureDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProcedureDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
