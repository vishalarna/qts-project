import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCertificationHistoryComponent } from './fly-panel-certification-history.component';

describe('FlyPanelCertificationHistoryComponent', () => {
  let component: FlyPanelCertificationHistoryComponent;
  let fixture: ComponentFixture<FlyPanelCertificationHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCertificationHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlyPanelCertificationHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
