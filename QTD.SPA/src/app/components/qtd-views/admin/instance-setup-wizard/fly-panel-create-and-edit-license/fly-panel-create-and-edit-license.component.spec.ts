import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCreateAndEditLicenseComponent } from './fly-panel-create-and-edit-license.component';

describe('FlyPanelCreateAndEditLicenseComponent', () => {
  let component: FlyPanelCreateAndEditLicenseComponent;
  let fixture: ComponentFixture<FlyPanelCreateAndEditLicenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCreateAndEditLicenseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCreateAndEditLicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
