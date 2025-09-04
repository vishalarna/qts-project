import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelViewEmpCertificationHistoryComponent } from './fly-panel-view-emp-certification-history.component';

describe('FlyPanelViewEmpCertificationHistoryComponent', () => {
  let component: FlyPanelViewEmpCertificationHistoryComponent;
  let fixture: ComponentFixture<FlyPanelViewEmpCertificationHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelViewEmpCertificationHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelViewEmpCertificationHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
