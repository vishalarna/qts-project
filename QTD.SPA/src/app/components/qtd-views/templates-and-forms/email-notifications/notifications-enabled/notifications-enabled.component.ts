import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-notifications-enabled',
  templateUrl: './notifications-enabled.component.html',
  styleUrls: ['./notifications-enabled.component.scss']
})
export class NotificationsEnabledComponent implements OnInit {

  @Input()
  mode: string

  @Input()
  enabled: boolean

  @Input()
  timingText:string;

  @Output()
  onNotificationEnabledToggleChangeEvent: EventEmitter<boolean> = new EventEmitter();

  @Input() events: Observable<boolean>;
  private eventsSubscription: Subscription;
  constructor(
  ) {

  }

  ngOnInit(): void {
    this.eventsSubscription = this.events?.subscribe((data) => {
      this.enabled = data;
    });
  }

  onCheckboxChanged(item: any) {
    //might just make more sense to pass the whole event.  I dont know yet
    this.onNotificationEnabledToggleChangeEvent.emit(item.target.checked);
  }

}
