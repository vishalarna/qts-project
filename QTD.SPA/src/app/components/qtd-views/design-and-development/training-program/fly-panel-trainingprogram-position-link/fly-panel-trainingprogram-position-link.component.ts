import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { title } from 'process';
import { Organization } from 'src/app/_DtoModels/Organization/Organization';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-trainingprogram-position-link',
  templateUrl: './fly-panel-trainingprogram-position-link.component.html',
  styleUrls: ['./fly-panel-trainingprogram-position-link.component.scss']
})
export class FlyPanelTrainingprogramPositionLinkComponent implements OnInit {
  linkPos: boolean = true;
  showActive: boolean = true;
  isLoading: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinkedIds: any[] = [];
  @Input() empfromAddView: any
  @Output() positionLinked = new EventEmitter<{ id: string, title:string }>();
  linkedIds: any[] = [];
  positions: Position[];
  filteredList: Position[];
  subscription = new SubSink();
  empID = '';
  deleteDescription = "";
  linkedEmps:any[] = [];
  modalType:"Add" | "Edit";
  employeeIdDelete:any;
  org:any;
  confirmed:any;
  positionId : any;
  posTitle : string;
  constructor(
    private posSrvc: PositionsService,
    private orgService: OrganizationsService,
    private empSrvc: EmployeesService,
    private taskSrvc: TasksService,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    public dialog:MatDialog,
    private route: Router,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void
  {
    // let segments = this.route.url.split('/')
    //
    // if (segments.includes('edit'))
    //  {
    //   this.empID = segments[segments.length - 1];
    //  }
    //  else
    //  {
    //   this.empID = this.empfromAddView;
    //  }
    this.getOrganizations();
  }




  linkToEmployee()
  {
    
    let option = 
    {
      organizationIds: this.linkedIds,
    };
    this.empSrvc.LinkOrganizationtoEmployee(this.empID, option).then(async (res) =>
    {
      this.alert.successToast('Organizations linked to ' + await this.labelPipe.transform('Employee'));
      this.refresh.emit('refresh position tbl');
      this.closed.emit('fp-link-pos-task-closed');
    });



  }

  // orgChecked(checked: boolean, id: any) {
  //   if (checked) {
  //     this.linkedIds.push(id);
  //   } else {
  //     this.linkedIds.splice(this.linkedIds.indexOf(id), 1);
  //   }
  //   this.linkedIds = [...new Set(this.linkedIds)];
  // }

  async getPositions() {
    await this.posSrvc.getAll().then((res) => {
      // this.positions = res;
      // this.filteredList = res;
    });
  }
  async deleteProcedure(templateRef:any,orgid:any){
    this.deleteDescription = `The following Organization Manager and ` + await this.labelPipe.transform('Employee') + `s have been assigned to this organization`;
    await this.orgService.get(orgid).then((res) => {
      this.org = res;
    });
     const dialogRef = this.dialog.open(templateRef, {
       width: '600px',
       height: 'auto',
       hasBackdrop: true,
       disableClose: true,
     });
   }

   async  getOrganizations()
   {
    this.isLoading = true;
    await this.posSrvc.getAllWithoutIncludes().then((res) => {
      this.positions = res;
      this.filteredList = res;

    }).finally(() => (this.isLoading = false));

  }
  async openFlyInPanel(templateRef: any,orgid:any) {
    
    this.modalType ="Edit"
    await this.orgService.get(orgid).then((res) => {
      this.org = res;
    });
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  async openFlyInPanelPosition(templateRef: any) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  positionChange(event:any)
  {
    this.positionId = event.value;
    this.positionLinked.emit(this.positionId);

  }
  posChecked(event:any, positionName: any) {
    
    
    this.linkedIds = [];
    this.linkedIds.push(event.value);
    this.posTitle = positionName;
    // if (checked)
    // {
    //   this.linkedIds.push(id);
    // } else {
    //   this.linkedIds.splice(this.linkedIds.indexOf(id), 1);
    // }

    //
  }
  linkToTrainingProgram()
  {
    this.positionId = this.linkedIds[0];
    this.positionLinked.emit({id:this.positionId,title:this.posTitle});
  }
  filterActive(makeActive: boolean)
  {
    this.filteredList = [...this.positions.filter((x) => x.active == makeActive)];
    this.showActive = makeActive;
  }
  filterData(e: Event) {
    
    let filterString = (e.target as HTMLInputElement).value;
    //this.filteredList.filter = this.positions.filter(((x)=>x.positionTitle)).includes(filterString);
    this.filteredList = [
      ...this.positions.filter((c) =>
            c.positionTitle.toLowerCase().includes(filterString.toLowerCase())
          ),
    ];
  }
}
