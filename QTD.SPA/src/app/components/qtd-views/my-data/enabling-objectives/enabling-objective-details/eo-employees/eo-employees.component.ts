import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { EmployeeWithCountOptions } from 'src/app/_DtoModels/Employee/EmployeeWithCountOptions';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-eo-employees',
  templateUrl: './eo-employees.component.html',
  styleUrls: ['./eo-employees.component.scss']
})
export class EoEmployeesComponent implements OnInit, AfterViewInit,OnDestroy {
  @Input() isActive = false;

  subscription = new SubSink();
  displayColumns: string[] = [
    'description',
    'positionTitle',
    'posQualificationDate',
    'traineeStatus',
  ];
  DataSource: MatTableDataSource<any>;

  eoId = "";
  empSrc: EmployeeWithCountOptions[] = [];
  alreadyLinked:any[] = [];
  selectedId = "";

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }

  constructor(
    public flyPanelSrvc : FlyInPanelService,
    private vcf : ViewContainerRef,
    private eoService : EnablingObjectivesService,
    private activateRoute : ActivatedRoute,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.activateRoute.params.subscribe((res:any)=>{
      this.eoId = String(res.id).split('-')[1];
      this.getLinkedEmployees();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getLinkedEmployees(){

    await this.eoService.getLinkedEmployees(this.eoId).then((data) => {

      this.DataSource = new MatTableDataSource(data);
    }).catch((error) => {
      
    }).finally(() => {
    });
    setTimeout(() => {
      this.DataSource.sort = this.sort;
    }, 1)
    
    this.alreadyLinked = this.empSrc.map((data)=>{
      return data.id;
    });
    
  }

  openFlyPanel(templateRef : any){
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

}
