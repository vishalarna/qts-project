import { DatePipe } from '@angular/common';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { CSE_ILACertPartialCredit, CSE_ILACertPartialCreditCreateUpdateOption, CSE_ILACertSubRequirementPartialCredit } from '@models/CSE_ILACertLink_PartialCredit/CSE_ILACertPartialCreditCreateUpdateOption';
import { EmployeeIdsModel } from '@models/Employee/EmployeeIdsModel';
import { IDPVM } from 'src/app/_DtoModels/IDP/IDPVM';
import { IDP_ScoreOptions } from 'src/app/_DtoModels/IDP/IDP_ScoreOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { Cse_IlaCertPartialCreditService } from 'src/app/_Services/QTD/cse_ilaCertLink_partialCredit.service';
import { IdpService } from 'src/app/_Services/QTD/idp.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-edit-grade',
  templateUrl: './fly-panel-add-edit-grade.component.html',
  styleUrls: ['./fly-panel-add-edit-grade.component.scss']
})
export class FlyPanelAddEditGradeComponent implements OnInit {
  datePipe = new DatePipe('en-us');

  @Input() selectedIDPvm: IDPVM;
  @Input() employeeName: string;
  @Output() refresh = new EventEmitter<any>();
  helpString:string = "";
  showSpinner:boolean=false;
  isScoreValid=false;
  gradeForm: UntypedFormGroup = new UntypedFormGroup({
    score: new UntypedFormControl('', Validators.required),
    grade: new UntypedFormControl('', [Validators.required]),
    reason: new UntypedFormControl(''),
    isQualificationCompleted: new UntypedFormControl(false),
    completionDate:new UntypedFormControl('')
  });
  populateOjtDisable:boolean = false;
  warningDialogDescription:string = "";
  ilaCertData:any[] = [];
  subCreditValues: string[] = [];
  cseILACertPartialCreditData:any[];
  loader:boolean = false;
  editedCell: string | number | null = null;
  cehHrValue: string = '';
  @ViewChild('cehInput') cehInputRef: ElementRef<HTMLInputElement>;
  @ViewChildren('subInput') subInputRefs: QueryList<ElementRef<HTMLInputElement>>;
  shouldFocusInput: boolean = false;
  constructor(public flyPanelSrvc: FlyInPanelService,
                private idpService: IdpService,
                private alert : SweetAlertService,
                private labelPipe:LabelReplacementPipe,
                private ilaService:IlaService,
                public dialog: MatDialog,private cse_ilaCertPartialCreditService:Cse_IlaCertPartialCreditService
                ) { }

  ngOnInit(): void {
    this.setFormCurrentValues();
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
    this.populateOjtDisable = await this.ilaService.canPopulateOJTBeDeactivateAsync(this.selectedIDPvm?.ilaId);
    this.helpString = "Selecting this checkbox indicates that as part of successful completion of this ILA/Course," + await this.labelPipe.transform("Task") + " Qualification has been performed on designated" + await this.labelPipe.transform("Task") + "s linked to this ILA/Course. Selecting this box will record a " + await this.labelPipe.transform("Task") +" Qualification entry with the " + await this.labelPipe.transform("ILA") + "  Completed Date and a comment indicating qualification occurred as part of this ILA/Course";
    this.ilaCertData = await this.ilaService.getILANERCCertificationSubRequirementNamesForPartialCreditAsync(this.selectedIDPvm?.ilaId);
    this.subCreditValues = Array(this.ilaCertData[0]?.ilaCertificationSubRequirementLinkVM?.length || 0).fill('');
    await this.getClassEmpILACertPartialCreditData();
    this.loader = false;
  }

  closeFlyPanel() {
    this.flyPanelSrvc.close();
  }

  setFormCurrentValues(){
    
    this.gradeForm.get("score")?.setValue(this.selectedIDPvm.score);
    this.gradeForm.get("grade")?.setValue(this.selectedIDPvm.grade);
    this.gradeForm.get("completionDate")?.setValue(this.selectedIDPvm.completionDate ? this.formatDateToYYYYMMDD(this.selectedIDPvm.completionDate) : null);
    this.gradeForm.get("reason")?.setValue(this.selectedIDPvm.gradeUpdateReason);
    this.gradeForm.get("isQualificationCompleted")?.setValue(this.selectedIDPvm.taskQualificationCompleted);
    var inp=this.gradeForm.get("score")?.value;
    this.isScoreValid= (/^\d+$/.test(inp));
  }

  checkNumber(){
    var inp=this.gradeForm.get("score")?.value;
    this.isScoreValid= (/^\d+$/.test(inp));
  }
  
  formatDateToYYYYMMDD(date) {
    if (!date) return null;

    let parsedDate = new Date(date);
    let year = parsedDate.getFullYear();
    let month = ("0" + (parsedDate.getMonth() + 1)).slice(-2);
    let day = ("0" + parsedDate.getDate()).slice(-2);

    return `${year}-${month}-${day}`;
}


  async updateIDPScore(){
    this.showSpinner=true;
    var option=new IDP_ScoreOptions();
    var compDate = this.gradeForm.get("completionDate")?.value;
    option.completionDate= compDate ? this.datePipe.transform(compDate, "yyyy-MM-dd") + 'T12:00:00':null; 
    option.score=this.gradeForm.get("score")?.value;
    option.grade=this.gradeForm.get("grade")?.value;
    option.reason=this.gradeForm.get("reason")?.value;
    option.isCompleted=this.gradeForm.get("isQualificationCompleted")?.value;
    option.classScheduleId=this.selectedIDPvm.classScheduleId;
    option.employeeId=this.selectedIDPvm.empId;
    const result = await this.idpService.updateIDPScore(option);
    await this.addOrUpdateClassEmpILACertPartialCredit();
    this.showSpinner=false;
    this.closeFlyPanel()
    this.alert.successAlert("Grades updated Successfully");
    this.refresh.emit();
  }

  async submitData(templateRef: any) {
    const isQualificationCompleted = this.gradeForm.get('isQualificationCompleted')?.value;
  
    if (isQualificationCompleted) {
     const option = new EmployeeIdsModel();
      option.employeeIds.push(this.selectedIDPvm?.empId);
  
      const pendingTasks = await this.ilaService.getPendingLinkedTaskObjectivesAsync(this.selectedIDPvm?.ilaId, option);
  
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
              <td>${task.evaluatorsName.join(",") || '--'}</td>
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
        await this.updateIDPScore();
      }
    } else {
      await this.updateIDPScore();
    }
  }

  async getClassEmpILACertPartialCreditData(){
    var clsEmpIdOption = new EmployeeIdsModel();
    clsEmpIdOption.employeeIds.push(this.selectedIDPvm.classScheduleEmployeeId);
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
        input.value = this.subCreditValues[index]??'';
      }
    } else {
      input.value = this.subCreditValues[index]??'';
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

  async addOrUpdateClassEmpILACertPartialCredit(){
    var optionsList = new CSE_ILACertPartialCreditCreateUpdateOption();
    var options = new CSE_ILACertPartialCredit();
        options.classScheduleEmployeeId = this.selectedIDPvm.classScheduleEmployeeId;
        options.partialCreditHours =this.cehHrValue == ''? null : Number(this.cehHrValue);
        this.ilaCertData[0]?.ilaCertificationSubRequirementLinkVM.forEach((sub, i) => {
          const subReq = new CSE_ILACertSubRequirementPartialCredit();
          subReq.reqName = sub.reqName;
          subReq.partialCreditHours = this.subCreditValues[i] == '' ? null: Number(this.subCreditValues[i]); 
          options.subRequirements.push(subReq);
        })
        optionsList.cSE_ILACertPartialCredits.push(options);
    await this.cse_ilaCertPartialCreditService.addOrUpdateClassEmpILACertLinkPartialCreditHoursAsync(this.selectedIDPvm?.ilaId,optionsList);
  }

}
