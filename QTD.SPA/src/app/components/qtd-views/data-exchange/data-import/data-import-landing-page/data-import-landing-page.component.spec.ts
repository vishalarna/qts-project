import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataImportLandingPageComponent } from './data-import-landing-page.component';

describe('DataImportLandingPageComponent', () => {
  let component: DataImportLandingPageComponent;
  let fixture: ComponentFixture<DataImportLandingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DataImportLandingPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DataImportLandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
