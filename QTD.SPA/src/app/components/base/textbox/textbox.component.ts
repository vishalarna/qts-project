import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges,
  forwardRef,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-textbox',
  templateUrl: './textbox.component.html',
  styleUrls: ['./textbox.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextboxComponent),
      multi: true,
    },
  ],
})
export class TextboxComponent implements OnInit, ControlValueAccessor {
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
   The default value of the textbox
  */
  @Input()
  defaultValue: string = '';

  /**
   * for two way binding of an Input field
   */
  @Output()
  defaultValueChange = new EventEmitter<string>();

  /**
   Determines if the textbox is enabled or disabled
  */
  @Input()
  disabled: boolean = false;

  /**
   The name of the textbox
  */
  @Input()
  name: string;

  /**
   The placeholder text
  */
  @Input()
  placeholder: string = '';

  /**
   * type of input field
   */
  @Input()
  type: 'email' | 'text' | 'number' = 'text';

  /**
   Event fired when the value of the textbox changes
  */

  @Input()
  maxLength;

  @Input()
  minLength;

  @Output()
  changed = new EventEmitter<any>();

  @Input()
  cy: string;

  @Input() isPercentage: boolean = false;
  
  ngOnInit(): void {}

  valueChanged(e: Event) {
    if (this.disabled) return;
    const input = e.target as HTMLInputElement;
    let val = input.value;
    if (this.type === 'number' && this.isPercentage) {
      val = val.replace(/[^0-9.]/g, '');
      let numericVal = parseFloat(val);
  
      if (!isNaN(numericVal)) {
        numericVal = Math.min(Math.max(numericVal, 0), 100);
        val = numericVal.toString();
      } else {
        val = '';
      }
  
      input.value = val;
    }
    this.defaultValue = val;
    this.onChange(this.defaultValue);
    this.changed.emit(this.defaultValue);
  }
}
