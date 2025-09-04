import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { ToolsNotLinkedPanelType } from '../fly-panel-tool-not-linked';


@Component({
  selector: 'app-tool-overview',
  templateUrl: './tool-overview.component.html',
  styleUrls: ['./tool-overview.component.scss']
})
export class ToolOverviewComponent implements OnInit {
  stats = false
  displayedColumns = ['id','name', 'desc', 'modifyBy'];
  spinner: boolean;
  activeInactiveCheck = false;
  subscription = new SubSink();
  dataSource = new MatTableDataSource<any>();
  statsObject: any;
  popupPanelType: ToolsNotLinkedPanelType;
  historyData: any[] = [];

  constructor(
    public readonly flyPanelSrvc : FlyInPanelService,
    private readonly toolService: ToolsService,
    private readonly alert: SweetAlertService,
    private readonly vcf: ViewContainerRef)
  {
  }

  ngOnInit(): void {
    this.getStatusHistoryData();
    this.getToolStats();
  }

  async getToolStats(){
    this.statsObject = await this.toolService.getToolStatistics();
  }
  
  async getStatusHistoryData(){
    const data = await this.toolService.getAllToolStatus();
    this.historyData = data;
    this.historyData = [...this.historyData.slice(0, 5)];
    this.dataSource.data = this.historyData;
  }

  async openHistFlypanel(templateRef: TemplateRef<any>) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async openFlyInPanel(templateRef: TemplateRef<any>, panelType: ToolsNotLinkedPanelType) {
    if (panelType === 'Catactive' || 
      panelType === 'Catinactive' || 
      panelType === 'Active' || 
      panelType === 'Inactive')
    {
      this.activeInactiveCheck = true;
    }
    else {
      this.activeInactiveCheck = false;
    }

    this.popupPanelType = panelType;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }
}
