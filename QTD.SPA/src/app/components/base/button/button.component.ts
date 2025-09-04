import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  AfterViewInit,
  OnChanges,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent implements OnInit, OnChanges {
  constructor() {}

  /**
   Determines the color of the button.
  */
  @Input()
  color: 'primary' | 'secondary' | 'grey' | 'danger' | 'success' | 'blue' | 'blueBorder' = 'primary';

  /**
    Determines if the button is disabled
  */
  @Input()
  disabled: boolean;

  /**
    Determines if the button should display a drop down menu when clicked.
    This will display a chevron down
  */
  @Input()
  dropdown: boolean;

  /**
    Determines the Icon, if any that should be displayed
  */
  @Input()
  icon: string;

  /**
    Determines if the spinner is displayed or not
  */
  @Input()
  spinner: boolean;

  /**
    Determines if the shape of the button.
  */
  @Input()
  shape: 'rect' | 'round' = 'rect';

  /**
    Determines the size of the button.

    @lg large
    @md medium
    @sm small
  */
  @Input()
  size: 'lg' | 'md' | 'sm' = 'lg';

  /**
    The text of the button
  */
  @Input()
  text: string;

  /**
    Determines the variants of the button.
  */
  @Input()
  variant: 'contained' | 'outlined' | 'text' = 'contained';

  /**
   * Action Type of button
   */
  @Input()
  type: 'button' | 'submit' = 'submit';

  /**
   * Type of Button
   */

  @Input()
  ButtonType: 'button' | 'text' | 'btn-icon' | 'icon' = 'button' ;

  /**
   * This will place button in left right position
   */
  @Input() iconPosition: 'left' | 'right' = 'right';

  /**
    Event fired when button is clicked
  */
  @Output()
  clicked = new EventEmitter<Event>();

  @Input()
  customClasses: string;

  @Input()
  iconClass: string;

  @Input()
  leftIcon: string;

  @Input()
  rightIcon: string;

  @Input()
  cy: string;

  ngOnChanges(changes: SimpleChanges): void {
    this.getClasses();
    this.getIcons();
  }

  ngOnInit(): void {}

  getIcons() {
    /* if (this.icons) {
      let icons = 'fa';
      let iconsList = this.icons.trim().split(' ');
      iconsList.forEach((i) => {
        icons += ' fa-' + i;
      });

      this.icons = icons;
    }
    */
  }

  getClasses() {
    let classes = 'btn';
    /*
    switch (this.variant) {
      case variants.primary:
      default:
        classes += 'btn-primary ';
        break;
      case variants.secondary:
        classes += 'btn-secondary ';
        break;
      case variants.success:
        classes += 'btn-success ';
        break;
      case variants.danger:
        classes += 'btn-danger ';
        break;
      case variants.warning:
        classes += 'btn-warning ';
        break;
      case variants.info:
        classes += 'btn-info ';
        break;
      case variants.light:
        classes += 'btn-light';
        break;
      case variants.dark:
        classes += 'btn-dark';
        break;
      case variants.link:
        classes += 'btn-link';
        break;
      case variants.active:
        classes += 'btn-active';
        break;
      case variants.inactive:
        classes += 'btn-inactive';
        break;
    }
    */
  }
}
