import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { SimulatorScenario_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_VM';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { QTDService } from 'src/app/_Services/QTD/qtd.service';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { QtdUserVM } from '@models/QtdUser/QtdUserVM';
import { SimulatorScenario_CollaboratorPermissions_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_CollaboratorPermissions_VM';
import { SimulatorScenario_Collaborator_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Collaborator_VM';
import { SimulatorScenario_UpdateCollaborators_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateCollaborators_VM';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { cloneDeep } from 'lodash';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Component({
  selector: 'app-colloborator-simulator-scenario-modal',
  templateUrl: './colloborator-simulator-scenario-modal.component.html',
  styleUrls: ['./colloborator-simulator-scenario-modal.component.scss'],
})
export class ColloboratorSimulatorScenarioModalComponent implements OnInit {
  @Output() closed = new EventEmitter<Event>();
  @Input() inputSimulatorScenario_VM: SimulatorScenario_VM;
  @ViewChild('selectControl', { static: false }) selectControl!: MatSelect;
  collaborationForm: UntypedFormGroup;
  collaborationSimScenario: SimulatorScenario_CollaboratorPermissions_VM[] = [];
  userDetails: QtdUserVM[] = [];
  userDetailsList: QtdUserVM[];
  allUserDetails: QtdUserVM[];
  editor = ckcustomBuild;
  editorPermissionId : string;
  collaborators : SimulatorScenario_Collaborator_VM[] = [];
  collaboratorDataSource : MatTableDataSource<SimulatorScenario_Collaborator_VM> = new MatTableDataSource<SimulatorScenario_Collaborator_VM>();
  displayColumns : string[] = ["name" , "email" , "permission", "action"]
  
  constructor(
    private fb: UntypedFormBuilder,
    private simScenariosService: SimulatorScenariosService,
    private qtdService: QTDService,
    private alert: SweetAlertService,
    private dialogSrvc: MatDialog
  ) {}

  ngOnInit() {
    this.collaborators = cloneDeep(this.inputSimulatorScenario_VM?.collaborators) ?? [] ;
    this.collaboratorDataSource = new MatTableDataSource(this.collaborators);
    this.intializeCollaboratorForm();
    this.loadAsync();
  }
  
  async loadAsync(){
    await this.getQtdUserDetailAsync();
    await this.getCollabPermsForSimScenarioAsync();
  }

  intializeCollaboratorForm() {
    this.collaborationForm = this.fb.group({
      personnel: [null],
      message: [this.inputSimulatorScenario_VM?.message],
      searchPersonnelTxt:[null]
    });
  }

  async getQtdUserDetailAsync() {
    this.userDetails = await this.qtdService.getAllActiveAsync();
    this.allUserDetails = this.userDetails;
    this.userDetails = this.userDetails.filter(x=> !this.collaborators?.some(y=>y.userId === x.id));
    this.userDetailsList =this.userDetails;
  }

  async getCollabPermsForSimScenarioAsync() {
    this.collaborationSimScenario = await this.simScenariosService.getAllCollaboratorPermissionsAsync();
    this.editorPermissionId = this.collaborationSimScenario.find(x=>x.name == "Editor")?.id
  }

  removeCollaborator(collaborator: any) {
    const index = this.collaborators.indexOf(collaborator);
    if (index !== -1) {
      this.collaborators.splice(index, 1);
      this.collaboratorDataSource.data = this.collaborators;
    }
    let user = this.allUserDetails.find(x=> x.id == collaborator.userId);
    if(user != null){
      this.userDetails.push(user);
      this.userDetailsList.push(user);
      this.collaborationForm.get("personnel")?.setValue(null);
    }
  }

  onPersonnelSelect(event:any){
    const selectedUser = event.value;
    if (!this.collaborators.some(collab => collab.userId == selectedUser.id)) {
      this.collaborators.push({
        id: null,
        userId : selectedUser.id,
        name: selectedUser.person.firstName + ' ' + selectedUser.person.lastName,
        email: selectedUser.person.username,
        collaboratorPermissionId : this.editorPermissionId
      });
      this.collaboratorDataSource.data = this.collaborators;
    }
    this.userDetails = this.userDetails.filter(x => x != event.value);
    this.userDetailsList = this.userDetailsList.filter(x => x != event.value);
  }

  onPermissionChange(collaborator:SimulatorScenario_Collaborator_VM, event:any){
    collaborator.collaboratorPermissionId= event.value;
  }

  async shareFormValueAsync() {
    const updateCollaboratorsVM = new SimulatorScenario_UpdateCollaborators_VM();
    updateCollaboratorsVM.collaborators = this.collaborators;
    await this.simScenariosService.addCollaboratorScenarioAsync(this.inputSimulatorScenario_VM?.id, updateCollaboratorsVM).then((res) => {
      this.inputSimulatorScenario_VM.collaborators = res;
      this.alert.successToast("Simulator Scenario Collaborators Updated Successfully");
    });
    this.inputSimulatorScenario_VM.message = this.collaborationForm.get("message")?.value;
    await this.simScenariosService.updateAsync(this.inputSimulatorScenario_VM?.id,this.inputSimulatorScenario_VM);
    this.dialogSrvc.closeAll();
  }

  collaboratorSearch(){
    var searchValue = this.collaborationForm.get('searchPersonnelTxt')?.value;
    if (searchValue !== undefined && searchValue !== null) {
      searchValue = String(searchValue).toLowerCase();
    } else {
      searchValue = "";
    }
    this.userDetails = this.userDetailsList.filter(x => 
      `${x.person.firstName.trim().toLowerCase()} ${x.person.lastName.trim().toLowerCase()}`.includes(searchValue.trim().toLowerCase())
  );
  }
  resetSearch(){
    setTimeout(() => {
      this.collaborationForm.get('searchPersonnelTxt')?.setValue('');
      this.collaboratorSearch();
    }, 500);
  }

  handleKeydown(event: KeyboardEvent) {
    this.selectControl._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }
}
