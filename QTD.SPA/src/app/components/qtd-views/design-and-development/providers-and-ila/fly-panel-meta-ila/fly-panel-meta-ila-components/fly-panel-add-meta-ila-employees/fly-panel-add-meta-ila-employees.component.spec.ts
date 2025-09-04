import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddMetaILAEmployeesComponent } from './fly-panel-add-meta-ila-employees.component';

describe('FlyPanelAddMetaILAEmployeesComponent', () => {
  let component: FlyPanelAddMetaILAEmployeesComponent;
  let fixture: ComponentFixture<FlyPanelAddMetaILAEmployeesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddMetaILAEmployeesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddMetaILAEmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
