import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelDocumentStorageComponent } from './fly-panel-document-storage.component';

describe('FlyPanelDocumentStorageComponent', () => {
  let component: FlyPanelDocumentStorageComponent;
  let fixture: ComponentFixture<FlyPanelDocumentStorageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelDocumentStorageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelDocumentStorageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
