import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-fly-panel-assign-score',
  templateUrl: './fly-panel-assign-score.component.html',
  styleUrls: ['./fly-panel-assign-score.component.scss']
})
export class FlyPanelAssignScoreComponent implements OnInit {
  showSpinner = false;
  @Output() closed = new EventEmitter<any>();

  assignScoreForm: UntypedFormGroup = new UntypedFormGroup({
    PretestScore: new UntypedFormControl('', Validators.required),
    PreTestGrade: new UntypedFormControl('', [Validators.required]),
    Notes: new UntypedFormControl(''),
  });
  constructor() { }

  ngOnInit(): void {
  }

  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }
}
