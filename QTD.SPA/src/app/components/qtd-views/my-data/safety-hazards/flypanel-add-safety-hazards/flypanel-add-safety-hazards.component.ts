import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe, formatDate } from '@angular/common';
import {
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Procedure_SaftyHazard_Link } from 'src/app/_DtoModels/Procedure_SaftyHazard_Link/Procedure_SaftyHazard_Link';
import { Procedure_SaftyHazard_LinkOptions } from 'src/app/_DtoModels/Procedure_SaftyHazard_Link/Procedure_SaftyHazard_LinkOptions';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { SaftyHazardCreateOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCreateOptions';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { SafetyHazardCategoryService } from 'src/app/_Services/QTD/safety-hazard-category.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { SaftyHazard_SetCreateOptions } from 'src/app/_DtoModels/SaftyHazard_Set/SaftyHazard_SetCreateOptions';
import { SafetyHazardSetService } from 'src/app/_Services/QTD/safety-hazard-set.service';
import { MatStepper } from '@angular/material/stepper';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { SaftyHazard_Tool_LinkOptions } from 'src/app/_DtoModels/SaftyHazard_Tool/SaftyHazard_Tool_LinkOptions';
import { SaftyHazardWithSet } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardWithSet';
import { SafetyHazard_Set } from 'src/app/_DtoModels/SaftyHazard_Set/SafetyHazard_Set';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-flypanel-add-safety-hazards',
  templateUrl: './flypanel-add-safety-hazards.component.html',
  styleUrls: ['./flypanel-add-safety-hazards.component.scss'],
})
export class FlypanelAddSafetyHazardsComponent
  implements OnInit, OnDestroy, AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refreshSHTreeData = new EventEmitter();
  @Input() oldSHId: any;
  @Input() makeCopy: boolean = false;
  @Input() safetyHazardCheck:boolean;
  @Input() shouldNavigate = false;

  @ViewChild("ckeditor") ckeditor: any;
  onEventOrRequest(event: any) {
    this.ckeditor.instance.setData('');
  }

  mainSpinner = false;
  Editor = ckcustomBuild;
  showSpinner: boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  step1Form: UntypedFormGroup;
  step2Form: UntypedFormGroup;
  step3Form: UntypedFormGroup;
  setForm: UntypedFormGroup = new UntypedFormGroup({});
  procId = '';
  fileUploaded = false;
  fileName = '';
  fileData = '';
  subscriptions = new SubSink();
/*   dateError = false; */
  datePipe = new DatePipe('en-us');
  setList: any[] = ["0"];

  selectedCatId = '';
  resetForm: boolean = false;
  addCategory: boolean = false;
  PPEControl = new UntypedFormControl([]);
  PPEs: Tool[] = [];
  PPEList: any[] = [];
  oldSHTitle = "";
  oldSHNumber = "";
  addAnotherCheck:boolean=false;
  addTool:boolean=false;
  isLoading:boolean=false;
  SHNumber: number = 6;
  addSH: boolean = true;
  ppeList: any[] = [];
  categories: any[];
  @ViewChild(MatStepper) stepper: MatStepper;

  constructor(
    private fb: UntypedFormBuilder,
    private positionService: PositionsService,
    public breakpointObserver: BreakpointObserver,
    public shCatService: SafetyHazardCategoryService,
    public alert: SweetAlertService,
    public sHService: SafetyHazardsService,
    public route: ActivatedRoute,
    public procService: ProceduresService,
    public dataBroadcastService: DataBroadcastService,
    public shSetService: SafetyHazardSetService,
    public toolService: ToolsService,
    private router: Router,
    private cdr : ChangeDetectorRef,
    private labelPipe:LabelReplacementPipe
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.readyStep1Form();
    this.readyStep2Form();
    this.readyStep3Form();
    this.readySetForm();
    this.cdr.detach();

    setInterval(()=>{
      this.cdr.detectChanges();
    },20);
  }

  ngAfterViewInit(): void {
    this.populateCatData();
    this.subscriptions.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
    });

    this.readyTools();


    if (this.oldSHId !== undefined) {
      this.readyFormData();
    }
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; 
    } else {
      return { whitespaceOnly: true }; 
    }
  }

  async readyFormData() {
    this.mainSpinner = true;
    await this.sHService.getWithSet(this.oldSHId).then((res: SaftyHazardWithSet) => {
      this.oldSHNumber = res.saftyHazard.number;
      this.oldSHTitle = res.saftyHazard.title;
      this.setFormData(res);
    }).finally(() => {
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  setFormData(data: SaftyHazardWithSet) {
    this.step1Form.get('categoryId')?.setValue(data.saftyHazard.saftyHazardCategoryId);

    this.step2Form.get('SHDesc')?.setValue(data.saftyHazard.text);
    this.step2Form.get('SHName')?.setValue(data.saftyHazard.title);
    this.step2Form.get('hyperlink')?.setValue(data.saftyHazard.hyperLinks);
    this.step2Form.get('SHNumber')?.setValue(data.saftyHazard.number);
    this.step2Form.get('RevisionNumber')?.setValue(data.saftyHazard.revisionNumber);

    this.step3Form.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));

    data.safetyHazard_Sets.forEach((set: SafetyHazard_Set, i: any) => {
      this.setForm.get(`Abatement${i}`)?.setValue(set.safetyHzAbatementText);
      this.setForm.get(`Controls${i}`)?.setValue(set.safetyHzControlsText);
      if ((i + 1) < data.safetyHazard_Sets.length) {
        this.setForm.addControl(`Abatement${i + 1}`, new UntypedFormControl());
        this.setForm.addControl(`Controls${i + 1}`, new UntypedFormControl());
        this.setList.push("0");
      }
    });

    // the objects mismatch with the original PPE so we need to filter out original objects
    var PPEData = this.PPEs.filter((ppe: any) => {
      return data.tools.filter((tool: Tool) => {
        return tool.id === ppe.id;
      }).length > 0;
    })

    this.PPEControl = new UntypedFormControl([...PPEData]);
    this.PPEList.push(this.PPEControl);
    this.fileData = data.saftyHazard.files;
    this.fileUploaded = (this.fileData?.length ?? 0) > 0;
    this.fileName = data.saftyHazard.fileName;
    this.mainSpinner = false;
  }

  async readyTools() {
    await this.toolService.getAll().then((res: Tool[]) => {
      this.PPEs = res;
    });
    this.ppeList.push(this.PPEControl);
  }

  async populateCatData() {
    this.isLoading=true;
    await this.shCatService
      .getAll()
      .then((res: SaftyHazard_Category[]) => {
        this.categories = res;
      })
      .catch(async (err: any) => {
        this.isLoading=false;
        this.alert.errorToast('Error Fetching '+await this.labelPipe.transform("Safety Hazard")+' Categories ' + err);
      });
  }

  selectedChanged(event: any) {
   /*  if (event.selectedIndex === 2) {
      this.addAnotherCheck = true;
    } */
    event.selectedIndex === 2 ? this.addAnotherCheck = true : this.addAnotherCheck = false;
  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      categoryId: new UntypedFormControl('', [Validators.required]),
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      SHNumber: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      SHDesc: new UntypedFormControl(''),
      SHName: new UntypedFormControl('',[Validators.required,this.whitespaceOnlyValidator]),
      PPE: new UntypedFormControl(''),
      hyperlink: new UntypedFormControl(''),
      RevisionNumber : new UntypedFormControl('')
    });
  }

  readySetForm() {
    this.setList.forEach((data: any, i: any) => {
      this.setForm.addControl(`Abatement${i}`, new UntypedFormControl());
      this.setForm.addControl(`Controls${i}`, new UntypedFormControl());
    });
  }

  readyStep3Form() {
    this.step3Form = this.fb.group({
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), 'yyyy-MM-dd')),
      reason: new UntypedFormControl('',[Validators.required,this.whitespaceOnlyValidator]),
      addNew: new UntypedFormControl({ value: false, disabled: this.oldSHId !== undefined }),
    });
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */

  async getCategories(id: any) {
    this.selectedCatId = id;
    this.sHService
      .getShWithSHCatId(this.selectedCatId)
      .then((res: SaftyHazard_Category[]) => {
        this.SHNumber = res.length + 1;
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Fetching '+await this.labelPipe.transform("Safety Hazard")+' Number Data ' + err.message);
      });
  }

  openCategoryPanel() {
    
    this.addCategory = true;
    this.addSH = false;
  }

  openSubCategoryPanel() {
    this.addCategory = false;
    this.addSH = false;
  }

  openTopicPanel() {
    this.addCategory = false;
    this.addSH = false;
  }

  closePanel() {
    this.addCategory = false;
    this.addSH = true;
  }

  closeToolPanel(){
    this.addTool = false;
    this.addSH = true;
  }

  async saveSafetyHazard() {
    this.showSpinner = true;
    var options = new SaftyHazardCreateOptions();
    options.saftyHazardCategoryId = this.step1Form.get('categoryId')?.value;
    options.description = this.step2Form.get('SHDesc')?.value;
    options.number = this.step2Form.get('SHNumber')?.value;
    options.title = this.step2Form.get('SHName')?.value;
/*     if (this.makeCopy) {
      options.number = options.number.trim().toLowerCase() === this.oldSHNumber.trim().toLowerCase() && options.number.concat("-C").length <= 20
        ? options.number.concat("-Copy") : options.number;
      options.title = options.title.trim().toLowerCase() === this.oldSHTitle.trim().toLowerCase()
        ? options.title.concat("-Copy") : options.title;
    } */
    options.text = this.step2Form.get('SHDesc')?.value;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    options.files = this.fileData;
    options.hyperLinks = this.step2Form.get('hyperlink')?.value;
    options.changeNotes = this.step3Form.get('reason')?.value;
    options.revisionNumber = this.step2Form.get('RevisionNumber')?.value;
    if (this.fileUploaded) {
      options.files = this.fileData;
      options.fileName = this.fileName;
    }
    await this.sHService
      .create(options)
      .then(async (res: SaftyHazard) => {
        this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")} Saved Successfully `);
        this.refreshSHTreeData.emit();
       // this.dataBroadcastService.updateMyDataNavBar.next();
        this.saveSet(res.id);
        this.linkTools(res.id);

        if (this.step3Form.get('addNew')?.value) {
          this.step1Form.reset();
          this.step2Form.reset();
          this.step3Form.reset();
          this.resetPPEAndSet();
          this.stepper.reset();
          this.step3Form.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
        } else {
          this.closed.emit('fp-add-eo-closed');
          if(this.safetyHazardCheck){
            this.router.navigate([`/my-data/safety-hazards/sh/${res.id}`]);
          }
        }
        if(this.shouldNavigate){
          this.dataBroadcastService.navigateOnChange.next({type:"sh",data:res});
        }
        else{
          this.dataBroadcastService.updateMyDataNavBar.next(null);
        }
      }).finally(() => {
        this.showSpinner = false;
      });
  }

  async copySafetyHazard(){
    
    this.showSpinner = true;
    var options = new SaftyHazardCreateOptions();
    options.saftyHazardCategoryId = this.step1Form.get('categoryId')?.value;
    options.description = this.step2Form.get('SHDesc')?.value;
    options.number = this.step2Form.get('SHNumber')?.value;
    options.title = this.step2Form.get('SHName')?.value;
    options.number = options.number.trim().toLowerCase() === this.oldSHNumber.trim().toLowerCase() && options.number.concat("-C").length <= 20
      ? options.number.concat("-Copy") : options.number;
    options.title = options.title.trim().toLowerCase() === this.oldSHTitle.trim().toLowerCase()
      ? options.title.concat("-Copy") : options.title;
    options.text = this.step2Form.get('SHDesc')?.value;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    options.files = this.fileData;
    options.hyperLinks = this.step2Form.get('hyperlink')?.value;
    options.changeNotes = this.step3Form.get('reason')?.value;
    options.revisionNumber = this.step2Form.get('RevisionNumber')?.value;
    if (this.fileUploaded) {
      options.files = this.fileData;
      options.fileName = this.fileName;
    }
    await this.sHService
      .copy(this.oldSHId,options)
      .then(async (res: SaftyHazard) => {
        this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")} Copied Successfully `);
        this.refreshSHTreeData.emit();
        this.dataBroadcastService.updateMyDataNavBar.next(null);
        this.saveSet(res.id);
        this.linkTools(res.id);
        if (this.resetForm) {
          this.step1Form.reset();
          this.step2Form.reset();
          this.step3Form.reset();
          this.resetPPEAndSet();
          this.stepper.reset();
          this.step3Form.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
        } else {
          this.closed.emit('fp-add-eo-closed');
          this.router.navigate([`/my-data/safety-hazards/sh/${res.id}`]);
          this.dataBroadcastService.navigateOnChange.next({type:"sh",data:res});
        }
      }).finally(() => {
        this.showSpinner = false;
      });
  }

  async updateSafetyHazard() {
    this.showSpinner = true;
    var options = new SaftyHazardCreateOptions();
    options.saftyHazardCategoryId = this.step1Form.get('categoryId')?.value;
    options.description = this.step2Form.get('SHDesc')?.value;
    options.number = this.step2Form.get('SHNumber')?.value;
    options.title = this.step2Form.get('SHName')?.value;
    options.text = this.step2Form.get('SHDesc')?.value;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    options.files = this.fileData;
    options.hyperLinks = this.step2Form.get('hyperlink')?.value;
    options.changeNotes = this.step3Form.get('reason')?.value;
    options.revisionNumber = this.step2Form.get('RevisionNumber')?.value;
    if (this.fileUploaded) {
      options.files = this.fileData;
      options.fileName = this.fileName;
    }
    await this.sHService.update(this.oldSHId, options).then(async (res: any) => {
      this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")} Saved Successfully `);
      this.refreshSHTreeData.emit();
      this.dataBroadcastService.updateProcSHLink.next(null);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.saveSet(res.id);
      this.linkTools(res.id);
      this.closed.emit('fp-add-eo-closed');
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  linkTools(id: any) {
    var options = new SaftyHazard_Tool_LinkOptions();;
    options.toolIds = this.PPEControl.value.map((data: Tool) => {
      return data.id;
    });
    this.sHService.linkTools(id, options)
      .then((res: any) => { });
  }

  saveSet(id: any) {
    var options = new SaftyHazard_SetCreateOptions();
    this.setList.forEach(async (data: any, i: any) => {
      options.safetyHzAbatementText = this.setForm.get(`Abatement${i}`)?.value;
      options.safetyHzControlsText = this.setForm.get(`Controls${i}`)?.value;

      if (options.safetyHzAbatementText !== null && options.safetyHzControlsText !== null) {
        await this.shSetService
          .createAndLink(id, options)
          .then((res: any) => { });
      }
    })
  }

  resetPPEAndSet() {
    const length = this.setList.length;
    this.setList = ["0"];
    for (var i = 1; i < length; i++) {
      this.setForm.removeControl(`Abatment${i}`);
      this.setForm.removeControl(`Controls${i}`);
    }
    this.setForm.reset();
    this.PPEControl = new UntypedFormControl([]);
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
      this.step2Form.get('hyperlink')?.setValue('');
    };
  }

  removeFile() {
    this.fileName = '';
    this.fileData = '';
    this.fileUploaded = false;
  }

  refreshCategoryList() {
    this.refreshSHTreeData.emit();
    this.populateCatData();
  }

  removePPE(i: any) {
    const tool = this.PPEControl.value as Tool[];
    this.removeFirst(tool, i);
    this.PPEControl.setValue(tool);
  }

  private removeFirst(array: Tool[], toRemove: Tool): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  OnClick(selected: boolean) {
    if (selected) {
      this.PPEList.push(this.PPEControl);
    }
  }

  addSet() {
    const length = this.setList.length;
    this.setForm.addControl(`Abatement${length}`, new UntypedFormControl());
    this.setForm.addControl(`Controls${length}`, new UntypedFormControl());
    this.setList.push("0");
  }

  removeSet() {
    this.setList.pop();
    const length = this.setList.length;
    this.setForm.removeControl(`Abatment${length}`);
    this.setForm.removeControl(`Controls${length}`);
  }

  openToolPanel(){
    this.addTool = true;
    this.addSH = false;
  }
}
