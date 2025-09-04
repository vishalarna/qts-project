import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CehUploadExportComponent } from './ceh-upload-export.component';

describe('CehUploadExportComponent', () => {
  let component: CehUploadExportComponent;
  let fixture: ComponentFixture<CehUploadExportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CehUploadExportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CehUploadExportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
