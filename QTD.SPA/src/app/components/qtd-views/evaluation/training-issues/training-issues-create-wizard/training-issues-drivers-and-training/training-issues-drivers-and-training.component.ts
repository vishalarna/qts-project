import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TrainingIssue_DataElementCategory_VM } from '@models/TrainingIssues/TrainingIssue_DataElementCategory_VM';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { TrainingIssue_DriverSubType_VM } from '@models/TrainingIssues/TrainingIssue_DriverSubType_VM';
import { TrainingIssue_DriverType_VM } from '@models/TrainingIssues/TrainingIssue_DriverType_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-training-issues-drivers-and-training',
  templateUrl: './training-issues-drivers-and-training.component.html',
  styleUrls: ['./training-issues-drivers-and-training.component.scss']
})
export class TrainingIssuesDriversAndTrainingComponent implements OnInit {
  @Input() inputTrainingIssue_Vm: TrainingIssue_VM
  @ViewChild("linkProcedure") linkProcedure: TemplateRef<any>;
  @ViewChild("linkRegularityRequirement") linkRegularityRequirement: TemplateRef<any>;
  @ViewChild("linkSafetyHazard") linkSafetyHazard: TemplateRef<any>;
  @ViewChild("linkTool") linkTool: TemplateRef<any>;
  @ViewChild("linkILA") linkILA: TemplateRef<any>;
  @ViewChild("linkMetaILA") linkMetaILA: TemplateRef<any>;
  @ViewChild("linkTest") linkTest: TemplateRef<any>;
  @ViewChild("linkPretest") linkPretest: TemplateRef<any>;
  @ViewChild("linkCbt") linkCbt: TemplateRef<any>;
  @ViewChild("linkTrainingProgram") linkTrainingProgram: TemplateRef<any>;
  @ViewChild("linktestItem") linktestItem: TemplateRef<any>;
  @ViewChild("linkTask") linkTask: TemplateRef<any>;
  @ViewChild("linkEnablingObjective") linkEnablingObjective: TemplateRef<any>;
  @ViewChild("linkMetaEnablingObjective") linkMetaEnablingObjective: TemplateRef<any>;
  @ViewChild("linkMetaTask") linkMetaTask: TemplateRef<any>;
  driversForm: UntypedFormGroup;
  trainingIssueDataElementCategoryList: TrainingIssue_DataElementCategory_VM[] = [];
  trainingIssueDriverTypeList: TrainingIssue_DriverType_VM[] = [];
  selectedDriverType: TrainingIssue_DriverType_VM = new TrainingIssue_DriverType_VM();
  selectedDriverSubType: TrainingIssue_DriverSubType_VM = new TrainingIssue_DriverSubType_VM();
  selectedCategoryType: TrainingIssue_DataElementCategory_VM = new TrainingIssue_DataElementCategory_VM();
  selectedElementType: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  trainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  loader: boolean = false;

  constructor(
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private trainingIssuesService: TrainingIssuesService,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService
  ) { }

  ngOnInit() {
    this.initializeDriversDetailsForm();
    this.loadAsync();
    this.trainingIssueDataElementVM = this.inputTrainingIssue_Vm?.dataElement;
  }

  initializeDriversDetailsForm() {
    this.driversForm = this.fb.group({
      driverTypeComments: new UntypedFormControl(this.inputTrainingIssue_Vm?.otherComments ?? null, [Validators.required])
    });
  }

  async loadAsync() {
    await this.getAllWithSubTypesAsync();
    await this.getAllDataElementsWithCategoriesAsync();
  }

  async getAllWithSubTypesAsync() {
    this.loader = true;
    await this.trainingIssuesService.getAllWithSubTypesAsync().then((res) => {
      this.trainingIssueDriverTypeList = res;
      this.selectedDriverType = this.trainingIssueDriverTypeList.find(x => x.id == this.inputTrainingIssue_Vm?.driverTypeId);
      if (this.selectedDriverType != null) {
        let subTypes = this.selectedDriverType.subTypes;
        this.selectedDriverSubType = subTypes.find(x => x.id == this.inputTrainingIssue_Vm?.driverSubTypeId);
      }
    }).catch((error) => {
      this.loader = false;

    });
  }

  onChangeDriverType(isChecked: boolean, type: TrainingIssue_DriverType_VM) {
    if (isChecked) {
      this.selectedDriverType = type;
      this.selectedDriverSubType = null;
      this.inputTrainingIssue_Vm.driverTypeId = type?.id;
      this.inputTrainingIssue_Vm.driverSubTypeId = null;
      this.inputTrainingIssue_Vm.otherComments = this.driversForm.get('driverTypeComments')?.value;
    }
  }

  onChangeDriverSubType(isChecked: boolean, subType: TrainingIssue_DriverSubType_VM, type: TrainingIssue_DriverType_VM) {
    if (isChecked) {
      this.selectedDriverType = type;
      this.selectedDriverSubType = subType;
      this.inputTrainingIssue_Vm.driverTypeId = type?.id;
      this.inputTrainingIssue_Vm.driverSubTypeId = this.selectedDriverType?.type != 'Other' ? subType?.id : null;
      this.inputTrainingIssue_Vm.otherComments = this.selectedDriverType?.type == 'Other' ? this.driversForm.get('driverTypeComments')?.value : null;
      if (this.selectedDriverType?.type != 'Other') {
        this.driversForm.get('driverTypeComments')?.setValue('');
      }
    }
  }

  onInputComments() {
    this.inputTrainingIssue_Vm.otherComments = this.driversForm.get('driverTypeComments')?.value;
  }

  async getAllDataElementsWithCategoriesAsync() {
    await this.trainingIssuesService.getAllDataElementsWithCategoriesAsync().then(async (res) => {
      this.trainingIssueDataElementCategoryList = res;
      this.selectedElementType = this.inputTrainingIssue_Vm?.dataElement;
      this.selectedCategoryType = this.trainingIssueDataElementCategoryList.find(category =>
        category.dataElementVMs.some(dataElementVM =>
          dataElementVM.dataElementType === this.inputTrainingIssue_Vm?.dataElement.dataElementType
        )
      );
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
    });
  }

  onChangeElementType(isChecked: boolean, element: TrainingIssue_DataElement_VM, category: TrainingIssue_DataElementCategory_VM) {
    if (isChecked) {
      this.selectedCategoryType = category;
      if(this.trainingIssueDataElementVM?.dataElementType == element.dataElementType){
        this.selectedElementType = this.trainingIssueDataElementVM;
      }
      else {
        element.dataElementId = null;
        this.selectedElementType = element;
      }
      this.openFlyInPanelForElement(element.dataElementType);
    }
  }

  openFlyInPanelForElement(type: string): string {
    let result: string;
    switch (type.toLocaleLowerCase()) {
      case "procedure":
        this.openFlyInPanel(this.linkProcedure);
        break;
      case "regulatoryrequirement":
        this.openFlyInPanel(this.linkRegularityRequirement);
        break;
      case "safetyhazard":
        this.openFlyInPanel(this.linkSafetyHazard);
        break;
      case "tool":
        this.openFlyInPanel(this.linkTool);
        break;
      case "ilascourses":
        this.openFlyInPanel(this.linkILA);
        break;
      case "metailascourses":
        this.openFlyInPanel(this.linkMetaILA);
        break;
      case "test":
        this.openFlyInPanel(this.linkTest);
        break;
      case "pretest":
          this.openFlyInPanel(this.linkPretest);
          break;
      case "computerbasedtraining":
        this.openFlyInPanel(this.linkCbt);
        break;
      case "trainingprogram":
        this.openFlyInPanel(this.linkTrainingProgram);
        break;
      case "testitem":
        this.openFlyInPanel(this.linktestItem);
        break;
      case "task":
        this.openFlyInPanel(this.linkTask);
        break;
      case "enablingobjective":
        this.openFlyInPanel(this.linkEnablingObjective);
        break;
      case "metaenablingobjective":
        this.openFlyInPanel(this.linkMetaEnablingObjective);
        break;
      case "metatask":
        this.openFlyInPanel(this.linkMetaTask);
        break;
      default:
        result = `Unhandled type: ${type}`;
    }
    return result;
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async linkElementDetails(elementDetail: TrainingIssue_DataElement_VM) {
    this.loader = true;
    await this.trainingIssuesService.UpdateDataElementAsync(elementDetail, this.inputTrainingIssue_Vm?.id).then(async (res) => {
      this.trainingIssueDataElementVM = res;
      this.inputTrainingIssue_Vm.dataElement = res;
      this.alert.successToast("Data Element Linked Successfully ")
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
    })

    this.loader = false;
  }

}
