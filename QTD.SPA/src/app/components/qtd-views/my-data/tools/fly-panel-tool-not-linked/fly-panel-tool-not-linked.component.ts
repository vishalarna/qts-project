import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { UnlinkedTool } from '@models/Tool';
import { Result } from '@models/result';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';


@Component({
  selector: 'app-fly-panel-tool-not-linked',
  templateUrl: './fly-panel-tool-not-linked.component.html',
  styleUrls: ['./fly-panel-tool-not-linked.component.scss']
})
export class FlyPanelToolNotLinkedComponent implements OnInit {
  showActive = true;
  isLoading = false;
  unlinkHeader = '';
  unlinkDescription = '';
  unlinkIds: any[] = [];
  searchText = '';
  treedataSource = new MatTreeNestedDataSource<TreeData>();
  tasktreeControl = new NestedTreeControl<TreeData>(
    (node: any) => node.children
  );
  hasTaskChild = (_: number, node: TreeData) =>
    !!node.children && node.children.length > 0;

  @Input() panelType: ToolsNotLinkedPanelType = 'Active';
  @Input() activeInactiveCheck = false;

  constructor(
    public readonly flyPanelSrvc: FlyInPanelService,
    public readonly dialog: MatDialog,
    private readonly alert: SweetAlertService,
    private readonly toolService:ToolsService,
    private labelPipe: LabelReplacementPipe,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe
)
  {
  }

  ngOnInit(): void {
    if (this.activeInactiveCheck) {
      this.fetchCategories();
    }
    else {
      this.fetchNotLinkedTools();
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  getTitleName(): string {
    switch (this.panelType) {
      case 'Active': return 'Active Tools';  
      case 'Inactive': return 'Inactive Tools';
      case 'Catactive': return 'Active Categories';
      case 'Catinactive': return 'Inactive Categories';
      case 'ToolsNotLinkedToSA': return 'Tools Not Linked to Sa';
      case 'ToolsNotLinkedToTask': return 'Tools Not Linked to Task';
      default: return '';
    }
  }

  public 

  private async fetchNotLinkedTools() {
    if (this.panelType === 'ToolsNotLinkedToTask') {
      this.isLoading = true;
      this.toolService.getToolsNotLinkedToTask().subscribe((result) => this.processToolsResult(result));
    }
    else if (this.panelType === 'ToolsNotLinkedToSA') {
      this.isLoading = true;
      this.toolService.getToolsNotLinkedToEo().subscribe((result) => this.processToolsResult(result));
    }
  }

  private processToolsResult(result: Result<UnlinkedTool[]>) {
    if (!result.data || result.error) {
      this.alert.errorToast(result.error);
      this.isLoading = false;
      return;
    }
    
    const activeTools = result.data.filter((i) => i.active === this.showActive);

    const categoryMap = activeTools.reduce((map, tool) => { // group tools by category name
      if (!map.has(tool.categoryName)) {
        map.set(tool.categoryName, []);
      }

      map.get(tool.categoryName).push(tool);
      return map;
    }, new Map<string, UnlinkedTool[]>());

    let index = 1;
    const treeData = Array.from(categoryMap).map(([categoryName, tools]) => ({
      id: index++,
      description: `${index - 1} - ${categoryName}`,
      isSafetyHazard: false,
      children: tools.map((tool) => ({
        id: tool.id,
        description: `${tool.number} - ${tool.name}`,
        active: tool.active,
        isSafetyHazard: true
      }))
    }));

    this.treedataSource.data = treeData;
    this.isLoading = false;
  }
  
  private fetchCategories() {
    switch(this.panelType){
      case 'Catactive':
        this.toolService.getCatList(this.panelType).then((data)=>{

          this.treedataSource.data = Object.assign([],data);
        }).catch((error)=>{
          this.alert.errorToast('Error Fetching Categories Data');
        })
        break;

      case 'Catinactive':
        this.toolService.getCatList(this.panelType).then((data)=>{
          this.treedataSource.data = Object.assign([],data);
        }).catch((error)=>{
          this.alert.errorToast('Error Fetching Categories Data');
        })
        break;

      case 'Active':
        this.toolService.getToolList(this.panelType).then((data)=>{
          this.treedataSource.data = Object.assign([],data);
        }).catch((error)=>{
          this.alert.errorToast('Error Fetching Data');
        })
        break;

      case 'Inactive':
        this.toolService.getToolList(this.panelType).then((data)=>{
          this.treedataSource.data = Object.assign([],data);
        }).catch((error)=>{
          this.alert.errorToast('Error Fetching Data');
        })
        break;
    }
  }
}

export type ToolsNotLinkedPanelType = 'ToolsNotLinkedToTask' | 'ToolsNotLinkedToSA' | 'Catactive' | 'Catinactive' | 'Active' | 'Inactive';

interface TreeData {
  id: any;
  description: string;
  active?:boolean;
  children?: TreeData[];
  isSafetyHazard?: boolean;
}
