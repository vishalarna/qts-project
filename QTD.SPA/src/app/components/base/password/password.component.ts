import {
  Component,
  EventEmitter,
  forwardRef,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PasswordComponent),
      multi: true,
    },
  ],
})
export class PasswordComponent implements OnInit, ControlValueAccessor {
  constructor() {}

  writeValue(value: any) {
    this.defaultValue = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  setDisabledState(isDisabled: boolean) {
    
    this.disabled = isDisabled;
  }

  onChange = (_: any) => {};
  onTouch = (_: any) => {};

  /**
   The default value of the input
  */
  @Input()
  defaultValue: string;

  /**
   Determines if the input is disabled
   disabled passwords still should be able to use the showPassword proprety
  */
  @Input()
  disabled: boolean;

  /**
   The name of the password field on the form
  */
  @Input()
  name: string = 'Password';

  /**
    The placeholder text
  */
  @Input()
  placeholder: string;

  /**
   * Determines if the input should be masked or not
   * If true then the input should have an "eye" icon that will be open (true) or
   * closed (false) that when clicked will toggle this value
   */
  @Input()
  showPassword: boolean = false;

  /**
   Fired when the value is changed
  */
  @Output()
  changed = new EventEmitter<Event>();

  @Input()
  cy: string;

  ngOnInit(): void {}

  valueChanged(e: Event) {
    
    if (this.disabled) return;
    let val = (e.target as HTMLInputElement).value;
    this.defaultValue = val;
    this.onChange(this.defaultValue);
  }
}
