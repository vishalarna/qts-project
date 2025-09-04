import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-input-error',
  templateUrl: './input-error.component.html',
  styleUrls: ['./input-error.component.scss'],
})
export class InputErrorComponent implements OnInit, OnChanges {
  constructor() {}
  ngOnChanges(changes: SimpleChanges): void {
    this.getClass();
  }

  /**
   The text of the error
  */
  @Input()
  text: string;

  @Input()
  cy: string;

  getClass() {
    return 'text-danger';
  }

  ngOnInit(): void {}
}
