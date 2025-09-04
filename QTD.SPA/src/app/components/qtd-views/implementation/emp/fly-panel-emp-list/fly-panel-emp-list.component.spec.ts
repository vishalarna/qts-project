import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEmpListComponent } from './fly-panel-emp-list.component';

describe('FlyPanelEmpListComponent', () => {
  let component: FlyPanelEmpListComponent;
  let fixture: ComponentFixture<FlyPanelEmpListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEmpListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEmpListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
