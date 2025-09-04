import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { TrainingIssue_DriverSubType_VM } from '@models/TrainingIssues/TrainingIssue_DriverSubType_VM';
import { TrainingIssue_DriverType_VM } from '@models/TrainingIssues/TrainingIssue_DriverType_VM';
import { TrainingIssue_Severity_VM } from '@models/TrainingIssues/TrainingIssue_Severity_VM';
import { TrainingIssue_Status_VM } from '@models/TrainingIssues/TrainingIssue_Status_VM';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-filter-training-issues',
  templateUrl: './fly-panel-filter-training-issues.component.html',
  styleUrls: ['./fly-panel-filter-training-issues.component.scss'],
})
export class FlyPanelFilterTrainingIssuesComponent implements OnInit {
  @Input() filterTrainingIssuesValue;
  @ViewChild('driverSelect', { static: false }) driverSelect!: MatSelect;
  @ViewChild('driverSubTypeSelect', { static: false })
  driverSubTypeSelect!: MatSelect;
  @Output() trainingIssueFilterChange = new EventEmitter<any>();
  filterTrainingIssueForm: UntypedFormGroup;
  trainingIssueDriversTypes: TrainingIssue_DriverType_VM[] = [];
  trainingIssueSeverities: TrainingIssue_Severity_VM[] = [];
  trainingIssueStatuses: TrainingIssue_Status_VM[] = [];
  userStatuses: string[] = ['Active', 'Inactive'];
  selectDriverSubType: TrainingIssue_DriverSubType_VM[] = [];
  initialDriverData: any[] = [];
  intialDriverSubTypeData: any[] = [];
  public loader:boolean =false;
  constructor(
    private fb: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private trainingIssuesService: TrainingIssuesService
  ) {}

  async ngOnInit(): Promise<void> {
    this.initializeTrainingIssueForm();
    await this.loadAsync();
  }

  async loadAsync(){
    this.loader=true;
    await this.getAllTrainingIssueDriverType();
    await this.getAllTrainingIssueSeverity();
    await this.getAllTrainingIssueStatus();
    this.loader=false;
  }

  initializeTrainingIssueForm() {
    this.filterTrainingIssueForm = this.fb.group({
      dueDate: new UntypedFormControl(this.filterTrainingIssuesValue?.dueDate),
      driver: new UntypedFormControl(this.filterTrainingIssuesValue?.driver),
      driverSubType: new UntypedFormControl(this.filterTrainingIssuesValue?.driverSubType),
      severity: new UntypedFormControl(this.filterTrainingIssuesValue?.severity),
      status: new UntypedFormControl(this.filterTrainingIssuesValue?.status),
      activeStatus: new UntypedFormControl(this.filterTrainingIssuesValue?.activeStatus),
      searchDriverType: new UntypedFormControl(''),
      searchDriverSubType: new UntypedFormControl(''),
    });
  }

  closeFlyPanel() {
    this.flyPanelService.close();
  }

  clearSelection(name: string) {
    this.filterTrainingIssueForm.get(name)?.setValue(null);
    if(name === "driver"){
      this.filterTrainingIssueForm.get('driverSubType')?.setValue(null);
    }
  }

  async getAllTrainingIssueDriverType() {
    this.trainingIssueDriversTypes = await this.trainingIssuesService.getAllWithSubTypesAsync();
    this.initialDriverData = Object.assign(this.trainingIssueDriversTypes);
    if (this.filterTrainingIssuesValue?.driver) {
     this.selectDriverType(this.filterTrainingIssuesValue?.driver);
    }
  }

  selectDriverType(event: any) {
    const selectedType = this.trainingIssueDriversTypes.find(
      (type) => type.type === event
    );
    if (selectedType) {
      this.selectDriverSubType = selectedType.subTypes;
      this.intialDriverSubTypeData = Object.assign(this.selectDriverSubType);
    }
  }

  async getAllTrainingIssueSeverity() {
    this.trainingIssueSeverities = await this.trainingIssuesService.getAllSeveritiesAsync();
  }

  async getAllTrainingIssueStatus() {
    this.trainingIssueStatuses = await this.trainingIssuesService.getAllStatusesAsync();
  }

  driverTypeSearch() {
    var filterString =
      this.filterTrainingIssueForm.get('searchDriverType')?.value ?? "";
      this.trainingIssueDriversTypes = this.initialDriverData.filter((f) => {
      return f.type.toLowerCase().includes(filterString.toLowerCase());
    });
  }

  driverSubTypeSearch() {
    var filterString = this.filterTrainingIssueForm.get('searchDriverSubType')?.value ?? "";
    this.selectDriverSubType = this.intialDriverSubTypeData.filter((f) => {
      return f.subType.toLowerCase().includes(filterString.toLowerCase());
    });
  }

  applyFiltersClick() {
    this.filterTrainingIssuesValue = this.filterTrainingIssueForm.value;
    this.trainingIssueFilterChange.emit(this.filterTrainingIssuesValue);
    this.flyPanelService.close();
  }

  resetSearch(){
    setTimeout(() => {
      this.filterTrainingIssueForm.get('searchDriverType')?.setValue('');
      this.driverTypeSearch();
    }, 500);
  }
 
  handleKeydown(event: KeyboardEvent) {
    this.driverSelect._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

  resetDriverSubTypeSearch(){
    setTimeout(() => {
      this.filterTrainingIssueForm.get('searchDriverSubType')?.setValue('');
      this.driverSubTypeSearch();
    }, 500);
  }

  handleDriverSubTypeKeydown(event: KeyboardEvent){
    this.driverSubTypeSelect._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

}
