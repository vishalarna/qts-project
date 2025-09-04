import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkPositionEmployeeComponent } from './fly-panel-link-position-employee.component';

describe('FlyPanelLinkPositionEmployeeComponent', () => {
  let component: FlyPanelLinkPositionEmployeeComponent;
  let fixture: ComponentFixture<FlyPanelLinkPositionEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkPositionEmployeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkPositionEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
