import { ComponentFixture, TestBed } from '@angular/core/testing';

import {  SupportingDocumentsAndIssuesComponent } from './supporting-documents-and-issues.component';

describe('SupportingDocumentsAndIssuesComponent', () => {
  let component: SupportingDocumentsAndIssuesComponent;
  let fixture: ComponentFixture<SupportingDocumentsAndIssuesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupportingDocumentsAndIssuesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SupportingDocumentsAndIssuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
