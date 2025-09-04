import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddEmployeePositionComponent } from './fly-panel-add-employee-position.component';

describe('FlyPanelAddEmployeePositionComponent', () => {
  let component: FlyPanelAddEmployeePositionComponent;
  let fixture: ComponentFixture<FlyPanelAddEmployeePositionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddEmployeePositionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddEmployeePositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
