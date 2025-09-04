import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-fill-eval-form-dialog',
  templateUrl: './fill-eval-form-dialog.component.html',
  styleUrls: ['./fill-eval-form-dialog.component.scss']
})
export class FillEvalFormDialogComponent implements OnInit {
  evalForm = new UntypedFormGroup({});
  @Output() canceled = new EventEmitter<any>();
  @Output() selected = new EventEmitter<any>();
  @Input() selectedData!:any;
  constructor() { }

  ngOnInit(): void {
    this.evalForm.addControl('type',new UntypedFormControl('individual',Validators.required));
  }

  emitSelection(){
    var data = this.evalForm.get('type')?.value;
    this.selected.emit(data);
  }

}
