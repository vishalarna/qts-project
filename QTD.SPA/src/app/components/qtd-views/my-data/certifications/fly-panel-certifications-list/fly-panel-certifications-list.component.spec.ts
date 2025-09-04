import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCertificationsListComponent } from './fly-panel-certifications-list.component';

describe('FlyPanelCertificationsListComponent', () => {
  let component: FlyPanelCertificationsListComponent;
  let fixture: ComponentFixture<FlyPanelCertificationsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCertificationsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCertificationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
