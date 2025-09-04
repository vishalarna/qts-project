import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailNotificationNavigationBarComponent } from './email-notification-navigation-bar.component';

describe('EmailNotificationBarComponent', () => {
  let component: EmailNotificationNavigationBarComponent;
  let fixture: ComponentFixture<EmailNotificationNavigationBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailNotificationNavigationBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailNotificationNavigationBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
