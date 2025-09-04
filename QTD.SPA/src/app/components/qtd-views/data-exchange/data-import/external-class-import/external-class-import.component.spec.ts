import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalClassImportComponent } from './external-class-import.component';

describe('ExternalClassImportComponent', () => {
  let component: ExternalClassImportComponent;
  let fixture: ComponentFixture<ExternalClassImportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExternalClassImportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExternalClassImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
