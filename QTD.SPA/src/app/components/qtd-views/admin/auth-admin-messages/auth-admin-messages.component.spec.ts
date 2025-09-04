import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthAdminMessagesComponent } from './auth-admin-messages.component';

describe('AuthAdminMessagesComponent', () => {
  let component: AuthAdminMessagesComponent;
  let fixture: ComponentFixture<AuthAdminMessagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthAdminMessagesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthAdminMessagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
