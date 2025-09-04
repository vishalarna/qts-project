import { animate, state, style, transition, trigger } from '@angular/animations';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, QueryList, ViewChild, ViewChildren, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';
import { MetaILAEmployeesLinkOptions } from '@models/MetaILAEmployeesLink/MetaILAEmployeesLinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-meta-idp-detail',
  templateUrl: './meta-idp-detail.component.html',
  styleUrls: ['./meta-idp-detail.component.scss'],
  animations: [
      trigger('detailExpand', [
        state('collapsed', style({ height: '0px', maxHeight: '0px' })),
        state('expanded', style({ height: '*' })),
        state('collapsed, void', style({ height: '0px', minHeight: '0' })),
        transition(
          'expanded <=> collapsed',
          animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
        ),
      ]),
    ],
})
export class MetaIdpDetailComponent implements OnInit {

  @Input() employeeName : string = '';
  metaILADataSource = new MatTableDataSource<MetaILAVM>();
  metaILAsList:any[] = [];
  subscription = new SubSink();
  empId:string = '';
  displayedColumns:string[] = ["expand","metaILATitle","completedLinkedCourses","metaStatus","metaTestCompletedDate","metaTestGrade","metaTestScore","metaStudentEvaluationStatus","action"];
  expandedData: any | null;
  displayedExpandedColumns: string[] = ["ilaNumber","ilaName","startDate","completedDate","grade","score"];
  expandedTableDataSourcesMap = new Map<string, MatTableDataSource<any>>();
  innerLoader:boolean = false;
  metaILAIds:string[] = [];
  enrollMetaILADescription:string = '';
  enrollMetaILAId:string = '';
  header:string = '';
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChildren('nestedSort') nestedSorts!: QueryList<MatSort>;
  mainLoader:boolean = false;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private metaILAService:MetaILAService,
    private route: ActivatedRoute,
    public labelPipe:LabelReplacementPipe,
    public dialog: MatDialog,
    private alertService:SweetAlertService
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.empId = res.id;
    });
    this.getAllMetaILAsLinkedToEmployeeAsync();
  }

  async getAllMetaILAsLinkedToEmployeeAsync(){
    this.mainLoader = true;
    this.metaILAsList = await this.metaILAService.getAllMetaILAsLinkedToEmployeeForIDPAsync(this.empId);
    this.metaILAIds = this.metaILAsList.map(item=>item.metaILAId);
    this.makeDataSource(this.metaILADataSource,this.metaILAsList);
    setTimeout(() => {
      this.metaILADataSource.sort = this.sort;
    }, 100);
    this.mainLoader = false;
  }

  makeDataSource(dataSource,data:any){
    dataSource.data = data;
  }

  async openFlyInPanel(templateRef: any) {
      const portal = new TemplatePortal(templateRef, this.vcf);
      this.flyPanelSrvc.open(portal);
  }

  toggleRow(row: any, event: Event) {
    event.stopPropagation();
    if (this.expandedData === row) {
      this.expandedData = null;
      return;
    }
    this.expandedData = row;
    this.fetchNestedData(row.metaILAId);
  }

  async fetchNestedData(metaIlaId: string) {
    this.innerLoader = true;
    const metaILAMembers = await this.metaILAService.getLinkedMetaILAsMembersByMetaILAIdForIDPAsync(metaIlaId, this.empId);
    var updatedMetaILAMembers = metaILAMembers.map(item => {
      let startDateStr;
      if (item.startDate == null) {
        startDateStr = null;
      } else {
        startDateStr = item.startDate.endsWith('Z') ? item.startDate : item.startDate + 'Z';
      }
      return {
        ...item, startDate: startDateStr == null ? null : startDateStr.toLocaleString()
      };
    });
    const nestedDataSource = new MatTableDataSource(updatedMetaILAMembers);
    this.expandedTableDataSourcesMap.set(metaIlaId, nestedDataSource);
    this.innerLoader = false;
    setTimeout(() => {
      this.assignNestedSort(metaIlaId);
    });
  }

  async openEnrollEmployeeModal(templateRef: any, row?:any){
    this.header = `Enroll ` + await this.labelPipe.transform('Employee'); 
    this.enrollMetaILADescription = `Are you sure you would like to enroll this ` + await this.labelPipe.transform('Employee') + ` any  <i>On Demand</i> ` + await this.labelPipe.transform('ILA') +  `'s linked to this Meta? Training will be made immediately available to the ` + await this.labelPipe.transform('Employee') + ` and course Start Date will be set to the current date`;
    this.enrollMetaILAId = row.metaILAId;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getLinkMetaILAToEmployeeAsync(e:any){
    var options = new MetaILAEmployeesLinkOptions();
    options.employeeIDs.push(this.empId);
    options.metaILAIDs = e.map(item=>item);
    options.isComingFrom = "metaIdp";
    await this.metaILAService.linkMetaILAEmployee(options);
    await this.getAllMetaILAsLinkedToEmployeeAsync();
  }

  getEnrollDisabled(row:any){
    var value = row.completedLinkedCourses.split("/")[1];
    return value == "0" ? true : false;
  }

  async enrollMetaILAAsync(){
    var options = new MetaILAEmployeesLinkOptions();
    options.employeeIDs.push(this.empId);
    options.metaILAIDs.push(this.enrollMetaILAId);
    options.isComingFrom = "metaIdpEnroll"
    await this.metaILAService.linkMetaILAEmployee(options);
    this.getAllMetaILAsLinkedToEmployeeAsync();
    this.alertService.successToast("Enrolled");
    await this.getAllMetaILAsLinkedToEmployeeAsync();
  }

  getExpandedDataSource(metaILAId: string): MatTableDataSource<any> | null {
    return this.expandedTableDataSourcesMap.get(metaILAId) || null;
  }

  assignNestedSort(metaIlaId: string) {
    const index = this.metaILAsList.findIndex(item => item.metaILAId === metaIlaId);
    if (index === -1) return;
    const nestedSort = this.nestedSorts.toArray()[index];
    const dataSource = this.expandedTableDataSourcesMap.get(metaIlaId);
  
    if (nestedSort && dataSource) {
      dataSource.sort = nestedSort;
    }
  }

}
