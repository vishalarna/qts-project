import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Observable, Subscription} from 'rxjs';
import {ClientSettings_Notification} from 'src/app/_DtoModels/ClientsSettingsNotification/ClientSettings_Notification';

export interface Fruit {
  name: string;
}

@Component({
  selector: 'app-email-address-editor',
  templateUrl: './email-address-editor.component.html',
  styleUrls: ['./email-address-editor.component.scss']
})

export class EmailAddressEditorComponent implements OnInit {
  @Input() emailAddresses: Array<any> = [];
  public inputAddress: string
  removable = true;
  @Input()
  mode: string;
  @Input()
  enabled: boolean;
  @Output()
  customRecipientsChangeEvent: EventEmitter<any> = new EventEmitter();
  private eventsSubscription: Subscription;
  @Input() events: Observable<ClientSettings_Notification>;
  @Input() employees: boolean;
  @Input() managers: boolean;
  @Input() others: string;
  @Input() showRecipients: boolean = true;

  constructor() {
  }

  ngOnInit(): void {
    if (this.emailAddresses.length > 0) {
      this.others = 'true';
    }
    this.setInitialValues();
  }

  setInitialValues() {
    this.eventsSubscription = this.events.subscribe((data) => {
      this.employees = data[2].value === 'true' ? true : false;
      this.managers = data[3].value == 'true' ? true : false;
      this.getInputEmailAddress(data[4].value);
    });

    this.getInputEmailAddress(this.others)
  }

  public addEmailAddress(val: string): void {
    const emailFromInput = val.trim();
    if (emailFromInput) {
      if (this.validateEmail(emailFromInput)) {
        this.emailAddresses.push({value: emailFromInput, invalid: false});
        this.inputAddress = ''
        this.onCustomSettingChanged("Send To Others", this.getEmailAddressesAsString());
      } else {
      }
    }
  }

  removeEmail(emailAddress: string): void {
    if (this.emailAddresses.indexOf(emailAddress) >= 0) {
      this.emailAddresses.splice(this.emailAddresses.indexOf(emailAddress), 1);
    }

    this.onCustomSettingChanged("Send To Others", this.getEmailAddressesAsString());
  }

  onEmailAddressChange(event) {
    if (this.inputAddress.substr(-1) === ' ') {
      this.addEmailAddress(this.inputAddress);
    }
  }

  public getInputEmailAddress(value) {
    this.emailAddresses = [];
    if (value) {
      const tempEmailAddress = value.split(',');
      tempEmailAddress.forEach(element => {
        this.emailAddresses.push({value: element, invalid: false});
      });
    }
  }

  private validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }

  private getEmailAddressesAsString(): string {
    if (this.emailAddresses) {
      return this.emailAddresses.map(r => r.value).join(',');
    } else return '';
  }

  public onCustomSettingChanged(key, value: string): void {
    this.customRecipientsChangeEvent.emit({key, value});
  }
}
