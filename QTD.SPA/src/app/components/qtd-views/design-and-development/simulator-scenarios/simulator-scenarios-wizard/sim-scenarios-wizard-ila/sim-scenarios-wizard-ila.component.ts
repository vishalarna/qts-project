import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  Component,
  Input,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { SimulatorScenarioILA_Link } from '@models/SimulatorScenarioILA_Link/SimulatorScenarioILA_Link';
import { SimulatorScenario_ILA_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_ILA_VM';
import { SimulatorScenario_Prerequisite_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Prerequisite_VM';
import { SimulatorScenario_Procedure_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Procedure_VM';
import { SimulatorScenario_UpdateILAs_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateILAs_VM';
import { SimulatorScenario_UpdatePrerequisites_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdatePrerequisites_VM';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-sim-scenarios-wizard-ila',
  templateUrl: './sim-scenarios-wizard-ila.component.html',
  styleUrls: ['./sim-scenarios-wizard-ila.component.scss'],
})
export class SimScenariosWizardIlaComponent implements OnInit {
  @Input() inputSimScenariosId: string;
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM =new SimulatorScenario_VM();
  @Input() mode:string;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('prerequisiteSort') prerequisiteSort: MatSort;
  @ViewChild('metaPaginator') set metaPaginator(paging: MatPaginator) {
    if (paging) this.ilaDataSource.paginator = paging;
  }
  @ViewChild('prerequisitePaginator') set prerequisitePaginator(
    paging: MatPaginator
  ) {
    if (paging) this.preRequisitesDataSource.paginator = paging;
  }
  simScanrioILAUpdateOptions: SimulatorScenario_UpdateILAs_VM;
  simScanrioPrerequisitesUpdateOptions: SimulatorScenario_UpdatePrerequisites_VM;
  preRequisitesDataSource: MatTableDataSource<any> =
    new MatTableDataSource<any>();
  ilaDataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  ilaSelection = new SelectionModel<any>(true, []);
  preReqSelection = new SelectionModel<any>(true, []);
  ilaForm: UntypedFormGroup;
  unlinkPreReqDesc: string;
  unlinkPreReqId: string;
  unlinkILADesc: string;
  unlinkIlaId: string;
  linkedILAIds: any[] = [];
  linkedPreRequisitesIds: any[] = [];
  displayRequisitesColumns: string[] = [
    'id',
    'number',
    'description',
    'action',
  ];
  displayILAsColumns: string[] = ['id', 'number', 'description', 'action'];
  isILALinkUnlink : boolean = false;
  isILAUnlink : boolean = false ;
  isPreReqLinkUnlink : boolean = false;
  isPreReqUnlink : boolean = false ;
  constructor(
    public dialog: MatDialog,
    private formBuilder: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private simSceariosService: SimulatorScenariosService,
    private labelPipe: LabelReplacementPipe,
    private alert: SweetAlertService
  ) {}

  async ngOnInit(): Promise<void> {
    this.initializeILAForm();
    await this.loadAsync();
    this.simScanrioILAUpdateOptions = new SimulatorScenario_UpdateILAs_VM();
    this.simScanrioPrerequisitesUpdateOptions =
      new SimulatorScenario_UpdatePrerequisites_VM();
  }

  initializeILAForm() {
    this.ilaForm = this.formBuilder.group({
      isAvailableForAll: [''],
    });
  }

  async loadAsync() {
    this.ilaDataSource.data = this.inputSimulatorScenario_VM?.ilAs;
    this.linkedILAIds =
      this.inputSimulatorScenario_VM?.ilAs?.map((ila) => ila?.ilaId) ?? [];
    setTimeout(() => {
      this.ilaDataSource.sort = this.sort;
    }, 1);
    this.preRequisitesDataSource.data =
      this.inputSimulatorScenario_VM?.prerequisites;
    this.linkedPreRequisitesIds =
      this.inputSimulatorScenario_VM?.prerequisites.map((pr) => pr?.ilaId) ??
      [];
    setTimeout(() => {
      this.preRequisitesDataSource.sort = this.prerequisiteSort;
    }, 1);
  }

  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  handleILAsLinked(event: any) {
    const newILAs = event;
    newILAs?.forEach((val) => {
      var ilaVm = new SimulatorScenario_ILA_VM(val.id, val.name);
      this.simScanrioILAUpdateOptions.setILAs(ilaVm);
    });
    this.inputSimulatorScenario_VM.ilAs.forEach((res) => {
      this.simScanrioILAUpdateOptions.setILAs(res);
    });
    this.updateSSILAs();
  }

  async updateSSILAs() {
    this.isILALinkUnlink = true;
    await this.simSceariosService
      .linkILAsToScenarios(
        this.inputSimScenariosId,
        this.simScanrioILAUpdateOptions
      )
      .then(async (res) => {
        this.ilaSelection.clear();
        this.inputSimulatorScenario_VM.ilAs = res;
        this.linkedILAIds = this.inputSimulatorScenario_VM.ilAs.map(
          (k) => k.ilaId
        );
        this.ilaDataSource.data = this.inputSimulatorScenario_VM.ilAs;
        this.alert.successToast('Simulator Scenario ' +
          (await this.labelPipe.transform('ILA')) + 's Updated Successfully'
        );
      }).finally(()=> {
        this.isILALinkUnlink = false;
        this.isILAUnlink = false;
      });
  }

  async unlinkIlaModal(templateRef: any, row?: SimulatorScenario_ILA_VM) {
    this.unlinkILADesc = `You are selecting to unlink `  + await this.labelPipe.transform('ILA') +`  <b>${row.number} - ${row.description}</b> from the Simulator Scenario.`;
    this.unlinkIlaId = row.ilaId;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  unlinkPreRequisiteModal(templateRef: any, row?: SimulatorScenario_Prerequisite_VM) {
    this.unlinkPreReqDesc = `You are selecting to unlink Pre-requisite <b>${row.number} - ${row.description}</b> from the Simulator Scenario.`;
    this.unlinkPreReqId = row.ilaId;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async unlinkILAsModalAsync(templateRef: any) {
    this.unlinkILADesc = `You are selecting to unlink the following ${await this.labelPipe.transform('ILA')}s from the Simulator Scenario.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  unlinkIlaAsync() {
    this.simScanrioILAUpdateOptions.iLAs = [];
    this.inputSimulatorScenario_VM.ilAs.forEach((val) => {
      this.simScanrioILAUpdateOptions.setILAs(val);
    });
    const index = this.simScanrioILAUpdateOptions.iLAs.findIndex(
      (r) => r.ilaId === this.unlinkIlaId
    );
    if (index !== -1) {
      this.simScanrioILAUpdateOptions.iLAs.splice(index, 1);
    }
    this.updateSSILAs();
  }

  unlinkPreRequisitesModal(templateRef: any) {
    this.unlinkPreReqDesc = `You are selecting to unlink the following Pre-requisites from the Simulator Scenario.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  unlinkPreReqAsync() {
    this.simScanrioPrerequisitesUpdateOptions.prerequisites = [];
    this.inputSimulatorScenario_VM.prerequisites.forEach((val) => {
      this.simScanrioPrerequisitesUpdateOptions.setPrerequisites(val);
    });
    const index =this.simScanrioPrerequisitesUpdateOptions.prerequisites.findIndex((r) => r.ilaId === this.unlinkPreReqId );
    if (index !== -1) {
      this.simScanrioPrerequisitesUpdateOptions.prerequisites.splice(index, 1);
    }
    this.updateSSPreRequisites();
  }

  async removeILAAsync() {
    this.simScanrioILAUpdateOptions = new SimulatorScenario_UpdateILAs_VM();
    this.ilaSelection.selected.forEach((val) => {
      var ssILAIndex = this.inputSimulatorScenario_VM.ilAs.findIndex(
        (r) => r.id == val.id
      );
      this.inputSimulatorScenario_VM.ilAs.splice(ssILAIndex, 1);
      this.simScanrioILAUpdateOptions.iLAs =
        this.inputSimulatorScenario_VM.ilAs;
    });
    await this.updateSSILAs();
  }

  async handlePreRequisitesLinked(event: any) {
    const preRequisites = event;
    preRequisites?.forEach((val) => {
      var preRequisitesVm = new SimulatorScenario_Prerequisite_VM(
        val.id,
        val.name
      );
      this.simScanrioPrerequisitesUpdateOptions.setPrerequisites(
        preRequisitesVm
      );
    });
    this.inputSimulatorScenario_VM.prerequisites.forEach((res) => {
      this.simScanrioPrerequisitesUpdateOptions.setPrerequisites(res);
    });
   await this.updateSSPreRequisites();
  }

  async updateSSPreRequisites() {
    this.isPreReqLinkUnlink = true;
    await this.simSceariosService
      .linkPreReqsToScenarios(
        this.inputSimScenariosId,
        this.simScanrioPrerequisitesUpdateOptions
      )
      .then(async (res) => {
        this.preReqSelection.clear();
        this.inputSimulatorScenario_VM.prerequisites = res;
        this.linkedPreRequisitesIds = this.inputSimulatorScenario_VM.prerequisites.map((k) => k.ilaId);
        this.preRequisitesDataSource.data = this.inputSimulatorScenario_VM.prerequisites;
        this.alert.successToast('Simulator Scenario Pre-requisites Updated Successfully');
      })
      .finally(()=>{
        this.isPreReqLinkUnlink = false;
        this.isPreReqUnlink = false;
      });
  }

  removePreRequisitesAsync() {
    this.simScanrioPrerequisitesUpdateOptions =
      new SimulatorScenario_UpdatePrerequisites_VM();
    this.preReqSelection.selected.forEach((val) => {
      var ssPrerequisitesIndex = this.inputSimulatorScenario_VM.prerequisites.findIndex((r) => r.id == val.id);
      this.inputSimulatorScenario_VM.prerequisites.splice(ssPrerequisitesIndex,1);
      this.simScanrioPrerequisitesUpdateOptions.prerequisites =this.inputSimulatorScenario_VM.prerequisites;
      });
    this.updateSSPreRequisites();
  }

  isAllILAsSelected() {
    const numSelected = this.ilaSelection.selected.length;
    const numRows = this.ilaDataSource.data.length;
    return numSelected === numRows;
  }

  masterToggleILAs() {
    this.isAllILAsSelected()
      ? this.ilaSelection.clear()
      : this.ilaDataSource.data.forEach((row) => this.ilaSelection.select(row));
  }

  isAllPreReqsSelected() {
    const numSelected = this.preReqSelection.selected.length;
    const numRows = this.preRequisitesDataSource.data.length;
    return numSelected === numRows;
  }

  masterTogglePreReqs() {
    this.isAllPreReqsSelected()
      ? this.preReqSelection.clear()
      : this.preRequisitesDataSource.data.forEach((row) =>
          this.preReqSelection.select(row)
        );
  }
}
