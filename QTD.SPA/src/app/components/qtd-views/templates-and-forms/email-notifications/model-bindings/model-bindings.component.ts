import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-model-bindings',
  templateUrl: './model-bindings.component.html',
  styleUrls: ['./model-bindings.component.scss']
})
export class ModelBindingsComponent implements OnInit {

  @Input() modelItems: Array<any> = [];
  @Input() mode: string;
  @Input() isDisabled: boolean;
  @Output()
  modelItemButtonClicked: EventEmitter<any> = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }
  public getModelItemProperties(item) {
    this.modelItemButtonClicked.emit(item.template);
  }

}
