import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss'],
})
export class SpinnerComponent implements OnInit {
  constructor() {}

  /**
   Determines the type of spinner to show
  */
  @Input()
  variant: 'border' | 'grow' = 'border';

  /**
   Determines the color of spinner
  */
  @Input()
  color: string;

  ngOnInit(): void {}
}
