import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CehuploadComponent } from './cehupload.component';

describe('CehuploadComponent', () => {
  let component: CehuploadComponent;
  let fixture: ComponentFixture<CehuploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CehuploadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CehuploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
