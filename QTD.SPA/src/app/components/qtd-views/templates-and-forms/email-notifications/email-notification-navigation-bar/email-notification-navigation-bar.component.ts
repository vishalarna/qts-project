import { Component, ElementRef, OnInit, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-email-notification-navigation-bar',
  templateUrl: './email-notification-navigation-bar.component.html',
  styleUrls: ['./email-notification-navigation-bar.component.scss']
})
export class EmailNotificationNavigationBarComponent implements OnInit {
  @Input()
  public clientNotifications: Array<any> = [];

  @Input()
  public selectedNotification: number = -1;

  @Output()
  onNotificationClickEvent: EventEmitter<boolean> = new EventEmitter();
  constructor(
    private store: Store<{ toggle: string }>,
  ) {
  }

  ngOnInit(): void {
    
  }

  toggleMainMenu() {
    this.store.dispatch(sideBarToggle());
  }
  getSelectedNotification(val: any) {
    this.onNotificationClickEvent.emit(val.name);
  }
}
