import { Component, EventEmitter, forwardRef, Input, OnInit, Output } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-date',
  templateUrl: './date.component.html',
  styleUrls: ['./date.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DateComponent),
      multi: true,
    },
  ],
})
export class DateComponent implements OnInit {

  constructor() { }
  @Output()
  defaultValueChange = new EventEmitter<Date>();

  @Output()
  changed = new EventEmitter<Event>();

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

   //name of date field on the fly in
   @Input()
   name:string

     //min of date field on the fly in
     @Input()
     minDate:string

       //max of date field on the fly in
       @Input()
       maxDate:string
 
   //placeholder for date field on the fly in
   @Input()
   placeholder:string
 
   //date is enabled or disbaled
   @Input()
   disabled: boolean = false;
 
   //value for ngModel 
   @Input()
   defaultValue: string = '';

  ngOnInit(): void {}

  // for changing the value of date
  valueChanged(e: Event) {
    if (this.disabled) return;
    let val = (e.target as HTMLInputElement).value;
    this.defaultValue = val;
    this.onChange(this.defaultValue);
    this.changed.emit(e);
  }
}
