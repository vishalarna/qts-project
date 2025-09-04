import { Component, Input, OnInit } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Output, EventEmitter } from '@angular/core';
@Component({
  selector: 'app-fly-panel-filter-idp',
  templateUrl: './fly-panel-filter-idp.component.html',
  styleUrls: ['./fly-panel-filter-idp.component.scss']
})
export class FlyPanelFilterIdpComponent implements OnInit {
  selectedGrades: string[] = ['Apple', 'Orange', 'Banana'];
  datepipe = new DatePipe('en-us');
  IlaFilterForm = new UntypedFormGroup({});
  FilterStatus=["All","Active","Inactive"]
  @Output() FilterEmployeesEvent = new EventEmitter<any>();
  @Input() FilterData:any=null;
  constructor(public flyPanelSrvc: FlyInPanelService) { }

  ngOnInit(): void {
    this.selectedGrades.push("Opt 1")
    this.readyForm();
  }

  async readyForm() {
    if (this.FilterData!==null) {
      var data=this.FilterData.FilterForm;
      this.IlaFilterForm.addControl('IsSelfRegestrationEnabled', new UntypedFormControl(data.get('IsSelfRegestrationEnabled')?.value));
      this.IlaFilterForm.addControl('selectedGrade', new UntypedFormControl(data.get('selectedGrade')?.value));
      this.IlaFilterForm.addControl('EndDate', new UntypedFormControl(data.get('EndDate')?.value));
      this.IlaFilterForm.addControl('startDate', new UntypedFormControl(data.get('startDate')?.value));
      this.IlaFilterForm.addControl('plannedDate', new UntypedFormControl(data.get('plannedDate')?.value));
      this.IlaFilterForm.addControl('SelectedStatus', new UntypedFormControl(data.get('SelectedStatus')?.value));
      this.IlaFilterForm.addControl('plannedDateEnd', new UntypedFormControl(data.get('plannedDateEnd')?.value));
      this.IlaFilterForm.addControl('plannedDateStart', new UntypedFormControl(data.get('plannedDateStart')?.value));
      this.IlaFilterForm.addControl('endDateStart', new UntypedFormControl(data.get('endDateStart')?.value));
      this.IlaFilterForm.addControl('startDateStart', new UntypedFormControl(data.get('startDateStart')?.value));

      this.IlaFilterForm.addControl('endDateEnd', new UntypedFormControl(data.get('endDateEnd')?.value));
      this.IlaFilterForm.addControl('startDateEnd', new UntypedFormControl(data.get('startDateEnd')?.value));
      this.IlaFilterForm.addControl('selectedGradeOptions', new UntypedFormControl(data.get('selectedGradeOptions')?.value));

    }else{
      this.IlaFilterForm.addControl('IsSelfRegestrationEnabled', new UntypedFormControl(0));
      this.IlaFilterForm.addControl('selectedGrade', new UntypedFormControl(0));
      this.IlaFilterForm.addControl('EndDate', new UntypedFormControl(1));
      this.IlaFilterForm.addControl('startDate', new UntypedFormControl(1));
      this.IlaFilterForm.addControl('plannedDate', new UntypedFormControl(1));
      this.IlaFilterForm.addControl('SelectedStatus', new UntypedFormControl(1));
      this.IlaFilterForm.addControl('plannedDateEnd', new UntypedFormControl());
      this.IlaFilterForm.addControl('plannedDateStart', new UntypedFormControl());
      this.IlaFilterForm.addControl('endDateStart', new UntypedFormControl());
      this.IlaFilterForm.addControl('startDateStart', new UntypedFormControl());

      this.IlaFilterForm.addControl('endDateEnd', new UntypedFormControl());
      this.IlaFilterForm.addControl('startDateEnd', new UntypedFormControl());
      this.IlaFilterForm.addControl('selectedGradeOptions', new UntypedFormControl());
    }
  }


  readyFormInsertion() {
    // this.mode=this.requalificationItem.requalificationRequired === true ? "update" : "new";
    // this.isEditable=this.requalificationItem.requalificationRequired !== null ? false  : true;
    // this.RequalificationhistoryForm.get('requalificationDueDate')?.setValue(this.datepipe.transform(this.requalificationItem.requalificationDueDate ?? Date.now(), 'yyyy-MM-dd'));
    // this.RequalificationhistoryForm.get('requalificationRequired')?.setValue( this.requalificationItem.requalificationRequired?? false);
    // this.RequalificationhistoryForm.get('requalificationFormNotes')?.setValue(this.requalificationItem.requalificationNotes?? "");

  }
  closeFlyPanel() {
    this.flyPanelSrvc.close();
  }

  gradeOptionChange(e: any) {


  }
  ApplyFiltersIlasTable(){

    this.FilterEmployeesEvent.emit({
    status:this.IlaFilterForm.get("SelectedStatus")?.value==1
      ? null : this.IlaFilterForm.get("SelectedStatus")?.value==2 ? true :false ,
    planedDateStart:this.IlaFilterForm.get("plannedDate")?.value==0 ? null : this.IlaFilterForm.get("plannedDateStart")?.value,
    planedDateEnd:this.IlaFilterForm.get("plannedDate")?.value==0 ? null : this.IlaFilterForm.get("plannedDateEnd")?.value,
    startDateStart:this.IlaFilterForm.get("startDate")?.value==0 ? null : this.IlaFilterForm.get("startDateStart")?.value,
    startDateEnd:this.IlaFilterForm.get("startDate")?.value==0 ? null : this.IlaFilterForm.get("endDateStart")?.value,
    endDateStart:this.IlaFilterForm.get("EndDate")?.value==0 ? null : this.IlaFilterForm.get("startDateEnd")?.value,
    endDateEnd:this.IlaFilterForm.get("EndDate")?.value==0 ? null : this.IlaFilterForm.get("endDateEnd")?.value,
    GradeSelected:this.IlaFilterForm.get("selectedGrade")?.value==0 ? ["All"]:this.IlaFilterForm.get("selectedGrade")?.value==1 ? ["No"] : this.IlaFilterForm.get("selectedGradeOptions")?.value,
    SelfRegisteredEnabled:this.IlaFilterForm.get("IsSelfRegestrationEnabled")?.value==0 ? null : this.IlaFilterForm.get("IsSelfRegestrationEnabled")?.value==1?true:false,
    FilterForm:this.IlaFilterForm
  });
    this.flyPanelSrvc.close();
  }

  change(e: any) {

  }
}
