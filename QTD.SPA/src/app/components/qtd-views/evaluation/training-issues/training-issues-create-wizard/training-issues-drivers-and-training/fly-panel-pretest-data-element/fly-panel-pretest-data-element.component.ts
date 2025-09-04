import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { TestDataVM } from '@models/Test/TestDataVM';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-pretest-data-element',
  templateUrl: './fly-panel-pretest-data-element.component.html',
  styleUrls: ['./fly-panel-pretest-data-element.component.scss']
})
export class FlyPanelPretestDataElementComponent implements OnInit {
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Output() closed = new EventEmitter<any>();
  checkListSelection = new SelectionModel<TestDataVM>(false);
  loader: boolean = false;
  pretestList: TestDataVM[]=[];
  originalPretestList: TestDataVM[]=[];
  filterSearchString: string = '';
  linkedId:string;
  providerList:any[] = [];
  ilaList:any[] = [];
  filterForm: UntypedFormGroup;
  constructor(
    public flyPanelService: FlyInPanelService,
    private testService : TestsService,
    public providerService:ProviderService,
    public ilaService:IlaService,
    private fb: UntypedFormBuilder
  ) { }

  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.getAllPretestsByTypeAsync();
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
      this.filterPretests();
    });
  }

  async getAllPretestsByTypeAsync(){
    this.loader = true;
    await this.testService.getAllTestsByTypeAsync('Pretest').then(async (res) => {
     this.pretestList = res;
     this.originalPretestList = Object.assign(this.pretestList);
     this.filterPretests();
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
    });
  }

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterPretests();
  }

  filterPretests() {
    const searchTerm = this.filterSearchString.trim().toLowerCase();
    const { status, providerId, ilaId } = this.filterForm.value;
  
    this.pretestList = this.originalPretestList.filter(item => {
      const matchesSearch = !searchTerm ||
        item?.testTitle?.toLowerCase().includes(searchTerm) ||
        item?.id?.toLowerCase().includes(searchTerm);
  
      const matchesStatus = status == null || item?.active == status;
  
      const matchesProvider = providerId == null || item?.providerId == providerId;
  
      const matchesIla = ilaId == null || item?.ilaId == ilaId;
  
      return matchesSearch && matchesStatus && matchesProvider && matchesIla;
    });
  }
  

  clearSearchString() {
    this.filterSearchString = "";
    this.filterPretests();
  }

  onChangepretest(selected: boolean, test: TestDataVM) {
    this.checkListSelection.clear();
    if (selected) {
      this.checkListSelection.select(test);
      this.linkedId = test.id;
    }
  }

  linkPretest() {
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
