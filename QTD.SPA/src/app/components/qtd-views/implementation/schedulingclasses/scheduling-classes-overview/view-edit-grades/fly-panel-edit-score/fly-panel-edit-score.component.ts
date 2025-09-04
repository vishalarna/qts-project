import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-fly-panel-edit-score',
  templateUrl: './fly-panel-edit-score.component.html',
  styleUrls: ['./fly-panel-edit-score.component.scss']
})
export class FlyPanelEditScoreComponent implements OnInit {
  showSpinner = false;
  @Output() closed = new EventEmitter<any>();

  scoreForm: UntypedFormGroup = new UntypedFormGroup({
    finalScore: new UntypedFormControl('', Validators.required),
    finalGrade: new UntypedFormControl('', [Validators.required]),
    gradeNotes: new UntypedFormControl(''),
  });
  constructor() { }

  ngOnInit(): void {
  }

  
  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }
}
