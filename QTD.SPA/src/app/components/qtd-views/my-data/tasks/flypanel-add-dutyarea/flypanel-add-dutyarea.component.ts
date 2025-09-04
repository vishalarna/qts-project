import { DatePipe, formatDate } from '@angular/common';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { DutyAreaCreateOptions } from 'src/app/_DtoModels/DutyArea/DutyAreaCreateOption';
import { DutyAreaUpdateOptions } from 'src/app/_DtoModels/DutyArea/DutyAreaUpdateOptions';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-add-dutyarea',
  templateUrl: './flypanel-add-dutyarea.component.html',
  styleUrls: ['./flypanel-add-dutyarea.component.scss'],
})
export class FlypanelAddDutyareaComponent implements OnInit, AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  showSpinner: boolean = false;
  dutyAreaForm: UntypedFormGroup;
  datePipe = new DatePipe('en-us');
  /* dateError = false; */
  @Input() DA_Number: any = '';
  @Input() oldDA: DutyArea | undefined;
  @Input() isCopy: boolean = false;
  @Input() shouldNavigate: boolean = false;
  constructor(
    private fb: UntypedFormBuilder,
    private dutyAreaService: DutyAreaService,
    private alert: SweetAlertService,
    private dataBroadcastService:DataBroadcastService,
  ) {}

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {

    this.readyDutyAreaForm();
    if (this.oldDA) {

      this.DA_Number = this.oldDA.number;
      this.dutyAreaForm.patchValue({
        title: this.oldDA.title,
        desc: this.oldDA.description,
        number: this.oldDA.number,
        letter: this.oldDA.letter,
        reason: "",
        effectiveDate: this.datePipe.transform(
          Date.now(),
          'yyyy-MM-dd'
        ),
      });
      this.dutyAreaForm.updateValueAndValidity();
      if (this.isCopy) this.getDANumber();


    }
    if (!this.DA_Number || (this.DA_Number === '' && !this.oldDA)) {

      this.getDANumber();
    }
  }

  readyDutyAreaForm() {
    this.dutyAreaForm = this.fb.group({
      letter: new UntypedFormControl('§', Validators.required),
      number: new UntypedFormControl(this.DA_Number, [Validators.required]),
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

  saveDutyArea() {
    this.showSpinner = true;
    var options = new DutyAreaCreateOptions();
    options.title = this.dutyAreaForm.get('title')?.value;
    options.description = this.dutyAreaForm.get('desc')?.value;
    options.number = this.dutyAreaForm.get('number')?.value;
    options.letter = this.dutyAreaForm.get('letter')?.value;
    options.reasonForRevision = this.dutyAreaForm.get('reason')?.value;
    options.effectiveDate = this.dutyAreaForm.get('effectiveDate')?.value;
    this.dutyAreaService
      .create(options)
      .then((res: DutyArea) => {
        this.showSpinner = false;
        if (this.dutyAreaForm.get('AddAnother')?.value) {
          this.dutyAreaForm.reset();
          this.dutyAreaForm.patchValue({ letter: '§' });
          this.dutyAreaForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
          this.getDANumber();
        } else {
          this.closed.emit('fp-add-DA-closed');
        }
        this.refresh.emit();
        this.alert.successToast('Duty Area Saved Successfully');
        if(this.shouldNavigate){
          this.dataBroadcastService.navigateOnChange.next({type:"DA",data:res});
        }
      })
      .catch((err: any) => {
       // this.alert.errorToast('Failed To Save Duty Area ' + err);
       this.showSpinner = false;
       this.alert.errorToast('Duty Area with The Same Number Already Exists');
      });
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  }
 */
  getDANumber() {
    let letter: string = this.dutyAreaForm.get('letter')?.value ?? '';
    if (letter === '' || !letter) {
      this.dutyAreaForm.patchValue({ letter: '' });
      this.dutyAreaForm.patchValue({ number: this.DA_Number });
      return;
    }
    // else if(this.oldDA !== undefined && !this.isCopy){
    //   this.dutyAreaForm.patchValue({ letter: '' });
    //   this.dutyAreaForm.patchValue({ number: this.DA_Number-1 });
    //   return;
    // }

    this.dutyAreaService.getDANumber(letter).then((res) => {
      this.dutyAreaForm.patchValue({ number: res });
    });
  }

  async editDutyArea() {
    this.showSpinner = true;
    let options: DutyAreaUpdateOptions = new DutyAreaUpdateOptions();
    options.title = this.dutyAreaForm.get('title')?.value;
    options.description = this.dutyAreaForm.get('desc')?.value;
    options.number = this.dutyAreaForm.get('number')?.value;
    options.letter = this.dutyAreaForm.get('letter')?.value;
    options.reasonForRevision = this.dutyAreaForm.get('reason')?.value;
    options.effectiveDate = this.dutyAreaForm.get('effectiveDate')?.value;

    await this.dutyAreaService.update(this.oldDA?.id, options).then((res) => {
      this.showSpinner = false;
      this.alert.successToast('Duty Area updated successfully');
      /*  if (this.dutyAreaForm.get('AddAnother')?.value == true) {
        this.oldDA = undefined;
        this.dutyAreaForm.reset();
      } else */ this.closed.emit('da-updated');
      this.refresh.emit();
    }).finally(()=>{
      this.showSpinner = false;
    })

  }

  copyDutyArea() {
    this.showSpinner = true;
    var options = new DutyAreaCreateOptions();
    options.title =
      String(this.dutyAreaForm.get('title')?.value).toLowerCase().trim() ===
      this.oldDA?.title.toLowerCase().trim()
        ? this.dutyAreaForm.get('title')?.value + ' - Copy'
        : this.dutyAreaForm.get('title')?.value;

    options.description = this.dutyAreaForm.get('desc')?.value;
    options.number = this.dutyAreaForm.get('number')?.value;
    options.letter = this.dutyAreaForm.get('letter')?.value;
    options.reasonForRevision = this.dutyAreaForm.get('reason')?.value;
    options.effectiveDate = this.dutyAreaForm.get('effectiveDate')?.value;
    this.dutyAreaService.create(options).then((res: any) => {
      this.showSpinner = false;
      if (this.dutyAreaForm.get('AddAnother')?.value) {
        this.dutyAreaForm.reset();
      } else {
        this.closed.emit('fp-add-DA-closed');
      }
      this.dataBroadcastService.navigateOnChange.next({type:"DA",data:res});
      this.refresh.emit();
      this.alert.successToast('Duty Area Saved Successfully');
    });
  }

  async inputChange(event:string){

  }
}
