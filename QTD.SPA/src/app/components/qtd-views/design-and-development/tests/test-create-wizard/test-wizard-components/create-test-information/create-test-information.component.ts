import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { skip } from 'rxjs/operators';
import { ILATraineeEvaluationCreateOptions } from 'src/app/_DtoModels/ILATraineeEvaluation/ILATraineeEvaluationCreateOptions';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { TestCreateOptions } from 'src/app/_DtoModels/Test/TestCreateOptions';
import { IlaTraineeEvaluationService } from 'src/app/_Services/QTD/ila-trainee-evaluation.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { TestTypeService } from 'src/app/_Services/QTD/test-type.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { Location } from '@angular/common'
import { TraineeEvaluationService } from 'src/app/_Services/QTD/trainee-evaluation.service';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';

@Component({
  selector: 'app-create-test-information',
  templateUrl: './create-test-information.component.html',
  styleUrls: ['./create-test-information.component.scss']
})
export class CreateTestInformationComponent implements OnInit, OnDestroy {

  TestInformationFormGroup: UntypedFormGroup = new UntypedFormGroup({});
  subscriptions = new SubSink();
  @Output() formChanges = new EventEmitter<any>();
  @Output() testData = new EventEmitter<TestCreateOptions>();
  @Output() evaluationData = new EventEmitter<ILATraineeEvaluationCreateOptions>();
  @Output() infoSaved = new EventEmitter<any>();
  @Output() error = new EventEmitter<any>();

  @Input() mode: 'add' | 'edit' | 'copy' = 'add';
  @Input() Id: any = "";

  Title: string;
  Instruction: string = '';
  TimeLimitHours: string;
  TimeLimitMins: string;
  testType: any[] = [];
  view_data: boolean = false;
  edit = false;
  isTestTypeSelected: boolean = false;
  provider_list: any[] = [];
  ila_list: any[] = [];
  dummyIla_list: any[] = [];
  isSpinner: boolean = true;
  isILALoading: boolean = false;
  isOpen = false;
  saveEvent: Observable<any>;
  subscription = new SubSink();
  test: TestCreateOptions = new TestCreateOptions();
  testId = 0;
  currentTest!: Test;
  noILAFound: boolean = false;
  noProviderFound: boolean = false;
  @ViewChild('selectControl', { static: false }) selectControl!: MatSelect;

  constructor(public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private saveStore: Store<{ saveTest: any }>,
    private testTypeService: TestTypeService,
    private providerService: ProviderService,
    private testService: TestsService,
    private ilaService: IlaService,
    private ilaTraineeEvalService: IlaTraineeEvaluationService,
    private traineeEvalService:TraineeEvaluationService
    ) {
    this.saveEvent = this.saveStore.select('saveTest');
  }

  ngOnInit(): void {
    this.isSpinner = true;
    this.readyEvalTypes();
    if (this.mode === 'add') {
      this.readyTestInformationForm();
      this.ReadyTestType();
      this.getProviders();
    }
    else {
      this.testService.get(this.Id).then((res: Test) => {
        this.currentTest = res;
        this.readyTestInformationForm();
        this.ReadyTestType();
        this.getProviders();
      })
    }

    this.subscription.sink = this.saveStore
      .pipe(select('saveTest'), skip(1))
      .subscribe((res) => {
        if (res.tabIndex === 0 && res.update !== undefined) {
          
          this.test.testTitle =
            this.TestInformationFormGroup.get('title')?.value;

          if (res.update === false) {
            this.testService
              .create(this.test)
              .then((res) => {
                this.testId = res.id;

              })
              .catch((err) => {

              });
          }

        }

      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();

  }

  evalTypeId!:any;

  async readyEvalTypes(){
   var allEvalTypes  = await this.traineeEvalService.getAll();
   this.evalTypeId = allEvalTypes.find((x)=>{
    return x.name === 'Written';
   }).id;
  }

  async getTestData() {
    await this.testService.get(this.Id).then((res: Test) => {
      this.currentTest = res;
    })
  }

  readyTestInformationForm() {
    this.TestInformationFormGroup = this.fb.group({
      title: new UntypedFormControl(this.Title, [Validators.required]),
      radio: new UntypedFormControl(),
      Instruction: new UntypedFormControl(''),
      provider: new UntypedFormControl(''),
      ILA: new UntypedFormControl('', [Validators.required]),
      TimeLimitHours: new UntypedFormControl(),
      TimeLimitMins: new UntypedFormControl(),
      ilaSearchText:new UntypedFormControl()
    });

    if (this.mode !== 'add') {
      this.TestInformationFormGroup.patchValue({
        title: this.currentTest.testTitle,
        Instruction: this.currentTest.ilaTraineeEvaluations[0].testInstruction,
        TimeLimitHours: this.currentTest.ilaTraineeEvaluations[0].testTimeLimitHours,
        TimeLimitMins: this.currentTest.ilaTraineeEvaluations[0].testTimeLimitMinutes,
      })
    }

    this.subscriptions.sink = this.TestInformationFormGroup.statusChanges.subscribe((res: any) => {
      if (res === 'VALID') {
        var test = new TestCreateOptions();
        test.testTitle = this.TestInformationFormGroup.get('title')?.value;
        test.testStatusId = null;
        this.testData.emit(test);
        var evalData = new ILATraineeEvaluationCreateOptions();
        evalData.evaluationTypeId = null;
        evalData.ilaId = this.TestInformationFormGroup.get('ILA')?.value;
        evalData.testId = '';
        evalData.testInstruction = this.TestInformationFormGroup.get('Instruction')?.value;
        evalData.testTimeLimitHours = this.TestInformationFormGroup.get('TimeLimitHours')?.value;
        evalData.testTimeLimitMinutes = this.TestInformationFormGroup.get('TimeLimitMins')?.value;
        evalData.testTitle = this.TestInformationFormGroup.get('title')?.value;
        evalData.testTypeId = (this.TestInformationFormGroup.get('radio')?.value).id;
        evalData.trainingEvaluationMethod = '';
        this.evaluationData.emit(evalData);

        this.formChanges.emit(res);
      }

    });
  }

  inputChange(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  async getProviders() {
    this.isSpinner = true;
    this.providerService
      .getActiveProviders()
      .then((res: any) => {
        this.provider_list = res;
        if(this.provider_list.length === 0)
        {
          this.noProviderFound = true;
        }
        else
        {
          this.noProviderFound = false;
        }
        if (this.mode !== 'add') {
          this.ilaService.get(this.currentTest.ilaTraineeEvaluations[0].ilaId).then((ila) => {
            var currProv = this.provider_list.find((data) => {
              return data.id === ila.providerId;
            });
            
            this.TestInformationFormGroup.patchValue({
              provider: currProv.id,
            });
            this.selectProvider(currProv.id);
          })
        }
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

  selectProvider(event: any) {
    this.ila_list = [];
    this.isILALoading = true;
    this.TestInformationFormGroup.get('ILA')?.setValue(undefined);
    this.ilaService.getByProvider(event)
      .then(async (res: any) => {
        this.ila_list = res.filter(c=>c.active);
        this.dummyIla_list = Object.assign(this.ila_list);
        if(this.ila_list.length == 0)
        {
          this.noILAFound = true;
        }
        else
        {
          this.noILAFound = false;
        }
        if (this.mode !== 'add') {
          var currILA = this.ila_list.find((data) => {
            return data.id === this.currentTest.ilaTraineeEvaluations[0].ilaId;
          });
          this.TestInformationFormGroup.patchValue({
            ILA: currILA.id,
          })
        }
      })
      .catch((err) => {
        
      })
      .finally(() => {
        this.isILALoading = false;
      });
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.view_data = false;
    this.edit = false;
    this.flyPanelService.open(portal);
  }

  async ReadyTestType() {
    this.isSpinner = true;
    await this.testTypeService.getAll().then((res: any) => {
      this.testType = res.splice(0,3);
      
      if (this.mode !== 'add') {
        var currType = this.testType.find((data) => {
          return data.id === this.currentTest.ilaTraineeEvaluations[0].testTypeId;
        });
        
        this.isTestTypeSelected = true;
        this.TestInformationFormGroup.patchValue({
          radio: currType,
        })
      }
    }).catch((err) => {
      this.alert.errorToast("Failed To Fetch Test Types");
    }).finally(() => {
      this.isSpinner = false;
    })
  }

  testTypeChange() {
    this.isTestTypeSelected = true;
  }

  async saveInfo() {
    var test = new TestCreateOptions();
    test.testTitle = this.TestInformationFormGroup.get('title')?.value;
    if (this.mode === 'copy' && test.testTitle.trim().toLowerCase() === this.currentTest.testTitle.trim().toLowerCase()) {
      test.testTitle = test.testTitle + ' - Copy';
    }
    test.testStatusId = null;
    this.testService.create(test).then((res: Test) => {
      var evalData = new ILATraineeEvaluationCreateOptions();
      evalData.evaluationTypeId = null;
      evalData.ilaId = this.TestInformationFormGroup.get('ILA')?.value;
      evalData.testId = '';
      evalData.testInstruction = this.TestInformationFormGroup.get('Instruction')?.value;
      evalData.testTimeLimitHours = this.TestInformationFormGroup.get('TimeLimitHours')?.value;
      evalData.testTimeLimitMinutes = this.TestInformationFormGroup.get('TimeLimitMins')?.value;
      evalData.testTitle = this.TestInformationFormGroup.get('title')?.value;
      evalData.testTypeId = (this.TestInformationFormGroup.get('radio')?.value).id;
      evalData.trainingEvaluationMethod = '';
      evalData.testId = res.id;
      if (evalData.testTimeLimitHours === null) {
        evalData.testTimeLimitHours = 0;
      }
      if (evalData.testTimeLimitMinutes === null) {
        evalData.testTimeLimitMinutes = 0;
      }
      
      this.ilaTraineeEvalService.create(evalData).then((_) => {
        this.alert.successToast(`Test and Related Data ${this.mode === 'copy' ? "Copied" : "Saved"}`);
        // this.location.go(`/dnd/tests/edit/${res.id}`);
        
        this.Id = res.id;
        this.getTestData();
        this.infoSaved.emit();
      })/* .catch((res: any) => {
        
        this.alert.errorToast(`Error ${this.mode === 'copy' ? "Copying" : "Saving"} Test Data`);
        this.error.emit();
      }) */
    })/* .catch((res: any) => {
      
      this.alert.errorToast(`Error ${this.mode === 'copy' ? "Copying" : "Saving"} Test Data`);
      this.error.emit();
    }) */
  }

  updateData() {
    var test = new TestCreateOptions();
    test.testTitle = this.TestInformationFormGroup.get('title')?.value;
    test.testStatusId = null;
    /// UPDATE TEST HERE THEN UPDATE THE TRAINEE EVALUATION FOR SINGLE EVENT EMISSION
    this.testService.update(this.Id, test).then((res: Test) => {
      var evalData = new ILATraineeEvaluationCreateOptions();
      evalData.evaluationTypeId = null;
      evalData.ilaId = this.TestInformationFormGroup.get('ILA')?.value;
      evalData.testId = '';
      evalData.testInstruction = this.TestInformationFormGroup.get('Instruction')?.value;
      evalData.testTimeLimitHours = this.TestInformationFormGroup.get('TimeLimitHours')?.value;
      evalData.testTimeLimitMinutes = this.TestInformationFormGroup.get('TimeLimitMins')?.value;
      evalData.testTitle = this.TestInformationFormGroup.get('title')?.value;
      evalData.testTypeId = (this.TestInformationFormGroup.get('radio')?.value).id;
      evalData.trainingEvaluationMethod = '';
      evalData.testId = this.Id;
      if (evalData.testTimeLimitHours === null) {
        evalData.testTimeLimitHours = 0;
      }
      if (evalData.testTimeLimitMinutes === null) {
        evalData.testTimeLimitMinutes = 0;
      }
      this.ilaTraineeEvalService.update(this.currentTest.ilaTraineeEvaluations[0].id, evalData).then((_) => {
        this.alert.successToast("Test And Evaluation Data Updated");
        //this.infoSaved.emit();
      }).catch((res: any) => {
        this.alert.errorAlert("Error Updating Test and Evaluation Data");
        this.error.emit();
      })
    });
  }

  ilaSearch(){
    var searchValue = this.TestInformationFormGroup.get('ilaSearchText')?.value;
    if (searchValue !== undefined && searchValue !== null) {
      searchValue = String(searchValue).toLowerCase();
    } else {
      searchValue = "";
    }
    this.ila_list = this.dummyIla_list.filter((x)=>{
      var fullData = `${x.number} - ${x.name}`;
      return fullData.toLowerCase().trim().includes(searchValue.trim())
    })
  }

  handleKeydown(event: KeyboardEvent) {
    this.selectControl._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

  resetSearch(){
    setTimeout(() => {
      this.TestInformationFormGroup.get('ilaSearchText')?.setValue('');
      this.ilaSearch();
    }, 500);
  }
}
