import { TrainingProgramCreateOptions } from './../../../../../_DtoModels/TrainingProgram/TrainingProgramCreateOptions';
import { TrainingProgramsService } from './../../../../../_Services/QTD/training-programs.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import {
  AbstractControl,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-add-training-program',
  templateUrl: './fly-panel-add-training-program.component.html',
  styleUrls: ['./fly-panel-add-training-program.component.scss'],
})
export class FlyPanelAddTrainingProgramComponent implements OnInit {
  //variables, input and output declared
  @Output() cancel: EventEmitter<any> = new EventEmitter<any>();
  @Input() positionIdChild: any;
  training_map: any[] = [];
  forDatabase: string;
  numberRegex = /^-?(0|[1-9]\d*)?$/;
  addTrainingProgramForm: UntypedFormGroup;

  constructor(
    private trainingProgram: TrainingProgramsService,
    private alertService: SweetAlertService,
    private translate: TranslateService,
    public flyPanelSrvc: FlyInPanelService
  ) {}

  ngOnInit(): void {
    //training map list
    this.training_map = [
      { title: 'Intial Training Map', value: 'I' },
      { title: 'Continue Training Map', value: 'C' },
    ];

    

    //initialize form group with form control names
    this.addTrainingProgramForm = new UntypedFormGroup({
      programType: new UntypedFormControl('', [Validators.required]),
      versionField: new UntypedFormControl(
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(this.numberRegex),
        ])
      ),
      startdate: new UntypedFormControl('', [Validators.required]),
      enddate: new UntypedFormControl(),
    });
  }

  //function for fly in to add new training program
  async onSubmit() {
    

    // if (!this.addTrainingProgramForm.valid) {
    //   this.alertService.errorAlert('Invalid data provided');
    //   return;
    // }
    // //creating createOpt for storing data in database
    // let createOpt: TrainingProgramCreateOptions = {
    //   version: this.addTrainingProgramForm.get('versionField')?.value,
    //   programType: this.forDatabase,
    //   startDate: this.addTrainingProgramForm.get('startdate')?.value,
    //   endDate: this.addTrainingProgramForm.get('enddate')?.value,
    //   positionId: this.positionIdChild,
    //   programTitle: 'I',
    // };

    // //service call
    // await this.trainingProgram.create(createOpt).then((res) => {
    //   this.alertService.successToast(this.translate.instant('L.' + res));
    //   this.flyPanelSrvc.close();
    // });
  } // onSubmit ends

  cancelButton(e: any) {
    
    this.cancel.emit(e);
  }
}
