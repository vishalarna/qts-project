import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { CSE_ILACertPartialCredit, CSE_ILACertPartialCreditCreateUpdateOption, CSE_ILACertSubRequirementPartialCredit } from '@models/CSE_ILACertLink_PartialCredit/CSE_ILACertPartialCreditCreateUpdateOption';
import { EmployeeIdsModel } from '@models/Employee/EmployeeIdsModel';
import { ClassRoasterUpdateOptions } from '@models/SchedulesClassses/Rosters/ClassRosterUpdateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { Cse_IlaCertPartialCreditService } from 'src/app/_Services/QTD/cse_ilaCertLink_partialCredit.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';

@Component({
  selector: 'app-roster-bulk-update-dialog',
  templateUrl: './roster-bulk-update-dialog.component.html',
  styleUrls: ['./roster-bulk-update-dialog.component.scss']
})
export class RosterBulkUpdateDialogComponent implements OnInit {

  bulkEditFormGroup: UntypedFormGroup;
  validGrades = ["P","F","W","O"];
  gradeError = true;
  scoreError = true;
  dateCompletionError = true;
  @Input() ilaId:any;
  @Output() bulkUpdateGrade = new EventEmitter<any>();
  @Output() canceled = new EventEmitter<any>();
  @Output() confirmed = new EventEmitter<any>();
  populateOjtDisable:boolean = false;
  warningDialogDescription:string = "";
  helpString:string = "";
  @Input() employeesDetail:any;
  @Input() classId:any;
  ilaCertData:any[] = [];
  loader:boolean = false;
  cehHrValues: string[] = []; 
  subCreditValues: string[][] = [];
  editedCell: string = "";
  shouldFocusInput: boolean = false; 
  
  constructor(private fb: UntypedFormBuilder,private ilaService:IlaService,
                    public dialog: MatDialog,private labelPipe:LabelReplacementPipe,private cse_ilaCertPartialCreditService:Cse_IlaCertPartialCreditService) { }

  ngOnInit(): void {
    this.readyBulkEditForm();
    this.loadAsync();
  }

  ngAfterViewChecked() {
    if (this.shouldFocusInput) {
      if (this.editedCell.startsWith('ceh_')) {
        const j = this.editedCell.split('_')[1];
        const input = document.getElementById(`cehInput_${j}`);
        if (input) input.focus();
      } else if (this.editedCell.startsWith('sub_')) {
        const [_, j, i] = this.editedCell.split('_');
        const input = document.getElementById(`subInput_${j}_${i}`);
        if (input) input.focus();
      }
      this.shouldFocusInput = false;
    }
  }

  async loadAsync(){
    this.loader = true;
    this.populateOjtDisable = await this.ilaService.canPopulateOJTBeDeactivateAsync(this.ilaId);
    this.helpString = "Selecting this checkbox indicates that as part of successful completion of this ILA/Course," + await this.labelPipe.transform("Task") + " Qualification has been performed on designated" + await this.labelPipe.transform("Task") + "s linked to this ILA/Course. Selecting this box will record a " + await this.labelPipe.transform("Task") +" Qualification entry with the " + await this.labelPipe.transform("ILA") + "  Completed Date and a comment indicating qualification occurred as part of this ILA/Course";
    this.ilaCertData = await this.ilaService.getILANERCCertificationSubRequirementNamesForPartialCreditAsync(this.ilaId);
    this.cehHrValues = new Array(this.employeesDetail.length).fill('');
    this.subCreditValues = this.employeesDetail.map(() => new Array(this.ilaCertData[0]?.ilaCertificationSubRequirementLinkVM?.length || 0).fill(''));
    await this.getClassEmpILACertPartialCreditData();
    this.loader = false;
  }

  readyBulkEditForm() {
    this.bulkEditFormGroup = this.fb.group({
      dateCompleted: new UntypedFormControl(null),
      score: new UntypedFormControl(null),
      gradeNotes: new UntypedFormControl(null),
      gradeValue: new UntypedFormControl(null),
      isQualificationCompleted:new UntypedFormControl(null)
    });
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {      
      event.preventDefault();
      this.scoreError = true      
      return false;
    } else {
      this.scoreError = false
      return true;
    }
  }


  checkInput(event:any){
    if(event.data){
      if(this.validGrades.includes(String(event.data).trim().toUpperCase())){
        this.gradeError = false;
      }
      else{
        this.gradeError =true
      }
    }
    else{
      this.gradeError = false;
    }
  }

  handleDateCompletion(event: any) {
    const inputDate = event.target.value;
    if (inputDate && isNaN(Date.parse(inputDate))) {
      this.dateCompletionError = true;
    } else {
      this.dateCompletionError = false;
    }
  }

  bulkUpdate(){
    var options  = new ClassRoasterUpdateOptions() 
      options.bulkGrade  =  this.bulkEditFormGroup.get('gradeValue')?.value;
      options.bulkScore  =  this.bulkEditFormGroup.get('score')?.value;
      options.bulkCompDate = this.bulkEditFormGroup.get('dateCompleted')?.value ? this.bulkEditFormGroup.get('dateCompleted')?.value + 'T12:00:00':null;
      options.bulkGradeNote = this.bulkEditFormGroup.get('gradeNotes')?.value;      
      options.isQualificationCompleted = this.bulkEditFormGroup.get('isQualificationCompleted')?.value;
      options.classId = this.classId;
      this.addOrUpdateClassEmpILACertPartialCredit();
      this.bulkUpdateGrade.emit(options)
  }

  handleDisable(value:any){
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
      const isQualificationCompleted = this.bulkEditFormGroup.get('isQualificationCompleted')?.value;
    
      if (isQualificationCompleted) {
       const option = new EmployeeIdsModel();
        option.employeeIds = this.employeesDetail.map(item=>item.employeeId);
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
          await this.bulkUpdate();
        }
      } else {
        await this.bulkUpdate();
      }
    }

    onCehHrInput(event: Event, j: number) {
      const input = event.target as HTMLInputElement;
      const value = input.value;
      if (/^\d*\.?\d{0,2}$/.test(value) || value === '') {
        if (value === '' || parseFloat(value) <= this.ilaCertData[0].cehHours) {
          this.cehHrValues[j] = value;
        } else {
          input.value = this.cehHrValues[j] ?? '';
        }
      } else {
        input.value = this.cehHrValues[j] ?? '';
      }
    }
  
    onSubCreditInput(event: Event, j: number, i: number) {
      const input = event.target as HTMLInputElement;
      const value = input.value;
      if (/^\d*\.?\d{0,2}$/.test(value) || value === '') {
        const maxAllowed = this.ilaCertData[0].ilaCertificationSubRequirementLinkVM[i].reqHours;
        if (value === '' || parseFloat(value) <= maxAllowed) {
          this.subCreditValues[j][i] = value;
        } else {
          input.value = this.subCreditValues[j][i] ?? '';
        }
      } else {
        input.value = this.subCreditValues[j][i] ?? '';
      }
    }

    onEditCeh(j: number) {
      this.editedCell = 'ceh_' + j;
      this.shouldFocusInput = true;
    }
  
    onEditSub(j: number, i: number) {
      this.editedCell = 'sub_' + j + '_' + i;
      this.shouldFocusInput = true;
    }

    async getClassEmpILACertPartialCreditData() {
      this.cehHrValues = new Array(this.employeesDetail.length).fill('');
      this.subCreditValues = this.employeesDetail.map(() => []);
    
      const employeeIds = this.employeesDetail.map(emp => emp.classScheduleEmployeeId);
      const clsEmpIdOption = new EmployeeIdsModel();
      clsEmpIdOption.employeeIds = employeeIds;
      const allData = await this.cse_ilaCertPartialCreditService.getByClassScheduleEmployeeIdAsync(clsEmpIdOption);
    
      this.employeesDetail.forEach((emp, j) => {
        const empData = allData.find(d => d.classScheduleEmployeeId === emp.classScheduleEmployeeId);
        if (empData) {
          this.setILACertPartialCreditValues(j, empData);
        }
      });
    }
    
    async setILACertPartialCreditValues(j: number, employeeData: any) {
      this.cehHrValues[j] = employeeData?.partialCreditHours ?? '';
      if (!this.subCreditValues[j]) {
        this.subCreditValues[j] = [];
      }
      const requiredLength = this.ilaCertData[0]?.ilaCertificationSubRequirementLinkVM?.length || 0;
      if (this.subCreditValues[j].length < requiredLength) {
        this.subCreditValues[j] = new Array(requiredLength).fill('').map((_, i) => this.subCreditValues[j][i] ?? '');
      }
      employeeData?.classScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits?.forEach((sub, i) => {
        if (i < this.subCreditValues[j].length) {
          this.subCreditValues[j][i] = sub.partialCreditHours ?? '';
        }
      });
    }

    async addOrUpdateClassEmpILACertPartialCredit() {
      const optionsList = new CSE_ILACertPartialCreditCreateUpdateOption();
    
      for (let j = 0; j < this.employeesDetail.length; j++) {
        const emp = this.employeesDetail[j];
        const cehValue = this.cehHrValues[j] ?? null;
    
        const subRequirements: CSE_ILACertSubRequirementPartialCredit[] = [];
        const subReqList = this.ilaCertData[0]?.ilaCertificationSubRequirementLinkVM || [];
    
        for (let i = 0; i < subReqList.length; i++) {
          const subReq = subReqList[i];
          const subCreditValue = this.subCreditValues[j]?.[i] ?? null;
    
          subRequirements.push({
            reqName: subReq.reqName,
            partialCreditHours: subCreditValue ? Number(subCreditValue) : null
          });
        }
    
        const empCredit: CSE_ILACertPartialCredit = {
          classScheduleEmployeeId: emp.classScheduleEmployeeId,
          partialCreditHours: cehValue ? Number(cehValue) : null,
          subRequirements: subRequirements
        };
    
        optionsList.cSE_ILACertPartialCredits.push(empCredit);
      }
      await this.cse_ilaCertPartialCreditService.addOrUpdateClassEmpILACertLinkPartialCreditHoursAsync(this.ilaId,optionsList)
    }
    

}
