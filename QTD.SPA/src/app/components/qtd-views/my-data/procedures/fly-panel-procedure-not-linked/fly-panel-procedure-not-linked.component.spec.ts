import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelProcedureNotLinkedComponent } from './fly-panel-procedure-not-linked.component';

describe('FlyPanelProcedureNotLinkedComponent', () => {
  let component: FlyPanelProcedureNotLinkedComponent;
  let fixture: ComponentFixture<FlyPanelProcedureNotLinkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelProcedureNotLinkedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelProcedureNotLinkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
