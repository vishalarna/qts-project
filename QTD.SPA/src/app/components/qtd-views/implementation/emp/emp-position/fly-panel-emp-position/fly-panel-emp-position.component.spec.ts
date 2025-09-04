import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEmpPositionComponent } from './fly-panel-emp-position.component';

describe('FlyPanelEmpPositionComponent', () => {
  let component: FlyPanelEmpPositionComponent;
  let fixture: ComponentFixture<FlyPanelEmpPositionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEmpPositionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEmpPositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
