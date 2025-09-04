import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UntypedFormGroup,
  UntypedFormBuilder,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { SubdutyArea } from 'src/app/_DtoModels/SubdutyArea/SubdutyArea';
import { SubdutyAreaCreateOptions } from 'src/app/_DtoModels/SubdutyArea/SubdutyAreaCreateOptions';
import { SubdutyAreaUpdateOptions } from 'src/app/_DtoModels/SubdutyArea/SubdutyAreaUpdateOptions';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-add-subdutyarea',
  templateUrl: './flypanel-add-subdutyarea.component.html',
  styleUrls: ['./flypanel-add-subdutyarea.component.scss'],
})
export class FlypanelAddSubdutyareaComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  showSpinner: boolean = false;
  SubDutyAreaForm: UntypedFormGroup;
 /*  dateError = false; */
  datePipe = new DatePipe('en-us');
  dutyAreas: DutyArea[] = [];
  @Input() oldSDA: SubdutyArea;
  @Input() isCopy: boolean = false;
  @Input() shouldNavigate: boolean = false;
  @Output() refresh = new EventEmitter<any>();
  constructor(
    private fb: UntypedFormBuilder,
    private daSrvc: DutyAreaService,
    private alert: SweetAlertService,
    private dataBroadcastService:DataBroadcastService,
  ) {}

  ngOnInit(): void {
    this.readySubDutyAreaForm();
    this.getallDutyAreas().then(() => {
      if (this.oldSDA) {
        this.SubDutyAreaForm.patchValue({
          dutyAreaId: this.oldSDA.dutyAreaId,
          number: this.oldSDA.subNumber,
          title: this.oldSDA.title,
          desc: this.oldSDA.description,
          reason: "",
          effectiveDate: this.datePipe.transform(
            Date.now(),
            'yyyy-MM-dd'
          ),
        });
        if (this.isCopy) this.getSubDutyAreaNumber(this.oldSDA.dutyAreaId);
      }
    });
  }

  readySubDutyAreaForm() {
    this.SubDutyAreaForm = this.fb.group({
      dutyAreaId: new UntypedFormControl('', [Validators.required]),
      number: new UntypedFormControl('', [Validators.required]),
      title: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      desc: new UntypedFormControl(''),
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      reason: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      AddAnother: new UntypedFormControl(false),
    });
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null;
    } else {
      return { whitespaceOnly: true };
    }
  }

  async getallDutyAreas() {
    await this.daSrvc.getAll().then((res) => {
      this.dutyAreas = res;
    });
  }

  async getSubDutyAreaNumber(id: any) {
    await this.daSrvc.getSDANumber(id).then((res) => {
      let sdanumber = res;

      this.SubDutyAreaForm.patchValue({ number: sdanumber });
    });
  }

  async saveSubDutyArea() {
    this.showSpinner = true;
    var options = new SubdutyArea();
    options.description = this.SubDutyAreaForm.get('desc')?.value;
    options.subNumber = this.SubDutyAreaForm.get('number')?.value;
    options.dutyAreaId = this.SubDutyAreaForm.get('dutyAreaId')?.value;
    if (
      this.isCopy &&
      this.SubDutyAreaForm.get('title')?.value === this.oldSDA.title
    )
      options.title = this.SubDutyAreaForm.get('title')?.value + ' - Copy';
    else {
      options.title = this.SubDutyAreaForm.get('title')?.value;
    }
    options.effectiveDate = this.SubDutyAreaForm.get('effectiveDate')?.value;
    options.reasonForRevision = this.SubDutyAreaForm.get('reason')?.value;

    this.daSrvc.addSubDutyArea(options.dutyAreaId, options).then((res: any) => {
      this.showSpinner = false;
      if (this.SubDutyAreaForm.get('AddAnother')?.value) {
        this.SubDutyAreaForm.reset();
        this.SubDutyAreaForm
        .get('effectiveDate')
        ?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
      } else {
        this.closed.emit('fp-add-SDA-closed');
      }
      this.refresh.emit();
      this.alert.successToast('SubDuty Area Created Successfully');
      if(this.shouldNavigate || this.isCopy){
        this.dataBroadcastService.navigateOnChange.next({type:"SDA",data:res});
      }
    }).finally(()=>{
      this.showSpinner = false;
    })
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */

  async editSubDutyArea() {
    this.showSpinner = true;
    var options = new SubdutyAreaUpdateOptions();
    options.description = this.SubDutyAreaForm.get('desc')?.value;
    options.subNumber = this.SubDutyAreaForm.get('number')?.value;
    var daId = this.SubDutyAreaForm.get('dutyAreaId')?.value;
    options.sdaId = this.oldSDA.id;
    options.title = this.SubDutyAreaForm.get('title')?.value;
    options.effectiveDate = this.SubDutyAreaForm.get('effectiveDate')?.value;
    options.reasonForRevision = this.SubDutyAreaForm.get('reason')?.value;

    await this.daSrvc
      .updateSubDutyArea(daId, options)
      .then((res: SubdutyArea) => {
        
        this.showSpinner = false;
        if (this.SubDutyAreaForm.get('AddAnother')?.value) {
          this.SubDutyAreaForm.reset();
        } else {
          this.closed.emit('fp-add-SDA-closed');
        }

        if(daId !== this.oldSDA.dutyAreaId){
          this.dataBroadcastService.navigateOnChange.next({type:"SDA",data:res});
        }
        this.refresh.emit();
        this.alert.successToast('SubDuty Area updated Successfully');
      }).finally(()=>{
        this.showSpinner = false;
      });
  }
}
