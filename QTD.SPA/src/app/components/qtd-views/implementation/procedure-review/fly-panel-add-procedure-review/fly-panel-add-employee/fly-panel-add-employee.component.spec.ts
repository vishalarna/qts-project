import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddEmployeeComponent } from './fly-panel-add-employee.component';

describe('FlyPanelAddEmployeeComponent', () => {
  let component: FlyPanelAddEmployeeComponent;
  let fixture: ComponentFixture<FlyPanelAddEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddEmployeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
