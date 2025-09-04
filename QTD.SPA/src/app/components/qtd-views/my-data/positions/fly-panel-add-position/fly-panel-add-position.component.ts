import { DatePipe, formatDate } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewContainerRef,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { PositionCreateOptions } from 'src/app/_DtoModels/Position/PositionCreateOptions';
import { PositionUpdateOptions } from 'src/app/_DtoModels/Position/PositionUpdateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-position',
  templateUrl: './fly-panel-add-position.component.html',
  styleUrls: ['./fly-panel-add-position.component.scss'],
})
export class FlyPanelAddPositionComponent implements OnInit {

  @Input() oldPosition: Position;
  @Input() isCopy: boolean;
  @Input() positionCheck:boolean;
  @Input() mode: 'Add' | 'Edit' | 'Copy' = 'Add';
  isEdit = false;
  showSpinner = false;
  AddAnotherdefinitionCategory: boolean = false;
/*   dateError = false; */
  defCategory: any;
  positionForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  iADescription = '';
  iaNote = '';

  fileUploaded = false;
  fileName = '';
  fileData = '';
  addPosition: boolean = true;
  positionNumber:any;
  positionError: boolean = false;

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Output() refreshNavBar = new EventEmitter<any>();
  @Input() shouldNavigate = false;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private positionService: PositionsService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe)
  {}

  ngOnInit(): void {
    this.isEdit = false;
    this.readydefinitionCategoryForm();
    if(this.oldPosition == undefined){
      this.getPositionNumber();
    }

    else if (this.oldPosition !== undefined) {
      this.readyFormsWithData();
    }
    // }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  readyFormsWithData() {
    
    // this.step1Form = this.fb.group({
    //   IssuingAuthority: new FormControl(this.oldProcedure.issuingAuthorityId, [
    //     Validators.required,
    //   ]),
    // });


    this.positionForm.get('posNumber')?.setValue(this.oldPosition.positionNumber);
    this.positionForm.get('posName')?.setValue(this.oldPosition.positionAbbreviation);
    this.positionForm.get('postitle')?.setValue(this.oldPosition.positionTitle);
    this.positionForm.get('posDescription')?.setValue(this.oldPosition.positionDescription);
    this.positionForm.get('hyperlink')?.setValue(this.oldPosition.hyperLink);
    this.positionForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(),'yyyy-MM-dd'));
    this.fileUploaded = true;
     if(this.oldPosition.fileName){

       this.fileName = this.oldPosition.fileName;
       this.fileUploaded = true;
     }else{
      this.fileUploaded = false;
      //this.fileName = "null";
    }



  }

  async getPositionNumber(){
    
    await this.positionService.GetPositionNumber().then((res: any) => {
      
      this.positionForm.get('posNumber')?.setValue(res + 1);
    })
    .catch(() => {

    });
  }

  ngAfterViewInit(): void {}

  closedefinition() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }

 /*  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */

  readydefinitionCategoryForm() {
    this.positionForm = this.fb.group({
      posNumber: new UntypedFormControl('', Validators.required),
      posName: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      postitle: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      posDescription: new UntypedFormControl(''),
      EffectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      posNote: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      hyperlink: new UntypedFormControl(''),
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

  fileChange(file: any) {
    if (!file[0].type.toLowerCase().includes('application/pdf')) {
      this.alert.errorToast('Please Upload a valid pdf file');
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(file[0]);
    reader.onloadend = () => {
      this.fileData = reader.result?.toString() ?? '';
      this.fileName = file[0].name;
      this.fileUploaded = true;
      this.positionForm.get('hyperlink')?.setValue('');
    };
  }

  removeFile() {
    this.fileName = '';
    this.fileData = '';
    this.fileUploaded = false;
  }

  savePosition() {
    
    this.showSpinner = true;
    var data = new PositionCreateOptions();
    data.positionNumber = this.positionForm.get('posNumber')?.value;
    data.positionTitle = this.positionForm.get('postitle')?.value;
    data.positionAbbreviation = this.positionForm.get('posName')?.value;
    /* if (this.isCopy) {
      data.positionTitle =
        data.positionTitle.trim().toLowerCase() ===
          this.oldPosition.positionTitle.trim().toLowerCase()
          ? data.positionTitle.concat('- Copy')
          : data.positionTitle;

    data.positionNumber =
        data.positionNumber ===
          this.oldPosition.positionNumber
          ? data.positionNumber = this.oldPosition.positionNumber + 1
          : data.positionNumber;

    data.positionsFileUpload = this.oldPosition.positionsFileUpload;
    data.fileName = this.oldPosition.fileName;
    } */
    data.positionDescription = this.positionForm.get('posDescription')?.value;
    data.effectiveDate = this.positionForm.get('EffectiveDate')?.value;
    data.ChangeNotes = this.positionForm.get('posNote')?.value;
    data.hyperlink = this.positionForm.get('hyperlink')?.value;
    data.effectiveDate = this.positionForm.get('EffectiveDate')?.value;

    data.isPublished = false;
    if (this.fileUploaded) {
      data.positionsFileUpload = this.fileData;
      data.fileName = this.fileName;
    }
    let newRes:any;

    this.positionService
      .create(data)
      .then(async (res: any) => {
        newRes = res;
        /* this.refreshNavBar.emit();
        this.dataBroadcastService.refreshPositionStats.next(); */
        this.alert.successToast(await this.transformTitle('Position')+'s Saved successfully');
        if(this.positionCheck || this.isCopy){
          this.router.navigate([`/my-data/positions/details/${res.id}`]);
        }

        if(this.shouldNavigate){
          this.dataBroadcastService.navigateOnChange.next({type:"pos",data:res});
        }
        else{
          this.dataBroadcastService.updateMyDataNavBar.next(null);
        }
      })
       .catch(async () => {
        this.alert.errorAlert(await this.transformTitle('Position')+ ' Information Already Exists');
        this.positionError=true;
      })
      .finally(() => {
        this.showSpinner = false;
        this.refreshNavBar.emit();
        this.dataBroadcastService.updatePositionInNavBar.next(null);
        this.dataBroadcastService.refreshPositionData.next(null);
        this.dataBroadcastService.refreshPositionStats.next(null);
        if (this.positionForm.get('AddAnother')?.value === true)
        {
          this.positionForm.get('posNote')?.setValue(' ');
          this.positionForm.reset();
          this.positionForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
          this.fileData = "";
          this.fileName = "";
          this.fileUploaded = false;
          this.getPositionNumber();
        }
        else if(!this.positionError)
        {
          this.closed.emit();
        }
        else if(this.positionError){
          this.positionForm.get('posNote')?.setValue(' ');
          this.positionForm.reset();
          this.positionForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
          this.fileData = "";
          this.fileName = "";
          this.fileUploaded = false;
          this.getPositionNumber();
        }

      /*   if(this.shouldNavigate){
          this.dataBroadcastService.navigateOnChange.next({type:"pos",data:newRes});
        }
        else{
          this.dataBroadcastService.updateMyDataNavBar.next();
        } */
      });
  }

  copyPosition() {
    
    this.showSpinner = true;
    var data = new PositionCreateOptions();
    data.positionNumber = this.positionForm.get('posNumber')?.value;
    data.positionTitle = this.positionForm.get('postitle')?.value;
    data.positionAbbreviation = this.positionForm.get('posName')?.value;
      data.positionTitle =
        data.positionTitle.trim().toLowerCase() ===
          this.oldPosition.positionTitle.trim().toLowerCase()
          ? data.positionTitle.concat('- Copy')
          : data.positionTitle;

    data.positionNumber =
        data.positionNumber ===
          this.oldPosition.positionNumber
          ? data.positionNumber = this.oldPosition.positionNumber + 1
          : data.positionNumber;

    data.positionsFileUpload = this.oldPosition.positionsFileUpload;
    data.fileName = this.oldPosition.fileName;
    data.positionDescription = this.positionForm.get('posDescription')?.value;
    data.effectiveDate = this.positionForm.get('EffectiveDate')?.value;
    data.ChangeNotes = this.positionForm.get('posNote')?.value;
    data.hyperlink = this.positionForm.get('hyperlink')?.value;
    data.effectiveDate = this.positionForm.get('EffectiveDate')?.value;

    data.isPublished = false;
    if (this.fileUploaded) {
      data.positionsFileUpload = this.fileData;
      data.fileName = this.fileName;
    }

    this.positionService
      .copy(this.oldPosition.id,data)
      .then(async (res: any) => {
        /* this.refreshNavBar.emit();
        this.dataBroadcastService.refreshPositionStats.next(); */
        this.alert.successToast(await this.transformTitle('Position') + 's Copied successfully');
          this.router.navigate([`/my-data/positions/details/${res.id}`]);
          this.refreshNavBar.emit();
        this.dataBroadcastService.updatePositionInNavBar.next(null);
        this.dataBroadcastService.refreshPositionData.next(null);
        this.dataBroadcastService.refreshPositionStats.next(null);
        this.closed.emit();
      })
      .catch(async () => {
        this.alert.errorToast(await this.transformTitle('Position') +'s Exists');
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  updatePosition() {
    this.showSpinner = true;
    var data = new PositionUpdateOptions();
    data.id = this.oldPosition.id;
    data.positionNumber = this.positionForm.get('posNumber')?.value;
    data.positionTitle = this.positionForm.get('postitle')?.value;
    data.positionAbbreviation = this.positionForm.get('posName')?.value;

    data.positionDescription = this.positionForm.get('posDescription')?.value;
    data.effectiveDate = this.positionForm.get('EffectiveDate')?.value;

    data.hyperlink = this.positionForm.get('hyperlink')?.value;

    data.isPublished = false;

    if(this.oldPosition.positionsFileUpload){
      data.positionsFileUpload = this.oldPosition.positionsFileUpload;
      data.fileName = this.oldPosition.fileName;
    }

    else if (this.fileUploaded) {
      data.positionsFileUpload = this.fileData;
      data.fileName = this.fileName;
    }
    this.positionService
      .update(this.oldPosition.id, data)
      .then(async (res: Position) => {
        this.alert.successToast(await this.transformTitle('Position')+ ' Successfully Updated');
        this.refreshNavBar.emit();
        this.dataBroadcastService.updatePositionInNavBar.next(null);
        this.dataBroadcastService.refreshPositionData.next(null);
        this.closed.emit('fp-update-pos-closed');
      })
      .catch(async (err: any) => {
        this.alert.errorToast(await this.transformTitle('Position')+ ' Information already exists');
      });
  }

  keyPressNumbers(event: any) {
    var charCode = event.which ? event.which : event.keyCode;
    // Only Numbers 0-9
     if (charCode < 48 || charCode > 57) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }
}
