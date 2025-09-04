import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { SimulatorScenarioOverview_SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenarioOverview_SimulatorScenario_VM';
import { SimulatorScenarioOverview_VM } from '@models/SimulatorScenarios_New/SimulatorScenarioOverview_VM';
import { SimulatorScenario_CollaboratorPermissions_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_CollaboratorPermissions_VM';
import { SimulatorScenario_Collaborator_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Collaborator_VM';
import { SimulatorScenario_FilterVM } from '@models/SimulatorScenarios_New/SimulatorScenario_FilterVM';
import { Store } from '@ngrx/store';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-simulator-scenarios-overview',
  templateUrl: './simulator-scenarios-overview.component.html',
  styleUrls: ['./simulator-scenarios-overview.component.scss'],
})
export class SimulatorScenariosOverviewComponent implements OnInit {
  dataSource: any;
  tableColumns: string[];
  @ViewChild(MatSort) sort: MatSort;
  searchText: string;
  simulatorScenario_VM: SimulatorScenarioOverview_VM;
  simScenarios: SimulatorScenario_Collaborator_VM[];
  deleteDescription: string;
  deleteSimScenarioId: string;
  activeDescription: string;
  activeSimScenarioId: string;
  inActiveDescription: string;
  inActiveSimScenarioId: string;
  filterValues: SimulatorScenario_FilterVM;
  filteredSimScenario:SimulatorScenarioOverview_SimulatorScenario_VM[];
  collaborationSimScenario: SimulatorScenario_CollaboratorPermissions_VM[] = [];
  editorPermissionId : string;
  viewerPermissionId : string;
  isOverviewLoading:boolean;
  @ViewChild('metaPaginator') set metaPaginator(paging: MatPaginator) {
    if (paging) this.dataSource.paginator = paging;
  }
  constructor(
    private router: Router,
    private store: Store<{ toggle: string }>,
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private simScenariosService: SimulatorScenariosService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private labelPipe:LabelReplacementPipe
  ) {}

 ngOnInit() {
    this.isOverviewLoading = false;
    this.filterValues=new SimulatorScenario_FilterVM();
    this.store.dispatch(sideBarOpen());
    this.tableColumns = [
      'CheckBox',
      'title',
      'ilAs',
      'positions',
      'difficulty',
      'status',
      'active',
      'action',
    ];
    this.dataSource = new MatTableDataSource<SimulatorScenarioOverview_SimulatorScenario_VM>();
    this.searchText = '';
    this.loadAsync();
  }

  async loadAsync() {
    this.isOverviewLoading = true;
    await this.simScenariosService.getOverviewAsync().then(res=>{
      this.isOverviewLoading = false;
      this.simulatorScenario_VM = res;
      this.dataSource = new MatTableDataSource<SimulatorScenarioOverview_SimulatorScenario_VM>(
        this.simulatorScenario_VM?.simulatorScenarios
        );
        setTimeout(() => {this.dataSource.sort = this.sort;}, 1);
      });
    this.collaborationSimScenario = await this.simScenariosService.getAllCollaboratorPermissionsAsync();
    this.editorPermissionId = this.collaborationSimScenario.find(x=>x.name == "Editor")?.id;
    this.viewerPermissionId = this.collaborationSimScenario.find(x=>x.name == "Viewer")?.id;

  }

  openScenirosWizard() {
    this.router.navigate(['/dnd/simulatorscenarios/create']);
  }

  searchUpdate(event: any) {
    const searchText = event.target.value.trim().toLowerCase();
    this.searchText = searchText;
    this.getSimScenariosFilterValues(this.searchText);
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf, {
      sideBarBackDrop: true,
    });
    this.flyPanelService.open(portal);
  }

  getActiveScenariosCount(active: boolean) {
    if (this.simulatorScenario_VM && this.simulatorScenario_VM.simulatorScenarios) {
    return (this.simulatorScenario_VM?.simulatorScenarios?.filter((x) => x?.active == active)?.length ?? 0);
      }
      else{
        return 0;
      }
  }
  getDraftScenariosCount(): number {
    if (this.simulatorScenario_VM && this.simulatorScenario_VM.simulatorScenarios) {
      return this.simulatorScenario_VM.simulatorScenarios.filter((scenario) => scenario.status === '1').length;
    } else {
      return 0;
    }
  }

  getPublishedScenariosCount(): number {
    if ( this.simulatorScenario_VM && this.simulatorScenario_VM.simulatorScenarios) {
      return this.simulatorScenario_VM.simulatorScenarios.filter((scenario) => scenario.status === '2').length;
    } else {
      return 0;
    }
  }

  getLinkedILAScenariosCount() {
    if ( this.simulatorScenario_VM && this.simulatorScenario_VM.simulatorScenarios) {
    return this.simulatorScenario_VM.simulatorScenarios.filter(
      (scenario) => scenario.ilAs !== ''
    ).length;
    } else{
      return 0;
    }
  }

  getUnlinkedILAScenariosCount() {
    if ( this.simulatorScenario_VM && this.simulatorScenario_VM.simulatorScenarios) {
    return this.simulatorScenario_VM.simulatorScenarios.filter(
      (scenario) => scenario.ilAs === ''
    ).length;
    } else{
      return 0;
    }
  }
  
  editSimulatorScenarios(id: string) {
    this.router.navigate(['/dnd/simulatorscenarios/edit/' + id]);
  }

  async copySimulatorScenarios(id: string) {
    var copiedScenarioId = await this.simScenariosService.copyScenarioById(id);
    this.alert.successToast('Simulator Scenarios Successfully Copied');
    this.router.navigate(['/dnd/simulatorscenarios/edit/' + copiedScenarioId?.id]);
  }

  viewSimulatorScenarios(id: string) {
    this.router.navigate(['/dnd/simulatorscenarios/view/' + id]);
  }

  async deleteSimulatorScenarios(templateRef: any, row: any) {
    this.deleteDescription = `The simulator scenario <b>${row.title}</b> is linked to an Active `  + await this.labelPipe.transform('ILA') +` <b>${row.ilAs}</b>. No `  + await this.labelPipe.transform('Employee') +`  records have been found for the listed  `  + await this.labelPipe.transform('ILA') + ` .`;
    this.deleteSimScenarioId = row.id;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteSimulatorScenarioById(id: string) {
    await this.simScenariosService.deleteScenarioById(id);
    this.alert.successToast('Simulator Scenario Deleted Successfully');
    await this.loadAsync();
  }

  activeSimulatorScenarios(templateRef: any, row: any) {
    this.activeDescription = `You are selecting to make the simulator scenario <b>${row.title}</b> Inactive.`;
    this.activeSimScenarioId = row.id;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  inactiveSimulatorScenarios(templateRef: any, row: any) {
    this.inActiveDescription = `You are selecting to make the simulator scenario <b>${row.title}</b> Active.`;
    this.inActiveSimScenarioId = row.id;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async activeSimulatorScenarioById(id: string) {
    await this.simScenariosService.inactiveAsync(id);
    this.alert.successToast('Simulator Scenario InActive Successfully');
    await this.loadAsync();
  }

  async inActiveSimulatorScenarioById(id: string) {
    await this.simScenariosService.activeAsync(id);
    this.alert.successToast('Simulator Scenario Active Successfully');
    await this.loadAsync();
  }

  getSimScenariosFilterValues(value:any){
    this.filterValues = value;
    this.filteredSimScenario = this.simulatorScenario_VM.simulatorScenarios;
    if (this.searchText) {
      this.filteredSimScenario = this.filteredSimScenario.filter(item => {
      return item?.title?.trim().toLowerCase().includes(this.searchText) || 
             item?.ilAs?.trim().toLowerCase().includes(this.searchText);
    });
  }

    if(this.filterValues?.position){
      this.applyPositionFilter();
    }

    if(this.filterValues?.activeStatus){
      this.applyActiveStatusFilter();
    }

    if(this.filterValues?.status){
      this.applyStatusFilter();
    }

    if(this.filterValues?.ila){
      this.applyILAFilter();
    }

    if(this.filterValues?.simScenariosNotLinkedToILA){
      this.applyScenariosNotLinkedToILa();
    }

    if(this.filterValues?.difficultyLevel){
      this.applyDifficultyLevelFilter();
    }

    this.dataSource = new MatTableDataSource(this.filteredSimScenario);
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
    },1);
  }

  applyPositionFilter(){
      const positionToFilter = this.filterValues.position.toString().trim();
      this.filteredSimScenario = this.filteredSimScenario.filter(x => x.positions && x.positions.includes(positionToFilter));
  }

  applyILAFilter() {
    const ilaToFilter = this.filterValues.ila.toString().trim();
    this.filteredSimScenario = this.filteredSimScenario.filter(x => x.ilAs && x.ilAs.includes(ilaToFilter));
  }

  applyActiveStatusFilter(){
    if(this.filterValues?.activeStatus == "Active"){
      this.filteredSimScenario =this.filteredSimScenario.filter(x => x.active == true);
    }
    if(this.filterValues?.activeStatus == "Inactive"){
      this.filteredSimScenario =this.filteredSimScenario.filter(x => x.active == false);
    }
  }

  applyStatusFilter() {
    if (this.filterValues?.status === "Draft") {
      this.filteredSimScenario = this.filteredSimScenario.filter(x => x.status === "1");
    }
    if (this.filterValues?.status === "Published") {
      this.filteredSimScenario = this.filteredSimScenario.filter(x => x.status === "2");
    }
  }

  applyScenariosNotLinkedToILa(){
    this.filteredSimScenario =this.filteredSimScenario.filter(x => x.ilAs.toString() === "" || x.ilAs === null);
  }

  applyDifficultyLevelFilter(){
    if (this.filterValues?.difficultyLevel === "High") {
      this.filteredSimScenario = this.filteredSimScenario.filter(x => x.difficulty === "1");
    }
    if (this.filterValues?.difficultyLevel === "Medium") {
      this.filteredSimScenario = this.filteredSimScenario.filter(x => x.difficulty === "2");
    }
    if (this.filterValues?.difficultyLevel === "Low") {
      this.filteredSimScenario = this.filteredSimScenario.filter(x => x.difficulty === "3");
    }
  }
  
  filterByTilesClick(value: string) {
    this.clearFlypanelFilters();
    this.filteredSimScenario = this.simulatorScenario_VM.simulatorScenarios;
    if(value == "Active"){
      this.filteredSimScenario =this.filteredSimScenario.filter(x => x.active == true);
      this.filterValues.activeStatus=value;
    }
    else if (value === 'Inactive') {
      this.filteredSimScenario =this.filteredSimScenario.filter(x => x.active == false);
      this.filterValues.activeStatus=value;
     }
    else if (value === 'ilaLinked') {
      this.filteredSimScenario = this.simulatorScenario_VM.simulatorScenarios.filter((scenario) => scenario.ilAs !== '');
      this.filterValues.simScenariosNotLinkedToILA=false;
    }
    else if (value === 'ilaUnlinked') {
      this.filteredSimScenario = this.simulatorScenario_VM.simulatorScenarios.filter((scenario) => scenario.ilAs == '');
      this.filterValues.simScenariosNotLinkedToILA=true;
    }
    else if (value === 'Published') {
      this.filteredSimScenario = this.filteredSimScenario.filter(x => x.status === "2");
      this.filterValues.status=value;
    }
    else if (value === 'Draft') {
      this.filteredSimScenario = this.filteredSimScenario.filter(x => x.status === "1");
      this.filterValues.status=value;
    }
    this.dataSource = new MatTableDataSource(this.filteredSimScenario);
  }
  clearFlypanelFilters(){
    this.filterValues = {
      provider: null,
      position: null,
      ila: null,
      difficultyLevel: null,
      status: null,
      activeStatus: null,
      simScenariosNotLinkedToILA: null,
    };
  }
}
