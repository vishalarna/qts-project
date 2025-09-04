import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelChangeEnrollmentSettingsComponent } from './fly-panel-change-enrollment-settings.component';

describe('FlyPanelChangeEnrollmentSettingsComponent', () => {
  let component: FlyPanelChangeEnrollmentSettingsComponent;
  let fixture: ComponentFixture<FlyPanelChangeEnrollmentSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelChangeEnrollmentSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelChangeEnrollmentSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
