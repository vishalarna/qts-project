import { DatePipe } from '@angular/common';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { CSE_ILACertPartialCredit, CSE_ILACertPartialCreditCreateUpdateOption, CSE_ILACertSubRequirementPartialCredit } from '@models/CSE_ILACertLink_PartialCredit/CSE_ILACertPartialCreditCreateUpdateOption';
import { EmployeeIdsModel } from '@models/Employee/EmployeeIdsModel';
import { ClassRoasterUpdateOptions } from '@models/SchedulesClassses/Rosters/ClassRosterUpdateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { Cse_IlaCertPartialCreditService } from 'src/app/_Services/QTD/cse_ilaCertLink_partialCredit.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';

@Component({
  selector: 'app-fly-panel-edit-enrollment',
  templateUrl: './fly-panel-edit-enrollment.component.html',
  styleUrls: ['./fly-panel-edit-enrollment.component.scss']
})
export class FlyPanelEditEnrollmentComponent implements OnInit {
  datePipe = new DatePipe('en-us');
  GradeError = false;
  showSpinner = false;
  validGrades = ["P","F","W","O"];
  @Input() currrentEmployee;
  @Input() classId ;
  @Output() closed = new EventEmitter<any>();
  @Output() updateEnrollment = new EventEmitter<any>();
  @Input() ilaId = "" ;
  updateForm:UntypedFormGroup
  readyForm(){
  this.updateForm = this.Fb.group({
    FinalScore: new UntypedFormControl(this.currrentEmployee.score),
    FinalGrade: new UntypedFormControl(this.currrentEmployee.grade),
    ClassCompletionDate: new UntypedFormControl(this.datePipe.transform(this.currrentEmployee.evaluationCompletedDate, 'yyyy-MM-dd')),
    GradeNotes: new UntypedFormControl(this.currrentEmployee.gradeNotes),
    isQualificationCompleted: new UntypedFormControl(this.currrentEmployee?.taskQualificationCompleted)
  });
}
  populateOjtDisable:boolean = false;
  warningDialogDescription:string = "";
  helpString:string = "";
  ilaCertData:any[] = [];
  loader:boolean = false;
  editedCell: string | number | null = null;
  cehHrValue: string = '';
  subCreditValues: string[] = [];
  cseILACertPartialCreditData:any[];
  @ViewChild('cehInput') cehInputRef: ElementRef<HTMLInputElement>;
  @ViewChildren('subInput') subInputRefs: QueryList<ElementRef<HTMLInputElement>>;
  shouldFocusInput: boolean = false;
  constructor(private Fb:UntypedFormBuilder,private ilaService:IlaService,
                  public dialog: MatDialog,private labelPipe:LabelReplacementPipe,private cse_ilaCertPartialCreditService:Cse_IlaCertPartialCreditService) { }

  ngOnInit(): void { 
    this.readyForm();
    this.loadAsync();
  }

  ngAfterViewChecked() {
    if (this.shouldFocusInput) {
      if (this.editedCell === 'ceh' && this.cehInputRef) {
        this.cehInputRef.nativeElement.focus();
      } else if (typeof this.editedCell === 'number' && this.subInputRefs && this.subInputRefs.length) {
        this.subInputRefs.first.nativeElement.focus();
      }
      this.shouldFocusInput = false;
    }
  }

  async loadAsync(){
    this.loader = true;
    this.populateOjtDisable = await this.ilaService.canPopulateOJTBeDeactivateAsync(this.ilaId);
    this.helpString = "Selecting this checkbox indicates that as part of successful completion of this ILA/Course," + await this.labelPipe.transform("Task") + " Qualification has been performed on designated" + await this.labelPipe.transform("Task") + "s linked to this ILA/Course. Selecting this box will record a " + await this.labelPipe.transform("Task") +" Qualification entry with the " + await this.labelPipe.transform("ILA") + "  Completed Date and a comment indicating qualification occurred as part of this ILA/Course"
    this.ilaCertData = await this.ilaService.getILANERCCertificationSubRequirementNamesForPartialCreditAsync(this.ilaId);
    this.subCreditValues = Array(this.ilaCertData[0]?.ilaCertificationSubRequirementLinkVM?.length || 0).fill('');
    await this.getClassEmpILACertPartialCreditData();
    this.loader = false;
  }

  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  checkInputTbl(event:any){
    if(event.data){
      if(this.validGrades.includes(String(event.data).trim().toUpperCase())){
        this.GradeError = false;
      }
      else{
        this.GradeError =true
      }
    }
    else{
      this.GradeError = false;
    }
  }

  updateClassEnrollment(){
    var options  = new ClassRoasterUpdateOptions() 
      options.grade  =  this.updateForm.get('FinalGrade')?.value;
      options.score  =  this.updateForm.get('FinalScore')?.value;
      options.completionDate = this.updateForm.get('ClassCompletionDate')?.value ? (this.updateForm.get('ClassCompletionDate')?.value + 'T12:00:00'):null;
      options.gradeNotes = this.updateForm.get('GradeNotes')?.value;
      options.classId = this.classId
      options.isQualificationCompleted = this.updateForm.get('isQualificationCompleted')?.value;
      this.addOrUpdateClassEmpILACertPartialCredit();
      this.updateEnrollment.emit(options)
  }

  checkDisable(value:any){
    if (value === undefined || value === null || value.trim() === '') {
      return false;
    }
    if (this.validGrades.includes(String(value).trim().toUpperCase())){
      return false
    }else{
      return true
    }    
  }

  async submitData(templateRef: any) {
    const isQualificationCompleted = this.updateForm.get('isQualificationCompleted')?.value;
  
    if (isQualificationCompleted) {
     const option = new EmployeeIdsModel();
      option.employeeIds.push(this.currrentEmployee?.classEmployeeId);
  
      const pendingTasks = await this.ilaService.getPendingLinkedTaskObjectivesAsync(this.ilaId, option);
  
      if (pendingTasks.length > 0) {
        let tableHtml = `
          <table class = "inner-html-dialog-table">
            <thead>
              <tr>
                <th>Task No.</th>
                <th>Task Description</th>
                <th>Evaluators</th>
              </tr>
            </thead>
            <tbody>
        `;
  
        pendingTasks.forEach(task => {
          tableHtml += `
            <tr>
              <td>${task.taskFullNumber || '--'}</td>
              <td>${task.description || '--'}</td>
              <td>${task.evaluatorsName?.join(",") || '--'}</td>
            </tr>
          `;
        });
  
        tableHtml += `
            </tbody>
          </table>
        `;
  
        this.warningDialogDescription = `TQs still pending in EMP. Checking this box will mark pending TQs linked to this ${await this.labelPipe.transform("ILA")} as completed and remove from EMP.
        ${tableHtml}`;
  
        const dialogRef = this.dialog.open(templateRef, {
          width: '700px',
          height: 'auto',
          hasBackdrop: true,
          disableClose: true,
        });
      } else {
        await this.updateClassEnrollment();
      }
    } else {
      await this.updateClassEnrollment();
    }
  }

  onCehHrInput(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = input.value;
    if (/^\d*\.?\d{0,2}$/.test(value) || value === '') {
      if (value === '' || parseFloat(value) <= this.ilaCertData[0].cehHours) {
        this.cehHrValue = value;
      } else {
        input.value = this.cehHrValue;
      }
    } else {
      input.value = this.cehHrValue;
    }
  }

  onSubCreditInput(event: Event, index: number) {
    const input = event.target as HTMLInputElement;
    const value = input.value;
    if (/^\d*\.?\d{0,2}$/.test(value) || value === '') {
      const maxAllowed = this.ilaCertData[0].ilaCertificationSubRequirementLinkVM[index].reqHours;
      if (value === '' || parseFloat(value) <= maxAllowed) {
        this.subCreditValues[index] = value;
      } else {
        input.value = this.subCreditValues[index] ?? '';
      }
    } else {
      input.value = this.subCreditValues[index] ?? '';
    }
  }

  onEditCeh() {
    this.editedCell = 'ceh';
    this.shouldFocusInput = true;
  }

  onEditSub(i: number) {
    this.editedCell = i;
    this.shouldFocusInput = true;
  }

  async addOrUpdateClassEmpILACertPartialCredit() {
    var optionsList = new CSE_ILACertPartialCreditCreateUpdateOption();
    var options = new CSE_ILACertPartialCredit();
    options.classScheduleEmployeeId = this.currrentEmployee.classScheduleEmployeeId;
    options.partialCreditHours =this.cehHrValue == ''? null : Number(this.cehHrValue);
    this.ilaCertData[0]?.ilaCertificationSubRequirementLinkVM.forEach((sub, i) => {
      const subReq = new CSE_ILACertSubRequirementPartialCredit();
      subReq.reqName = sub.reqName;
      subReq.partialCreditHours = this.subCreditValues[i] == '' ? null: Number(this.subCreditValues[i]); 
      options.subRequirements.push(subReq);
    })
    optionsList.cSE_ILACertPartialCredits.push(options);
    await this.cse_ilaCertPartialCreditService.addOrUpdateClassEmpILACertLinkPartialCreditHoursAsync(this.ilaId,optionsList);
  }

  async getClassEmpILACertPartialCreditData(){
    var clsEmpIdOption = new EmployeeIdsModel();
    clsEmpIdOption.employeeIds.push(this.currrentEmployee.classScheduleEmployeeId);
    this.cseILACertPartialCreditData = await this.cse_ilaCertPartialCreditService.getByClassScheduleEmployeeIdAsync(clsEmpIdOption);
    if(this.cseILACertPartialCreditData.length>0){
      await this.setILACertPartialCreditValues();
    }
  }

  async setILACertPartialCreditValues(){
    this.cehHrValue = this.cseILACertPartialCreditData[0]?.partialCreditHours ?? '';
    this.cseILACertPartialCreditData[0]?.classScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits.forEach((sub,i)=>{
      this.subCreditValues[i] = sub.partialCreditHours??''
    })
  }
  
}

