import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralDatabaseSettingsComponent } from './general-database-settings.component';

describe('GeneralDatabaseSettingsComponent', () => {
  let component: GeneralDatabaseSettingsComponent;
  let fixture: ComponentFixture<GeneralDatabaseSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GeneralDatabaseSettingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneralDatabaseSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
