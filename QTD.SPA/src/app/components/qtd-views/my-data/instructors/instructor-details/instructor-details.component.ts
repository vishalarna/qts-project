import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Instructor_HistoryCreateOptions } from 'src/app/_DtoModels/Instructor_History/Instructor_HistoryCreateOptions';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureOptions } from 'src/app/_DtoModels/Procedure/ProcedureOptions';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { Instructor } from 'src/app/_DtoModels/Instructors/Instructor';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';


@Component({
  selector: 'app-instructor-details',
  templateUrl: './instructor-details.component.html',
  styleUrls: ['./instructor-details.component.scss']
})
export class InstructorDetailsComponent implements OnInit
{
  modalType:"Add" | "Edit" | "Copy"
  datePipe = new DatePipe('en-us');
  isActive: boolean = true;
  subscription = new SubSink();
  issuAuthTitle = "";
  procTitle = "";
  revisionNumber = "";
  hyperlink = "";
  effectiveDate : string|null = "";
  deleteDescription = "";
  isCopy = false;
  description = '';
  website = '';
  title = '';
  id = '';
  isLoading = false;
  email =''
  IsWorkBookAdmin = ''
  effevtiveDate :any;
  name ='';
  number='';
  modalHeader = '';
  modalDescription = '';
  instructr:Instructor;
  isInstructorActive:boolean;
  insDelete:boolean=false;
  constructor( private route:ActivatedRoute,
    public vcf:ViewContainerRef,
    private procService:ProceduresService,
    private alert:SweetAlertService,
    private dataBroadcastService:DataBroadcastService,
    public flyPanelService:FlyInPanelService,
    public dialog:MatDialog,
    private router:Router,
    private insService:InstructorService,
    private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
  }
  ngAfterViewInit(): void
  {
    
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.isLoading = true;
      this.id = res.id;
      this.populateData();
      this.getInstructorData(res.id);
    });

    // this.subscription.sink = this.dataBroadcastService.refreshProcedureData.subscribe((res:any)=>{
    //   this.readyProcedureData(this.id);
    // });
  }

  // readyProcedureData(id:any){
  //   this.procService.get(id).then((res:Procedure)=>{
  //     this.currentProcedure = res;
  //     this.issuAuthTitle = res.procedure_IssuingAuthority.title;
  //     this.procTitle = res.title;
  //     this.isActive = res.active;
  //     this.revisionNumber = res.revisionNumber;
  //     this.hyperlink = res.hyperlink;
  //     this.effectiveDate = this.datePipe.transform(res.effectiveDate,"yyyy-MM-dd");
  //   })
  // }

  // async changeActiveStatus(activate:boolean){
  //   var options = new ProcedureOptions();
  //   options.procedureIds = [];
  //   options.actionType = activate ? "active" : "inactive";
  //   options.procedureIds.push(this.id);

  //   await this.procService.delete(this.id,options).then((res:any)=>{
  //     this.alert.successToast("Procedure Is now " + options.actionType);
  //     this.isActive = activate;
  //     this.dataBroadcastService.updateProcedureInNavBar.next();
  //   }).catch((err:any)=>{
  //     this.alert.errorToast(`Error Making Procedure ${options.actionType} ${err}`);
  //   })

  // }

  ngOnDestroy(): void {
    //this.subscription.unsubscribe();
  }

  // getData(event:any){
  //   var options = new ProcedureOptions();
  //   options.actionType = "delete";
  //   var data = JSON.parse(event);
  //   options.changeEffectiveDate = data['effectiveDate'];
  //   options.changeNotes = data['reason'];
  //   options.procedureIds = [];
  //   options.procedureIds.push(this.id);
  //   this.procService.delete(this.id, options).then((res: any) => {
  //     this.alert.successToast("Procedure Issuing Authority Deleted");
  //     this.router.navigate(['/my-data/procedures/overview']);
  //     this.dataBroadcastService.updateProcedureInNavBar.next();
  //   }).catch((err: any) => {
  //     this.alert.errorToast("Error Deleting Procedure Issuing Authority");
  //   })
  // }

 openflyPanel(templateRef:any,mode:any)
 {
    this.modalType = mode;
    if(this.modalType === 'Copy')
    {
      this.isCopy = true;
    }

    if(this.modalType === 'Edit')
    {
      this.insService.get(this.id)
      .then((data: any) => {
        //this.currentIssuingAuthority = data;
        this.isLoading = false;
        this.isActive = data.active;
        this.email = data.instructorEmail;
        this.IsWorkBookAdmin = data.isWorkBookAdmin === true  ? 'WorkBook Admin' : ''
        this.effevtiveDate = this.datePipe.transform(Date.now(), 'MM-dd-yyyy');
        this.description = data.instructorDescription;
        this.name = data.instructorName;
        
      })

    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  // editProcedure(templateRef:any){
  //   this.isCopy = false;
  //   const portal = new TemplatePortal(templateRef, this.vcf);
  //   this.flyPanelService.open(portal);
  // }

  async deleteProcedure(templateRef:any){
   this.deleteDescription = `You are selecting to delete ` + await this.labelPipe.transform('Instructor') + ` ${this.name}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  populateData() {
    this.insService.get(this.id)
      .then((data: any) => {
        //this.currentIssuingAuthority = data;
        this.isLoading = false;
        this.isActive = data.active;
        this.email = data.instructorEmail;
        this.IsWorkBookAdmin = data.isWorkBookAdmin === true  ? 'WorkBook Admin' : ''
        this.effevtiveDate = this.datePipe.transform(Date.now(), 'MM-dd-yyyy');
        this.description = data.instructorDescription;
        this.name = data.instructorName;
        this.number = data.instructorNumber;



        this.isInstructorActive = data.instructor_Category.active;
      })
      .catch(async (err: any) => {
        this.isLoading = false;
        this.alert.errorToast('Error Fetching ' + await this.labelPipe.transform('Instructor') + ' Detail');
      });
  }

  async changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Activate'  + await this.labelPipe.transform('Instructor')
      : 'Deactivate'  + await this.labelPipe.transform('Instructor');
    if(active ===  false){
      this.modalDescription = `You are about to change ` + await this.labelPipe.transform('Instructor') + ` status with name ${this.name}. If you continue, this ` + await this.labelPipe.transform('Instructor') + ` will be made Inactive and can no longer be assigned to classes.`;
    }
    else{
      this.modalDescription = `You are about to change ` + await this.labelPipe.transform('Instructor') + ` status with name, ${this.name}. If you continue, this ` + await this.labelPipe.transform('Instructor') + ` will be made Active and can be assigned to classes.`;
    }

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  MakeActive(e: any, active: boolean) {
    

    var idarray :any =[];
    idarray.push(this.id);
    var options = new Instructor_HistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.InstructorNotes = data['reason'];
    options.instructorIds = idarray;
    if (active) {
      options.ActionType = 'active';
    } else {
      options.ActionType = 'inactive';
    }
    // 
    // options.instructorIds?.push(this.id);
    this.insService
      .makeActiveInactiveOrDelete(options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Instructor') + ' Made ' + options.ActionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });

        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }
  async getInstructorData(id:any){
    await this.insService.get(id).then((res:any)=>{

      this.instructr = res;
      this.isActive = this.instructr.active;
      this.insDelete = res.classSchedules.length;
    }).finally(()=>{

    });
  }

  refresh(){
    this.populateData();
    this.getInstructorData(this.id);
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }

  Delete(e: any)
  {
    
    var options = new Instructor_HistoryCreateOptions();
    var idarray :any =[];
    idarray.push(this.id);
    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.InstructorNotes = data['reason'];
    options.instructorIds = idarray;
      options.ActionType = 'delete';


      this.insService
      .makeActiveInactiveOrDelete(options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Instructor') + ' deleted successfully');
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });
        this.router.navigate(['my-data/instructors/overview'])
        this.isActive = options.ActionType === 'active' ? true : false;
      });

  }
}


