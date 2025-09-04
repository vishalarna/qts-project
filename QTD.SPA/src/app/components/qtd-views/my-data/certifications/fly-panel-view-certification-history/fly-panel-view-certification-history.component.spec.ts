import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelViewCertificationHistoryComponent } from './fly-panel-view-certification-history.component';

describe('FlyPanelViewCertificationHistoryComponent', () => {
  let component: FlyPanelViewCertificationHistoryComponent;
  let fixture: ComponentFixture<FlyPanelViewCertificationHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelViewCertificationHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelViewCertificationHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
