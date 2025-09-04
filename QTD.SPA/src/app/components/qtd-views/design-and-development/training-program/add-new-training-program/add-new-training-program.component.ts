import { SelectionModel } from '@angular/cdk/collections';
import { BreakpointObserver } from '@angular/cdk/layout';
import { TemplatePortal } from '@angular/cdk/portal';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe, formatDate } from '@angular/common';
import { ChangeDetectorRef, Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStepper } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ILAWithCountOptions } from 'src/app/_DtoModels/ILA/ILAWithCountOptions';
import { TrainingProgramCreateOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgramCreateOptions';
import { TrainingProgram_HistoryCreateOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram_HistoryCreateOptions';
import { TrainingProgram_ILA_LinkCreateOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram_ILA_LinkCreateOptions';
import { TrainingProgramType } from 'src/app/_DtoModels/TrainingProgramType/TrainingProgramType';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TrainingProgramTypeService } from 'src/app/_Services/QTD/training-program-type.service';
import { TrainingProgramsService } from 'src/app/_Services/QTD/training-programs.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose, sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';

export interface PeriodicElement {
  name: string;
  position: number;
}
export interface ILA {
  number: number;
  title: string;
}
export interface ILAWithoutSelect {
  number: number;
  title: string;
}
const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'LSO Lead System Operator'},
  {position: 2, name: 'RM- Reliability Manager'}
];
const ELEMENT_DATA_ILA: ILA[] = [
  {number: 1, title: 'OJT Train the Trainer'},
  {number: 2, title: 'Introducing the Employee Portal'}
];
const ELEMENT_DATA_ILA_WS: ILAWithoutSelect[] = [
  {number: 1, title: 'OJT Train the Trainer'},
  {number: 2, title: 'Introducing the Employee Portal'}
];
@Component({
  selector: 'app-add-new-training-program',
  templateUrl: './add-new-training-program.component.html',
  styleUrls: ['./add-new-training-program.component.scss']
})
export class AddNewTrainingProgramComponent implements OnInit {
  step1Form!: UntypedFormGroup;
  toggleMenu: Observable<string>;
  dateError = false;
  stepperOrientation: Observable<StepperOrientation>;
  trainingProgramList:TrainingProgramType[]=[];
  datePipe = new DatePipe('en-us');
  trainingProgramType:number=0;
  currentIndex :number=0;
  positionId:any;
  trainingProgramId:any;
  fromCopiedTrainingId:any
  positionName:string;
  srcList: ILAWithCountOptions[] = [];
  unlinkIds: any[] = [];
  ilaIds :any[] = [];
  unlinkDescription:any;
  trainingProgram:any;
  mode: string = 'add';
  temp:any;
  savecopyID:any;
  editPositionData:any=[];
  yearOptions: number[] = [];
  isDateTimeValid: boolean;
  @ViewChild('stepper') stepper: MatStepper;
  constructor(private router:Router,
    private store: Store<{ toggle: string }>,
    public changeDetector: ChangeDetectorRef,
    public breakpointObserver: BreakpointObserver,
    private fb: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private trainingPrgmtypesrvc: TrainingProgramTypeService,
    private trainingProgramsrvc: TrainingProgramsService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private cdRef: ChangeDetectorRef,
    private labelPipe: LabelReplacementPipe) {

      this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

    }
    displayedColumns: string[] = ['num','position'];
    //dataSource = ELEMENT_DATA;
    displayedColumnsILA: string[] = ['id', 'number', 'description','action'];
    dataSourceILA : MatTableDataSource<any>;
    selection = new SelectionModel<any>(true, []);
    alreadyLinkedIdsILa : any[] = [];
    displayedColumnsILAWS: string[] = ['number', 'description'];
    dataSourceILAWS : MatTableDataSource<any>;
    alreadyLinkedIds:any=[];
    posTableArray:any=[];
    dataSourcePositions : MatTableDataSource<positionsTableData>;

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    this.readytrainingDetailsForm();
    this.toggleMenu = this.store.select('toggle');
    this.changeDetector.detectChanges();
    this.generateYearOptions();
    let positionData: { num: number, position: string }[] = [];
    this.dataSourcePositions = new MatTableDataSource(positionData);
    this.setupTimeField('startdate', 'startTime');
    this.setupTimeField('endDate', 'endTime');

//Check for Edit or Copy Mode
let segments = this.router.url.split('/');

if (segments.includes('edit')) 
{
  this.mode = 'edit';
  this.trainingProgramId = segments[segments.length - 1];
   this.savecopyID = segments[segments.length -1];
  this.getTrainingProgramDetails()
}
else if((segments.includes('copy')))
{
  this.mode = 'copy';
  this.trainingProgramId = segments[segments.length - 1];
  this.savecopyID = segments[segments.length -1];
  this.fromCopiedTrainingId = segments[segments.length - 1];
  this.getTrainingProgramDetails()
}
else
{
  this.getTrainingProgramTypes();
}
  }

  generateYearOptions() {
    const currentYear = new Date().getFullYear();
    for (let year = 2016; year <= currentYear; year++) {
      this.yearOptions.push(year);
    }
  }
  
  async goBack() {
    // if (this.previewContainer == 'edit') this.closeGuideEditor();
    //else await 
    this.router.navigate(['dnd/trainingprogram']);
  }
  
  toggleMainMenu() {
    //this.databroadcastSrvc.ToggleMainMenu.next('');
    this.store.dispatch(sideBarToggle())
  }

  yearChanged(event: any) {
    const selectedYear = event.value;
    this.step1Form.get('year')?.setValue(selectedYear);
  }

  readytrainingDetailsForm() 
  {
    this.step1Form = this.fb.group({
      trainingProgtype: new UntypedFormControl('', Validators.required),
      title: new UntypedFormControl('', Validators.required),
      startdate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), Validators.required),
      startTime: new UntypedFormControl({ value: '', disabled: true }, Validators.required),
      endDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      endTime: new UntypedFormControl({ value: '', disabled: true }, Validators.required),
      description: new UntypedFormControl(),
      version: new UntypedFormControl('',Validators.required),
      year:new UntypedFormControl('', Validators.required),
      startDateTime: new UntypedFormControl(''),
      endDateTime: new UntypedFormControl(''),
    });
  }
  
  private setupTimeField(dateControlName: string, timeControlName: string): void {
    const dateControl = this.step1Form.get(dateControlName);
    const timeControl = this.step1Form.get(timeControlName);
  
    dateControl?.valueChanges.subscribe(date => {
      if (date) {
        if (!timeControl?.value) {
          timeControl?.setValue('08:00');
        }
        timeControl?.enable();
      } else {
        timeControl?.reset();
        timeControl?.disable();
      }
    });
    const initialDate = dateControl?.value;
    if (initialDate) {
      if (!timeControl?.value) {
        timeControl?.setValue('08:00');
      }
      timeControl?.enable();
    }
  }
  addPositionData:any;
  async selectedChanged(event: any) {
    if (event.selectedIndex === 0) 
    {
      this.currentIndex = 0;
      if(this.trainingProgramId != undefined)
      {
        await this.trainingProgramsrvc.get(this.trainingProgramId).then((res)=> 
        {
        
          let positionData: { num: number, position: string }[] = [
            { "num": 1, "position":  res.position.positionTitle},
            
        ];
          
          this.dataSourcePositions = new MatTableDataSource(positionData);
          
        })
      }
      
    }
    else if (event.selectedIndex === 1) 
    {
      await this.trainingProgramsrvc.getLinkedILAWithCount(this.trainingProgramId).then((res) => 
      {
        this.srcList = res;
        this.srcList.forEach((ila)=>{
          this.alreadyLinkedIds.push(ila.id);
        });
    
        this.dataSourceILA = new MatTableDataSource(this.srcList);
        this.dataSourceILAWS = new MatTableDataSource(res);

      });
      this.currentIndex = 1;
    }
    else if (event.selectedIndex === 2) 
    {
      this.currentIndex = 2;
    }
    
  }
  dateChanged(event: any) {
    const timeControl = this.step1Form.get('startTime');
    if (event) {
      if (!timeControl?.value) {
        timeControl?.setValue('08:00');
      }
      timeControl?.enable();
    } else {
      timeControl?.reset();
      timeControl?.disable();
    }
  }

  endDateChanged(date: any): void {
    const timeControl = this.step1Form.get('endTime');
    if (date) {
      if (!timeControl?.value) {
        timeControl?.setValue('08:00');
      }
      timeControl?.enable();
    } else {
      timeControl?.reset();
      timeControl?.disable();
    }
  }
  async openFlyInPanel(templateRef: any) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  refreshILAData(){
    this.selection.clear();
    this.ilaIds = [];
    this.unlinkIds = [];
    this.readyILALinkedData();
  }
  async readyILALinkedData(){
    this.alreadyLinkedIds = [];
    this.srcList = await this.trainingProgramsrvc.getLinkedILAWithCount(this.trainingProgramId);
    
    this.srcList.forEach((ila)=>{
      this.alreadyLinkedIds.push(ila.id);
    });

    this.dataSourceILA = new MatTableDataSource(this.srcList);
    this.dataSourceILAWS = new MatTableDataSource(this.srcList);
  }

  getTrainingProgramTypes()
  {
      this.trainingPrgmtypesrvc.getAll().then((res) => {
        
        
        this.trainingProgramList = res;
        
        this.trainingProgramType = 1;
        if(this.temp)
        {
          this.chnagetrainingProgramType(this.temp)
        }
        //this.filteredList = res;
      });
  }

  trainingProgramTypeName:any;
  chnagetrainingProgramType(event: any)
  {
    
    
    var title = this.trainingProgramList.filter((data)=> data.id === event.value)[0].trainingProgramTypeTitle
    this.trainingProgramTypeName = title;
    if(title.toLowerCase() === 'initial training program')
    {
      this.trainingProgramType = 1
      this.step1Form.controls['year']?.clearValidators();
      this.step1Form.controls['year']?.updateValueAndValidity();
      this.step1Form.controls['title']?.clearValidators();
      this.step1Form.controls['title']?.updateValueAndValidity();
      this.step1Form.controls['version']?.setValidators(Validators.required);
      this.step1Form.controls['version']?.updateValueAndValidity();
      this.step1Form.controls['startdate']?.setValidators(Validators.required);
      this.step1Form.controls['startdate']?.updateValueAndValidity();
       
        

    }
    else if(title.toLowerCase() === 'continuing training program')
    {
        this.step1Form.controls['version']?.clearValidators();
        this.step1Form.controls['version']?.updateValueAndValidity();
        this.step1Form.controls['title']?.clearValidators();
        this.step1Form.controls['title']?.updateValueAndValidity();
        this.step1Form.controls['startdate']?.clearValidators();
        this.step1Form.controls['startdate']?.updateValueAndValidity();
        this.step1Form.controls['year']?.setValidators(Validators.required);
        this.step1Form.controls['year']?.updateValueAndValidity();
      this.trainingProgramType = 2
    }
    else if(title.toLowerCase() === 'cycle training program')
    {
      this.trainingProgramType = 3
      this.step1Form.controls['version']?.clearValidators();
      this.step1Form.controls['version']?.updateValueAndValidity();
      this.step1Form.controls['year']?.clearValidators();
      this.step1Form.controls['year']?.updateValueAndValidity();
      this.step1Form.controls['title']?.setValidators(Validators.required);
      this.step1Form.controls['title']?.updateValueAndValidity();
      this.step1Form.controls['startdate']?.setValidators(Validators.required);
      this.step1Form.controls['startdate']?.updateValueAndValidity();
        
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async SaveOrUpdateTrainingProgram()
  {
    
    

    if(this.positionId === undefined || this.positionId === null)
    {
      this.alert.errorAlert('Please link '+ await this.transformTitle('Position') +' in order to create training program');
      return
    }

    if (this.trainingProgramId != undefined)
    {
      let startDateTIme = `${this.step1Form.get('startdate')?.value}T${this.step1Form.get('startTime')?.value}`;
      let endDateTime = `${this.step1Form.get('endDate')?.value}T${this.step1Form.get('endTime')?.value}`;
      let createOpt: TrainingProgramCreateOptions = {
      tPVersionNo: this.step1Form.get('version')?.value,
      trainingProgramTypeId: this.step1Form.get('trainingProgtype')?.value,
      startDate: startDateTIme,
      endDate: endDateTime,
      positionId: this.positionId,
      programTitle: this.step1Form.get('title')?.value,
      description:  this.step1Form.get('description')?.value,
      year:   new Date(this.step1Form.get('year')?.value,  0, 1, 0, 0, 0, 0),
      copiedTrainingId:this.fromCopiedTrainingId
    };

    //service call
    await this.trainingProgramsrvc.update(this.trainingProgramId,createOpt).then((res) => 
    {
      
      this.alert.successToast("Training Program Updated");
      // this.stepper.next();
    });
    await this.trainingProgramsrvc.getLinkedILAWithCount(this.trainingProgramId).then((res) => 
      {
        this.srcList.forEach((ila)=>{
          this.alreadyLinkedIds.push(ila.id);
        });
    
        this.dataSourceILA = new MatTableDataSource(this.srcList);
        this.dataSourceILAWS = new MatTableDataSource(this.srcList);
        this.stepper.next();
      });

    }
    else
    {
      let startDateTIme = `${this.step1Form.get('startdate')?.value}T${this.step1Form.get('startTime')?.value}`;
      let endDateTime = `${this.step1Form.get('endDate')?.value}T${this.step1Form.get('endTime')?.value}`;
      let createOpt: TrainingProgramCreateOptions = {
      tPVersionNo: this.step1Form.get('version')?.value,
      trainingProgramTypeId: this.step1Form.get('trainingProgtype')?.value,
      startDate: startDateTIme,
      endDate: endDateTime,
      positionId: this.positionId,
      programTitle: this.step1Form.get('title')?.value,
      description:  this.step1Form.get('description')?.value,
      year:   new Date(this.step1Form.get('year')?.value,  0, 1, 0, 0, 0, 0),
      copiedTrainingId:this.fromCopiedTrainingId
    };

    //service call
    await this.trainingProgramsrvc.create(createOpt).then((res) => 
    {
      
      this.trainingProgram = res;
      this.trainingProgramId = res.trainingProgram.id;
      this.alert.successToast("Training Program Created");
      this.stepper.next();
    });

    }
   
    // if(this.trainingProgramId)
    // {
      
    // }
    
  }
  SetPositionId(event:any)
  {
      this.positionId = event.id;
      let positionData: { num: number, position: string }[] = [
        { "num": 1, "position":  event.title},
        
    ];
      this.positionName = event.title;
      this.dataSourcePositions = new MatTableDataSource(positionData);
      this.flyPanelService.close();
   
  }


  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSourceILA.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSourceILA.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }
  async unlinkItemsModal(templateRef: any, id?: any) {
    var title = this.step1Form.get('title')?.value;
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.labelPipe.transform('ILA') + 's \n';
    this.ilaIds = [];
    if (id) {
      this.ilaIds.push(id);
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id)?.number + ' - ' + this.srcList.find((x) => x.id == id)?.description;
    } else {
      this.unlinkIds.forEach((d,i) => {
        this.ilaIds.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d)?.number + ' - ' +  this.srcList.find((x) => x.id == d)?.description + '\n';
      });
      this.unlinkDescription += ' \n' + 'from Training Program ' + title;
    }
    
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  getData(e: any) {

    var data = JSON.parse(e);
    var options = new TrainingProgram_ILA_LinkCreateOptions();
    options.iLAIds = this.ilaIds;
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    this.trainingProgramsrvc.unlinkILA(this.trainingProgramId,options).then(async (_)=>{
      this.alert.successToast("Unlinked Selected " + await this.labelPipe.transform('ILA') + "s from Training Program");
      this.refreshILAData();
    })
  }
  async getTrainingProgramDetails()
  {
   
   await this.trainingProgramsrvc.get(this.trainingProgramId).then((res)=>
   {
      this.trainingProgram = res;      
      this.step1Form.patchValue({
        // certDate:this.datePipe.transform(this.oldempCert.issueDate, "yyyy-MM-dd"),
        // renewDate:this.datePipe.transform(this.oldempCert.renewalDate, "yyyy-MM-dd"),
        // exptDate:this.datePipe.transform(this.oldempCert.expirationDate, "yyyy-MM-dd")
      trainingProgtype: this.trainingProgram.trainingProgramTypeId,
      title: this.trainingProgram.programTitle,
      startdate: this.getDateStringFromUtc(this.trainingProgram.startDate),
      startTime:this.getTimeFromUtc(this.trainingProgram.startDate),
      endDate: this.getDateStringFromUtc(this.trainingProgram.endDate),
      endTime:this.getTimeFromUtc(this.trainingProgram.endDate),
      description:  this.trainingProgram.description,
      version: this.trainingProgram.tpVersionNo,
      year: parseInt(this.getYearOnly(this.trainingProgram.year), 10)
    });
    this.positionId = this.trainingProgram.positionId;
    let positionData: { num: number, position: string }[] = [
        { "num": 1, "position":  res.position.positionTitle},
        
    ];
      this.positionName = res.position.positionTitle;
      this.dataSourcePositions = new MatTableDataSource(positionData);
      this.editPositionData = positionData;

      this.temp = {value:res.trainingProgramType.id}
      
      this.getTrainingProgramTypes();

    })

 

    if(this.mode === 'copy')
    {
      this.savecopyID = this.trainingProgramId
      this.trainingProgramId = undefined;
    } 
  }

  convertUtcTimeToLocalTime(datetime: any): Date {
    var startDateString = this.datePipe.transform(datetime,'yyyy-MM-dd hh:mm a');
    const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    var newdatetime = new Date(Date.parse(localstartDateTimeString));
    return newdatetime;
  }

  getTimeFromUtc(datetime: any): string {
    const localDate = this.convertUtcTimeToLocalTime(datetime);
    return localDate.toLocaleTimeString('en-GB', {
      hour: '2-digit',
      minute: '2-digit',
      hour12: false,
    });
  }

  getDateStringFromUtc(datetime: any): string | null {
    const localDate: Date = this.convertUtcTimeToLocalTime(datetime);
    return this.datePipe.transform(localDate, 'yyyy-MM-dd');
  }
  
  PublishTrainingProgram(e: any) {

    
    var options = new TrainingProgram_HistoryCreateOptions();
    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.ChangeNotes = data['reason'];
    if(this.mode ==='add' || this.mode ==='edit'){
      this.trainingProgramsrvc.publishTrainingProgram(this.trainingProgramId,options).then((_)=>{
        this.alert.successToast("Training Program Published Successfully");
        this.router.navigate(['dnd/trainingprogram']);
      })
    }else if(this.mode === 'copy'){
      this.trainingProgramsrvc.publishTrainingProgram(this.savecopyID,options).then((_)=>{
        this.alert.successToast("Training Program Published Successfully");
        this.router.navigate(['dnd/trainingprogram']);
      })
    }
    
  }

  onTimeSelected() {
    var startTime = this.step1Form.get('startTime').value;
    var endTime = this.step1Form.get('endTime').value;
    var startDate = this.step1Form.get('startDate').value;
    var endDate = this.step1Form.get('endDate').value;
    if (startTime && endTime) {
      if (startDate == endDate) {
        if (startTime < endTime) {
          this.isDateTimeValid = false;
        } else {
          this.isDateTimeValid = true;
        }
      } else if (startDate < endDate) {
        this.isDateTimeValid = false;

      } else {
        this.isDateTimeValid = true;
      }
    }
  }

  PublishTrainingProgramModal(templateRef: any) {

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  async PreviewDetails()
  {
    this.getTrainingProgramDetails()
    this.stepper.next();
  }
  getYearOnly(dateValue: any): string {
    if (!dateValue) return '';
    try {
      const date = new Date(dateValue);
      if (!isNaN(date.getTime())) {
        return date.getFullYear().toString();
        
      }
    } catch {
      if (typeof dateValue === 'number') {
        return dateValue.toString();
      }
    }
    return '';
  }
  
  }

export class positionsTableData
{
  num:number;
  position:string;
}