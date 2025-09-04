import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailAddressEditorComponent } from './email-address-editor.component';

describe('EmailAddressEditorComponent', () => {
  let component: EmailAddressEditorComponent;
  let fixture: ComponentFixture<EmailAddressEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailAddressEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailAddressEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
