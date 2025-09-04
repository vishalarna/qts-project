import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-textarea',
  templateUrl: './textarea.component.html',
  styleUrls: ['./textarea.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextareaComponent),
      multi: true,
    },
  ],
})
export class TextareaComponent implements OnInit, ControlValueAccessor {
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
  disabled: boolean = false;
  @Input()
  placeholder: string = '';

  /**
   The default value of the textbox
  */
  @Input()
  defaultValue: string = '';
  constructor() {}

  /**
   * Custom Classes for 'a' element
   */
  @Input()
  customClasses: string;

  @Input()
  value: any = '';

  @Input()
  maxLength: number = 0;

  @Input() isDisabled = false;

  ngOnInit(): void {}

  valueChanged(e: Event) {
    if (this.disabled) return;
    let val = (e.target as HTMLInputElement).value;
    this.defaultValue = val;
    this.onChange(this.defaultValue);
  }
}
