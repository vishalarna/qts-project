import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe, formatDate } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  OnDestroy, AfterViewInit
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ToolCreateOptions } from 'src/app/_DtoModels/Tool/ToolCreateOptions';
import { ToolCategory } from 'src/app/_DtoModels/ToolCategory/ToolCategory';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-add-tool',
  templateUrl: './fly-panel-add-tool.component.html',
  styleUrls: ['./fly-panel-add-tool.component.scss'],
})
export class FlyPanelAddToolComponent implements OnInit,  OnDestroy, AfterViewInit {
  @Input() oldToolData:any;
  @Input() isCopy:any;
  @Output() emitSavedToolID = new EventEmitter<string>();
  addAnother: boolean = false;
  addTool: boolean = true;
  addCategory: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refreshSHTreeData = new EventEmitter();
  @Output() refreshCategories = new EventEmitter<any>();
  showSpinner: boolean = false;
  step1Form: UntypedFormGroup;
  fileUploaded = false;
  fileName = '';
  fileData = '';
  file: File;
  subscriptions = new SubSink();
  stepperOrientation: Observable<StepperOrientation>;
  toolForm: UntypedFormGroup;
  categories: ToolCategory[];
  toolId:any;
/*   dateError = false; */
  datePipe = new DatePipe('en-us');
  @ViewChild(MatStepper) stepper: MatStepper;

  constructor(
    private fb: UntypedFormBuilder,
    public breakpointObserver: BreakpointObserver,
    public alert: SweetAlertService,
    private toolSrvc: ToolsService,
    private route : ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.readyForm();
    this.getAllCategories();
  }

  ngAfterViewInit():void{


    if(this.oldToolData !== undefined){
      this.readyEditCopyForm();
    }
  }

  ngOnDestroy(): void {

  }

  readyEditCopyForm(){

    this.toolForm.get('categoryId')?.setValue(this.oldToolData.toolCategoryId);

    this.toolForm.get('number')?.setValue(this.oldToolData.number);
    this.toolForm.get('name')?.setValue(this.oldToolData.name);
    this.toolForm.get('description')?.setValue(this.oldToolData.description);
    this.toolForm.get('hyperlink')?.setValue(this.oldToolData.hyperlink);

    this.toolForm.get('notes')?.setValue(this.oldToolData.notes);

    this.toolForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
  }

  readyForm() {
    this.toolForm = this.fb.group({
      categoryId: new UntypedFormControl('', [Validators.required]),
      number: new UntypedFormControl('', [Validators.required]),
      name: new UntypedFormControl('', [Validators.required]),
      description: new UntypedFormControl(''),
      notes: new UntypedFormControl('', Validators.required),
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      hyperlink: new UntypedFormControl(''),
    });
  }

  closePanel() {
    this.addCategory = false;
    this.addTool = true;
    this.getAllCategories();
  }

  openPanel() {
    this.addCategory = true;
    this.addTool = false;
  }


 /*  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  }
 */
  async saveTool() {
    
 if(this.isCopy === true){
        var number = await this.toolSrvc.getNumber(this.toolForm.get('categoryId')?.value)
        var option: ToolCreateOptions = {
          toolCategoryId: this.toolForm.get('categoryId')?.value,
          number: number.toString(),
          name: this.toolForm.get('name')?.value + ' - Copy',
          hyperlink: this.toolForm.get('hyperlink')?.value,
          upload: this.fileData,
          effectiveDate: this.toolForm.get('effectiveDate')?.value,
          notes : this.toolForm.get('notes')?.value,
          description :  this.toolForm.get('description')?.value
        };
    }
    else{
        var option: ToolCreateOptions = {
          toolCategoryId: this.toolForm.get('categoryId')?.value,
          number: this.toolForm.get('number')?.value,
          name: this.toolForm.get('name')?.value,
          hyperlink: this.toolForm.get('hyperlink')?.value,
          upload: this.fileData,
          effectiveDate: this.toolForm.get('effectiveDate')?.value,
          notes : this.toolForm.get('notes')?.value,
          description :  this.toolForm.get('description')?.value
        };
    }

    await this.toolSrvc.create(option).then(async (res) => {
      if (this.addAnother) {
        this.resetAll();
      } else {
        this.closed.emit('tool-added-from-fp');
        this.emitSavedToolID.emit(res.id);
      }
      this.refreshCategories.emit();
      if(this.isCopy===true) {
        this.alert.successToast( await this.labelPipe.transform('Tool') + ' Data Copied successfully');
        this.router.navigate([`/my-data/tools/detail/${res.id}`]);
      }else{
        this.alert.successToast(await this.labelPipe.transform('Tool') +' and ' + await this.labelPipe.transform('Tool') + ' Status History added successfully');
      }

    });
  }

  resetAll(){

    this.toolForm.reset();
    this.toolForm.get('name')?.setValue('');
    this.toolForm.get('description')?.setValue('');
    this.toolForm.get('notes')?.setValue('');
    /* ({
      name: '',
      description: '',
      notes:''
    }); */
    this.stepper.reset();
    this.addAnother = false
    this.toolForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
  }
  async getAllCategories() {
    await this.toolSrvc.getAllToolCategories(false).then((res) => {
      this.categories = res;
    });
  }

  async getNumber(data:any){
    var number = await this.toolSrvc.getNumber(data.value);
    this.toolForm.patchValue({ number: number })
  }

  editTool(){
    
    this.showSpinner = true;
    var options = new ToolCreateOptions();
    options.toolCategoryId =this.toolForm.get('categoryId')?.value;
    options.description = this.toolForm.get('description')?.value;
    options.number = this.toolForm.get('number')?.value;
    options.name = this.toolForm.get('name')?.value;
    options.notes = this.toolForm.get("notes")?.value;
    options.effectiveDate = this.toolForm.get("effectiveDate")?.value;
    options.hyperlink = this.toolForm.get("hyperlink")?.value;


    this.toolSrvc.updateTool(this.oldToolData.id,options).then(async (res:any)=>{
      this.showSpinner = false;
      this.closed.emit('fp-add-sh-cat-closed');
      this.refreshCategories.emit();
      this.alert.successToast("Successfull Edited " + await this.labelPipe.transform('Tool') + " Data ");
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }).finally(()=>{
      this.showSpinner = false;
    })
  }
}
