import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationsEnabledComponent } from './notifications-enabled.component';

describe('NotificationsEnabledComponent', () => {
  let component: NotificationsEnabledComponent;
  let fixture: ComponentFixture<NotificationsEnabledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NotificationsEnabledComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NotificationsEnabledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
