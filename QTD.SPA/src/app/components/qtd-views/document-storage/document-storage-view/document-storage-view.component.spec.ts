import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentStorageViewComponent } from './document-storage-view.component';

describe('DocumentStorageViewComponent', () => {
  let component: DocumentStorageViewComponent;
  let fixture: ComponentFixture<DocumentStorageViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentStorageViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentStorageViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
