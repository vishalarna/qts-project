import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { CBT_ScormUploadVM } from '@models/Scorm/CBT_ScormUploadVM';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { ApiScormService } from 'src/app/_Services/QTD/Scorm/api.scorm.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-cbt-data-element',
  templateUrl: './fly-panel-cbt-data-element.component.html',
  styleUrls: ['./fly-panel-cbt-data-element.component.scss']
})
export class FlyPanelCbtDataElementComponent implements OnInit {
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Output() closed = new EventEmitter<any>();
  checkListSelection = new SelectionModel<CBT_ScormUploadVM>(false);
  loader: boolean = false;
  cbtList: CBT_ScormUploadVM[]=[];
  originalCbtList: CBT_ScormUploadVM[]=[];
  filterSearchString: string = '';
  linkedId:string;
  providerList:any[] = [];
  ilaList:any[] = [];
  filterForm: UntypedFormGroup;

  constructor(
    public flyPanelService: FlyInPanelService,
    public cbtScormService:ApiScormService,
    public providerService:ProviderService,
    public ilaService:IlaService,
    private fb: UntypedFormBuilder
  ) { }

  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.getAllCbtScormUploadAsync();
    this.initializeFilterForm();
    this.getAllProviders();
  }

  initializeFilterForm() {
    this.filterForm = this.fb.group({
      providerId: [null],
      ilaId: [null],
      status: [true] 
    });
  
    this.filterForm.valueChanges.subscribe(() => {
      this.filterCbt();
    });
  }

  async getAllCbtScormUploadAsync(){
    this.loader = true;
    await this.cbtScormService.getAllCbtScormUploadsAsync().then(async (res) => {
     this.cbtList = res;
     this.originalCbtList = Object.assign(this.cbtList);
     this.filterCbt();
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
    });
  }

  filterCbt() {
    const searchTerm = this.filterSearchString.trim().toLowerCase();
    const { status, providerId, ilaId } = this.filterForm.value;
  
    this.cbtList = this.originalCbtList.filter(item => {
      const matchesSearch = !searchTerm ||
        item?.name?.toLowerCase().includes(searchTerm) ||
        item?.id?.toLowerCase().includes(searchTerm);
  
      const matchesStatus = status == null || item?.active == status;
  
      const matchesProvider = providerId == null || item?.providerId == providerId;
  
      const matchesIla = ilaId == null || item?.ilaId == ilaId;
  
      return matchesSearch && matchesStatus && matchesProvider && matchesIla;
    });
  }
  

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterCbt();
  }

  clearSearchString() {
    this.filterSearchString = "";
    this.filterCbt();
  }

  onChangeCbt(selected: boolean, cbt: CBT_ScormUploadVM) {
    this.checkListSelection.clear();
    if (selected) {
      this.checkListSelection.select(cbt);
      this.linkedId = cbt.id;
    }
  }

  linkCbt() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

  async getAllProviders(){
    this.providerList = await this.providerService.getActiveProviders();
  }

  clearProvider() {
    this.filterForm.patchValue({ providerId: null, ilaId: null });
    this.ilaList = [];
  }
  
  clearIla() {
    this.filterForm.patchValue({ ilaId: null });
  }
  
  clearStatus() {
    this.filterForm.patchValue({ status: null });
  }
  
  async onProviderChange(providerId: number) {
    this.ilaList = await this.ilaService.getByProvider(providerId);
    this.filterForm.patchValue({ ilaId: null });
  }

}