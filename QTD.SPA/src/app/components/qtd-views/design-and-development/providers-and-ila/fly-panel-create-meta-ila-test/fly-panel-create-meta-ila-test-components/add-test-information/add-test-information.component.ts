import {AfterViewInit, Component,EventEmitter,Input,OnDestroy,OnInit, Output, ViewContainerRef} from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MetaILA_SummaryTest_ViewModel } from 'src/app/_DtoModels/MetaILA_SummaryTest/MetaILA_SummaryTest_ViewModel';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { TestType } from 'src/app/_DtoModels/TestType/TestType';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TestTypeService } from 'src/app/_Services/QTD/test-type.service';

@Component({
  selector: 'app-add-test-information',
  templateUrl: './add-test-information.component.html',
  styleUrls: ['./add-test-information.component.scss'],
})
export class AddTestInformationComponent implements OnInit, OnDestroy,AfterViewInit {
  @Input() metaILASummaryTestVM:MetaILA_SummaryTest_ViewModel;
  @Input() testTypeDescription:any;
  @Output() formStatus = new EventEmitter<boolean>();
  createMetaILATestForm: UntypedFormGroup;
  testTypes: TestType[] = [];
  positions:Position[]=[];
  constructor(
    private positionService: PositionsService,
    private fb: UntypedFormBuilder,
    private testTypeService: TestTypeService,
  ) {}
  
  async ngOnInit(): Promise<void> {
    this.initializeCreateMetaILATest();
    await this.loadAsync();
    this.setFormValues();
    this.formStatus.emit(this.createMetaILATestForm.valid);
  }

  ngOnDestroy(): void {
  }

  ngAfterViewInit(): void {
   
    
  }
  initializeCreateMetaILATest(){
    this.createMetaILATestForm = this.fb.group({
      title: new UntypedFormControl(null, [Validators.required]),
      instructions: new UntypedFormControl(null),
      TimeLimitHours: new UntypedFormControl(null),
      TimeLimitMins: new UntypedFormControl(null),
      testType: new UntypedFormControl(null ,[Validators.required]),
      position: new UntypedFormControl(null),
    });
  }

  async loadAsync(){
    await this.testTypeService.getAll().then((res: any) => {
      this.testTypes = res.splice(1,2);
    });
    this.positions = (await this.positionService.getAllWithoutIncludes()).filter(x=>x.active);
  }
  setFormValues() {
    this.createMetaILATestForm.patchValue({
      title: this.metaILASummaryTestVM?.test?.testTitle,
      instructions: this.metaILASummaryTestVM?.testInstruction,
      TimeLimitHours: this.metaILASummaryTestVM?.testTimeLimitHours,
      TimeLimitMins: this.metaILASummaryTestVM?.testTimeLimitMinutes,
      testType: this.metaILASummaryTestVM?.testTypeId,
      position: this.metaILASummaryTestVM?.positionId,
    });
  }
  titleChange() {
    let testTitle = this.createMetaILATestForm.get('title').value;
    this.metaILASummaryTestVM.test.testTitle = testTitle;
    this.formStatus.emit(this.createMetaILATestForm.valid);
  }
  
  instructionChange(){
    let instructions = this.createMetaILATestForm.get('instructions').value;
    this.metaILASummaryTestVM.testInstruction = instructions;
  }
  
  timeLimitHoursChange(){
    let timeLimitHours = this.createMetaILATestForm.get('TimeLimitHours').value;
    this.metaILASummaryTestVM.testTimeLimitHours = timeLimitHours;
  }

  timeLimitMinsChange(){
    let timeLimitMins = this.createMetaILATestForm.get('TimeLimitMins').value;
    this.metaILASummaryTestVM.testTimeLimitMinutes = timeLimitMins;
  }

  testTypeChange() {
    let testType = this.createMetaILATestForm.get('testType').value;
    this.metaILASummaryTestVM.testTypeId = testType;
    this.formStatus.emit(this.createMetaILATestForm.valid);
  }

  positionChange() {
    let position = this.createMetaILATestForm.get('position').value;
    this.metaILASummaryTestVM.positionId = position;
  }

  inputChange(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }
  
}
