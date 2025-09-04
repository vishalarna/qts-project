import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkedProceduresComponent } from './fly-panel-linked-procedures.component';

describe('FlyPanelLinkedProceduresComponent', () => {
  let component: FlyPanelLinkedProceduresComponent;
  let fixture: ComponentFixture<FlyPanelLinkedProceduresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkedProceduresComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkedProceduresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
