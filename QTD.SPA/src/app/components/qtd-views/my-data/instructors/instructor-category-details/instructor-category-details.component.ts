import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Instructor_Category } from 'src/app/_DtoModels/Instructor_Category/Instructor_Category';
import { Instructor_CategoryHistoryCreateOptions } from 'src/app/_DtoModels/Instructor_Category/Instructor_CategoryHistoryCreateOptions';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureOptions } from 'src/app/_DtoModels/Procedure/ProcedureOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { InstructorCategoryService } from 'src/app/_Services/QTD/instructor-category.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-instructor-category-details',
  templateUrl: './instructor-category-details.component.html',
  styleUrls: ['./instructor-category-details.component.scss']
})
export class InstructorCategoryDetailsComponent implements OnInit {
  modalType: "Add" | "Edit" | "Copy"
  datePipe = new DatePipe('en-us');
  isActive: boolean = true;
  subscription = new SubSink();
  currentProcedure: Procedure;
  issuAuthTitle = "";
  procTitle = "";
  revisionNumber = "";
  hyperlink = "";
  effectiveDate: string | null = "";
  deleteDescription = "";
  isCopy = false;
  description = '';
  website = '';
  title = '';
  id = '';
  isLoading = false;
  modalHeader = '';
  modalDescription = '';
  name ='';
  instructr_Category:Instructor_Category;
  deleteCheck:boolean;
  constructor(private route: ActivatedRoute,
    public vcf: ViewContainerRef,
    private procService: ProceduresService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private router: Router,
    private insCatService:InstructorCategoryService,
    private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
  }
  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.isLoading = true;
      this.id = res.id;
      this.populateData();
      this.getCategoriesData();
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

  openFlyPanel(templateRef: any, mode: any) {

    this.modalType = mode;
    if(this.modalType === 'Copy')
    {
      this.isCopy = true;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  // editProcedure(templateRef:any){
  //   this.isCopy = false;
  //   const portal = new TemplatePortal(templateRef, this.vcf);
  //   this.flyPanelService.open(portal);
  // }

  async deleteProcedure(templateRef: any) {
    this.deleteDescription = `You are selecting to delete ` + await this.labelPipe.transform('Instructor') + ` Category ${this.title}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  populateData()
  {
    
    this.insCatService
      .get(this.id)
      .then((data: any) => {
        //this.currentIssuingAuthority = data;
        this.isLoading = false;
        this.title = data.iCategoryTitle;
        this.isActive = data.active;
        this.description = data.iCategoryDescription;

        this.website = data.iCategoryUrl;
        this.instructr_Category = data;
        if(data.instructors.length > 0){
          this.deleteCheck = true;
        }
        else if(data.instructors.length === 0){
          this.deleteCheck = false;
        }
      })
      .catch(async (err: any) => {
        this.isLoading = false;
        this.alert.errorToast('Error Fetching ' + await this.labelPipe.transform('Instructor') + ' Category Detail');
      });
  }

  async getCategoriesData(){
    await this.insCatService.get(this.id).then((res:any)=>{

      this.instructr_Category = res;
      this.isActive = this.instructr_Category.active;
    }).finally(()=>{

    });
  }
  refresh(){
    this.populateData();
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }
  async changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Activate ' + await this.labelPipe.transform('Instructor') + ' Category'
      : 'Deactivate ' + await this.labelPipe.transform('Instructor') + ' Category';
    if(active === false){
      this.modalDescription = `You are selecting to make Category ${this.title} Inactive. If you continue, this Category and all associated ` + await this.labelPipe.transform('Instructor') + `s will be made Inactive`;
    }
    else{
      this.modalDescription = `You are selecting to make Category, ${this.title}, Active. If you continue, this Category and all associated ` + await this.labelPipe.transform('Instructor') + `s will be made Active.`;
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  MakeActive(e: any, active: boolean)
  {
    

    var options = new Instructor_CategoryHistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.CategoryNotes = data['reason'];
    if (active) {
      options.ActionType = 'active';
    } else {
      options.ActionType = 'inactive';
    }

    this.insCatService.makeActiveInactiveOrDelete(this.id, options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Instructor') + ' Category Made ' + options.ActionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });
        this.dataBroadcastService.updateMyDataNavBar.next(null);
        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }

  Delete(e: any)
  {
    
    var options = new Instructor_CategoryHistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.CategoryNotes = data['reason'];

      options.ActionType = 'delete';


    this.insCatService.makeActiveInactiveOrDelete(this.id, options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Instructor') + ' Category ' + options.ActionType);
        this.router.navigate(['my-data/instructors/overview'])
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });

        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }
}
