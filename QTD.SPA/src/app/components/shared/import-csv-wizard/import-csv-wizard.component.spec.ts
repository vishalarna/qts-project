import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportCsvWizardComponent } from './import-csv-wizard.component';

describe('ImportCsvWizardComponent', () => {
  let component: ImportCsvWizardComponent;
  let fixture: ComponentFixture<ImportCsvWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportCsvWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportCsvWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
