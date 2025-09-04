import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-fly-panel-edit-grades',
  templateUrl: './fly-panel-edit-grades.component.html',
  styleUrls: ['./fly-panel-edit-grades.component.scss']
})
export class FlyPanelEditGradesComponent implements OnInit {
  showSpinner = false;
  @Output() closed = new EventEmitter<any>();
  gradesForm: UntypedFormGroup = new UntypedFormGroup({
    PretestScore: new UntypedFormControl('', Validators.required),
    PreTestGrade: new UntypedFormControl('', [Validators.required]),
    CompletionDate: new UntypedFormControl('', [Validators.required]),
    Notes: new UntypedFormControl(''),
  });
  constructor() { }

  ngOnInit(): void {
  }


  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }
}
