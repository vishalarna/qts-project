import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: 'input[type=date][appDateValidation]'
})
export class TypedDateValidatorDirective {

  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event'])
  onInput(event: Event) {
    this.validateDate(event);
  }

  @HostListener('blur', ['$event'])
  onBlur(event: Event) {
    this.validateDate(event);
  }

  private validateDate(event: Event) {
    debugger;
    const inputDate = new Date((event.target as HTMLInputElement).value);
    const minDate = new Date('1950-01-01');

    if (inputDate < minDate) {
      (event.target as HTMLInputElement).setCustomValidity('Year should not be less than 1950');
    } else {
      (event.target as HTMLInputElement).setCustomValidity('');
    }
  }
}
