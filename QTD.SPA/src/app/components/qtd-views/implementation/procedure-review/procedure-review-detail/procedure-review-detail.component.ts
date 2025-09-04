import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ChartData, ChartOptions } from 'chart.js';
import { AddEmployeeToProcedureReviewCreationOptions, procedureReviewEmpUpdateOptions } from '@models/Procedure/Procedure_review';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-procedure-review-detail',
  templateUrl: './procedure-review-detail.component.html',
  styleUrls: ['./procedure-review-detail.component.scss']
})
export class ProcedureReviewDetailComponent implements OnInit {

  hideChart = false;
  procedureReviewId: string='';
  isSpinner: boolean = true;
  procedureReviewObject:any;
  procedureReviewSpinner = false;
  datePipe = new DatePipe('en-us');
  dataSourceProcedureReview = new MatTableDataSource<any>();
  subscription = new SubSink();
  currentDate:string;
  selectedEmpObject:any;
  selection = new SelectionModel<any>(true, []);
  displayedAllColumns: string[] = [
    'index',
    'employee',
    'position',
    'organization',
    'completedDate',
    'status',
    'procedureReviewResponse',
    'comments',
    'recall',
  ];
  editType: 'score' | 'grade' | 'none' = 'none';
  valueToSave!: any;
  isSaving = false;
  editId: any = "";
  constructor(
    private procedureService:ProceduresService,
    private route: ActivatedRoute,
    public flyPanelSrvc: FlyInPanelService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshProcedureReviewData.subscribe((_) => {
      this.getEmployees();
    })
    this.route.params.subscribe((params: any) => {
      if (params.hasOwnProperty('id') ) {
        this.procedureReviewId = params['id'];
        this.getEmployees();
        this.currentDate=this.datePipe.transform(new Date(), 'yyyy-MM-dd')
        this.getInfo(this.procedureReviewId)
      }
    });
  }

  async readyEMP() {
    this.editId = '';
    this.editType = 'none';
    this.valueToSave = "";
  }
  convertUtcToLocalDate(val : Date) : Date {
    var d = new Date(val); // val is in UTC
    var localOffset = d.getTimezoneOffset() * 60000;
    var localTime = d.getTime() - localOffset;

    d.setTime(localTime);
    return d;
}
  async getEmployees() {
    this.isSpinner = true;
    this.procedureService
      .getLinkProcedureReviewEmp(this.procedureReviewId)
      .then((res: any) => {
        this.dataSourceProcedureReview.data = res;
        this.setChartData();
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }

  async getInfo(id: string) {
    this.procedureReviewSpinner = true;

    this.procedureService.getProcedureReviewById(id).subscribe((result) => {
      if (result.data) {
        this.procedureReviewObject = result.data!;
      }
      
      this.procedureReviewSpinner = false;
    });
  }

  openFlyInPanelAddEmployee(templateRef: any) {
    const portal = new TemplatePortal(templateRef,null);
    this.flyPanelSrvc.open(portal);
  }
  // Pi Chart
  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: false,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        position: 'right',
      },
    },
  };
  public pieChartLabels = [['True'], ['False'], 'Not Started yet'];
  public pieChartLegend = true;
  public pieChartPlugins = [];

  public pieChartData: ChartData<'pie'> = {
    labels: this.pieChartLabels,
    datasets: [
      {
        label: 'Title label',
        data: [0,0,0],
        backgroundColor: ['rgb(220 245 223)', 'rgb(253 194 194)', '#fff'],
        borderColor: ['rgb(220 245 223)', 'rgb(253 194 194)', '#828886'],
        hoverBackgroundColor: [
          'rgb(220 245 223)',
          'rgb(253 194 194)',
          '#828886',
        ],
        hoverBorderColor: [
          'rgba(0, 160, 0, 1)',
          'rgba(240, 160, 0, 1)',
          'rgba(220, 0, 0, 1)',
        ],
      },
    ],
  };

  setChartData(){
    var trueData = this.dataSourceProcedureReview.data.filter(x=>x.response==='true' && x.status!=='Not Started').length;
    var falseData = this.dataSourceProcedureReview.data.filter(x=>x.response==='false' && x.status!=='Not Started').length;
    var notStartedData = this.dataSourceProcedureReview.data.filter(x=>x.status==='Not Started').length;
    var dataCounts = [trueData, falseData, notStartedData];

    this.pieChartData['datasets'][0]['data'] = Object.assign([], dataCounts);

    this.pieChartData = Object.assign({}, this.pieChartData);
  }

  async removeEMP() {
    var options:AddEmployeeToProcedureReviewCreationOptions = new AddEmployeeToProcedureReviewCreationOptions()
    options.procedureReviewId = this.procedureReviewId;
    options.employeeIds = this.selection.selected.map((data)=>{ return data.empId});
    await this.procedureService.unLinkProcedureReviewEmp(options.procedureReviewId,options).then(async (_)=>{
      this.getEmployees();
      this.selection.clear();
      this.dialog.closeAll()
      this.alert.successToast(await this.transformTitle('Procedure') + " review " + await this.labelPipe.transform('Employee') + " has been deleted ");
    })
  }
  async removeEMPSingle() {
    var options:AddEmployeeToProcedureReviewCreationOptions = new AddEmployeeToProcedureReviewCreationOptions()
    options.procedureReviewId = this.procedureReviewId;
    options.employeeIds.push(this.selectedEmpObject.empId);
    await this.procedureService.unLinkProcedureReviewEmp(options.procedureReviewId,options).then(async (_)=>{
      this.getEmployees();
      this.selection.clear();
      this.dialog.closeAll()
      this.alert.successToast(await this.transformTitle('Procedure') + " review " + await this.labelPipe.transform('Employee') + " has been deleted ");
    })
  }
  async updateValue() {
    this.isSaving = true;
    var options = new procedureReviewEmpUpdateOptions();
    switch (this.editType) {
      case 'grade':
        options.response=this.valueToSave;
        options.procedureReviewId=this.procedureReviewId;
        await this.procedureService.updateProcedureReviewResponse(this.editId, options).then((_) => {
          this.alert.successToast("Response Updated Successfully");
          this.getEmployees();
          this.readyEMP();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
      case 'score':
        options.comments=this.valueToSave;
        options.procedureReviewId=this.procedureReviewId;
        await this.procedureService.updateProcedureReviewComments(this.editId, options).then((_) => {
          this.alert.successToast("Comments Updated Successfully");
          this.getEmployees();
          this.readyEMP();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
    }
  }
     /** Whether the number of selected elements matches the total number of rows. */
     isAllSelected() {
      const numSelected = this.selection.selected.length;
      const numRows = this.dataSourceProcedureReview.data.length;
      return numSelected === numRows;
    }

    /** Selects all rows if they are not all selected; otherwise clear selection. */
    toggleAllRows() {
      if (this.isAllSelected()) {
        this.selection.clear();
        return;
      }
      this.selection.select(...this.dataSourceProcedureReview.data);
    }

    /** The label for the checkbox on the passed row */
    checkboxLabel(row?: any): string {
      if (!row) {
        return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
      }
      return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
    }
    removeDialog(templateRef: any){
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }

    removeDialogSingle(templateRef: any,empObject){
      this.selectedEmpObject=empObject;
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }

    goBack(){
      history.back();
    }

    async transformTitle(title: string) {
      const labelName = await this.labelPipe.transform(title);
      return labelName;
    }
}
