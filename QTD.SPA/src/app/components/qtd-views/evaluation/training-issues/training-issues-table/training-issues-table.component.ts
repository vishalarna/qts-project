import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-training-issues-table',
  templateUrl: './training-issues-table.component.html',
  styleUrls: ['./training-issues-table.component.scss']
})
export class TrainingIssuesTableComponent implements OnInit {
  @Input() dataElementTypeId = "";
  @Input() dataElementType = "";
  @ViewChild(MatSort) sort: MatSort;
  linkTrainingIssueColumns: string[] = ['issueCode','issueTitle', 'description','createdDate','dueDate','severityLevel','status'];
  trainingIssues:TrainingIssue_VM[];
  dataElement:TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  trainingIssuesDataSource:MatTableDataSource<TrainingIssue_VM> = new MatTableDataSource();
  constructor(
    private trainingisseService:TrainingIssuesService,
    private flyPanelService:FlyInPanelService,
    private vcf: ViewContainerRef) { }

  ngOnInit(): void {
    this.getTrainingIssuesByDataElementType(this.dataElementTypeId,this.dataElementType);
    this.getAllDataElementWithCategories();
  }

  async getTrainingIssuesByDataElementType(id:string,type:string){
    this.trainingIssues = await this.trainingisseService.getTrainingissueByDataElementTypeAsync(id,type);
    this.makeTrainingIssueTable(this.trainingIssues);
  }

  async getAllDataElementWithCategories(){
    var dataElementCategories = await this.trainingisseService.getAllDataElementsWithCategoriesAsync();
    this.dataElement = dataElementCategories.reduce((acc, curr) => acc.concat(curr.dataElementVMs), []).find(vm => vm.dataElementType === this.dataElementType);
  }

  makeTrainingIssueTable(trainingIssue:TrainingIssue_VM[]){
    this.trainingIssuesDataSource = new MatTableDataSource(trainingIssue);
  }

  openFlyInPanel(templateRef: any) {
      const portal = new TemplatePortal(templateRef, this.vcf);
      this.flyPanelService.open(portal);
    }

  closeAddTrainingIssueFlyIn(){
    this.flyPanelService.close();
  }

  refreshTable(e:any){
    this.trainingIssues.push(e);
    this.makeTrainingIssueTable(this.trainingIssues);
  }

}
