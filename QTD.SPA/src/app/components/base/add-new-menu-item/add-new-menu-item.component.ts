import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-add-new-menu-item',
  templateUrl: './add-new-menu-item.component.html',
  styleUrls: ['./add-new-menu-item.component.scss'],
})
export class AddNewMenuItemComponent implements OnInit {
  constructor() {}

  /**
   * value of text box component
   */
  @Input()
  textValue: string = '';

  /**
   * Placeholder for textbox component
   */
  @Input()
  Placeholder: string = 'Text Here';

  /**
   * Will bubble out event when Save button is clicked
   */
  @Output()
  Saved = new EventEmitter<any>();

  /**
   * Will bubble out event when cancel button is clicked
   */
  @Output()
  Cancelled = new EventEmitter<any>();

  ngOnInit(): void {}
}
