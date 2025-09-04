import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-label[text]',
  templateUrl: './label.component.html',
  styleUrls: ['./label.component.scss'],
})
export class LabelComponent implements OnInit {
  constructor() {}

  /**
   The name of the input the label decorates
  */
  @Input()
  for: string;

  /**
   The text to be displayed
  */
  @Input()
  text: string;

  /**
   * Set custom class for a label
   */
  @Input()
  customClasses: string;

  getClass() {
    return 'inline-block ' + this.customClasses;
  }

  ngOnInit(): void {}
}
