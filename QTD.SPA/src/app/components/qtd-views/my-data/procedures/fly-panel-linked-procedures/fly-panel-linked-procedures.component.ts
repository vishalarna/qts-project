import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-linked-procedures',
  templateUrl: './fly-panel-linked-procedures.component.html',
  styleUrls: ['./fly-panel-linked-procedures.component.scss'],
})
export class FlyPanelLinkedProceduresComponent implements OnInit, AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Input() Id: any; //! Use this Id to fetch the linked procedures and map into linkedProcedures and show the data in html then

  //! Pass the Title as input from selected Tab
  @Input() Title: string;
  @Input() selectedType: string;
  @Input() ilaNumber:any;
  @Input() shNumber:any;
  @Input() eoNumber:any;
  @Input() taskNumber:any;
  @Input() regNumber:any;
  linkedProcedures: Procedure[] = [];
  selectedTypeNumber:any;
  dataSource = new MatTreeNestedDataSource<ProcedureTreeModel>();
  treeControl = new NestedTreeControl<ProcedureTreeModel>(
    (node) => node.Children
  );
  navList: ProcedureTreeModel[] = [];
  isLoading: boolean = false;
  hasChild = (_: number, node: ProcedureTreeModel) =>
  !!node.Children && node.Children.length > 0;

  constructor(
    private procService: ProceduresService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    switch (this.selectedType) {
      case "Safety Hazard":
        this.readySHData();
        break;
      case "Task":
        this.readyTaskData();
        break;
      case "Regulatory Requirement":
        this.readyRRData();
        break;
      case "ILA":
        this.readyILAData();
        break;
      case "Enabling Objective":
        this.readyEOData();
        break;
    }
  }

  async readySHData() {
    await this.procService.getProceduresLinkedToSH(this.Id).then((res: Procedure[]) => {
      this.linkedProcedures = res;
      this.createProcedureTree();
      this.selectedTypeNumber = this.shNumber;
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Procedure') + "s That "+ await this.labelPipe.transform("Safety Hazard") +" is linked to " + err);
    })
  }

  async readyEOData() {
    await this.procService.getProceduresLinkedToEO(this.Id).then((res: Procedure[]) => {
      this.linkedProcedures = res;
      this.createProcedureTree();
      this.selectedTypeNumber = this.eoNumber;
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Procedure') + "s That " + await this.transformTitle('Enabling Objective') + " is linked to " + err);
    })
    .finally(() => {
      this.isLoading = false;
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async readyTaskData(){
    await this.procService.getProceduresLinkedToTask(this.Id).then((res:Procedure[])=>{
      this.linkedProcedures = res;
      this.createProcedureTree();
      this.selectedTypeNumber = this.taskNumber;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Procedure') + "s That " + await this.transformTitle('Task') + " is linked to " + err);
    })
    .finally(() => {
      this.isLoading = false;
    });
  }

  async readyILAData(){
    await this.procService.getProceduresLinkedToILA(this.Id).then((res:Procedure[])=>{
      this.linkedProcedures = res;
      this.createProcedureTree();
      this.selectedTypeNumber = this.ilaNumber;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Procedure') + "s That " + await this.labelPipe.transform('ILA') + " is linked to " + err);
    })
    .finally(() => {
      this.isLoading = false;
    });
  }

  async readyRRData(){
    await this.procService.getProceduresLinkedToRR(this.Id).then((res:Procedure[])=>{
      this.linkedProcedures = res;
      this.createProcedureTree();
      this.selectedTypeNumber = this.regNumber;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Procedure') + "s That " + await this.labelPipe.transform('Regulatory Requirement') + " is linked to " + err);
    })
    .finally(() => {
      this.isLoading = false;
    });
  }

  createProcedureTree(){
    let procedure_IssuingAuthorities = this.linkedProcedures.map(p=>p.procedure_IssuingAuthority).filter((obj, index, self) =>
    index === self.findIndex((o) => o.id === obj.id)).sort((a, b) => a.title.localeCompare(b.title));
    this.navList=[];
    procedure_IssuingAuthorities.forEach((data: any, index: any) => {
              this.navList.push({
                id: data.id,
                Title: index + 1 + ' - ' + data.title,
                active: data.active,
                Children: [],
                type: "ia",
                Collapsed: false
              });
              let procedures = this.linkedProcedures.filter(p=>p.issuingAuthorityId == data.id);
              procedures.sort((a, b) => {
                const numberA = a.number.toUpperCase();
                const numberB = b.number.toUpperCase();
    
                if (numberA < numberB) {
                  return -1;
                }
                if (numberA > numberB) {
                  return 1;
                }
                return 0;
              });
              procedures.forEach((proc: Procedure) => {
                this.navList[index].Children?.push({
                  id: proc.id,
                  Title: proc.number + ' - ' + proc.title,
                  selected: false,
                  active: proc.active,
                  type: "proc",
                  Collapsed: false
                });
              });
            });

                  this.dataSource.data = this.navList;
                  this.treeControl.dataNodes = this.navList;
  }
 
}


export class ProcedureTreeModel {
  id?: any;
  Title: string;
  IconName?: string;
  active?: boolean;
  Children?: ProcedureTreeModel[] = [];
  HasChildren?: boolean;
  Collapsed?: boolean = false;
  type?: string;
  number?: any;
  selected?: boolean;
}

