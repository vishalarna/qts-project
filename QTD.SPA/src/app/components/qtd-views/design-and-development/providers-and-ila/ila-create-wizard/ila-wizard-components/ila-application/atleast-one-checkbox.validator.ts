import { UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';

export function atleastOneCheckBox(minRequired = 1): Validators {
  return (formGroup: UntypedFormGroup):Validators |null=> {
    let checked = 0;

    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.controls[key];

      if (control.value === true) {
        checked ++;
      }
    });

    if (checked < minRequired) {
      return {
        requireOneCheckboxToBeChecked: true,
      };
    }

    return null;
  };
}


export function MaximumOneCheckBox(maxRequired = 1): Validators {
  return (formGroup: UntypedFormGroup):Validators |null=> {
    let checked = 0;

    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.controls[key];

      if (control.value === true) {
        checked ++;
      }
    });

    if (checked !==1) {
      return {
        requireOneCheckboxToBeChecked: true,
      };
    }

    return null;
  };
}
