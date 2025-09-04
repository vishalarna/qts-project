import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TQEvaluatorWithCount } from 'src/app/_DtoModels/TaskQualification/TQEvaluatorWithCount';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-evaluators',
  templateUrl: './task-evaluators.component.html',
  styleUrls: ['./task-evaluators.component.scss']
})
export class TaskEvaluatorsComponent implements OnInit {
  filterString = "";
  isLoading = false;
  dataSource!: MatTableDataSource<TQEvaluatorWithCount>;
  displayedColumns = ['evaluatorName', 'positionTitle', 'count', 'unlink'];
  selection = new SelectionModel<TQEvaluatorWithCount>(true, []);
  evaluatorsWithCounts: TQEvaluatorWithCount[] = [];
  selectedData!: TQEvaluatorWithCount;
  description = "";
  @ViewChild(MatPaginator) paginator!:MatPaginator;
  @ViewChild(MatSort) sort:MatSort;


  constructor(
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private tqService: TaskRequalificationService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  clearFilter(){
    this.filterString = '';
    this.dataSource.filter = null;
  }

  async readyData() {
    this.isLoading = true;
    this.evaluatorsWithCounts = await this.tqService.getEvaluatorWithCount();
     this.evaluatorsWithCounts = this.evaluatorsWithCounts.map((item)=>({
      ...item,
      evaluatorName : item.evaluatorFName +" "+ item.evaluatorLName,
      }));
      this.dataSource = new MatTableDataSource(this.evaluatorsWithCounts)
      this.isLoading = false;

      setTimeout(()=>{
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },1);
  }


  sortData(sort: Sort) {
    this.dataSource.sort = this.sort;
  }

  checkChange(event: any, row: TQEvaluatorWithCount) {
    if (event.checked) {
      this.selection.select(row)
    }
    else {
      this.selection.deselect(row);
    }
  }

  masterToggle(event: any) {
    this.dataSource.data.forEach((element) => {
      if (event.checked) {
        this.selection.select(element);
      }
      else {
        this.selection.deselect(element);
      }
    });
  }

  inputChange() {
    this.dataSource.filter = this.filterString.trim().toLowerCase();
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async unlinkData(templateRef: any, data: TQEvaluatorWithCount) {  
    if (data.count > 0) {
      this.alert.warningAlert('Remove Evaluator', `You are selecting to remove ${data.evaluatorName} as a ` + await this.transformTitle('Task') +` Qualification Evaluator. The selected ` + await this.labelPipe.transform('Employee') + ` is assigned to complete ` + await this.transformTitle('Task') +` & Skill Qualifications and/or has pending qualifications released to EMP. Click on the Pending Qualifications number value for the selected ` + await this.labelPipe.transform('Employee') + ` to view pending qualifications and reassign to another Evaluator.`)
    }
    else {
      this.selectedData = data;
      this.description = `You are selecting to remove ${data.evaluatorName} as a ` + await this.transformTitle('Task') +` Qualification Evaluator.`;

      const dialogRef = this.dialog.open(templateRef, {
        width: '800px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }
  }

  async removeEvaluator() {

    await this.tqService.removeEvaluator(this.selectedData.evaluatorId).then((_) => {
      this.alert.successToast("Evaluator Removed Successfully");
      this.dataSource.data = Object.assign(this.dataSource.data.filter((data) => {
        return data.evaluatorId !== this.selectedData.evaluatorId
      }));
      this.dataSource.paginator = this.paginator;
      this.dataBroadcastService.refreshTQStats.next(null);
    })
  }
}
