import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { TrainingProgram } from '@models/TrainingProgram/TrainingProgram';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TrainingProgramTypeService } from 'src/app/_Services/QTD/training-program-type.service';
import { TrainingProgramsService } from 'src/app/_Services/QTD/training-programs.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-training-program-data-element',
  templateUrl: './fly-panel-training-program-data-element.component.html',
  styleUrls: ['./fly-panel-training-program-data-element.component.scss']
})
export class FlyPanelTrainingProgramDataElementComponent implements OnInit {
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Output() closed = new EventEmitter<any>();
  checkListSelection = new SelectionModel<TrainingProgram>(false);
  loader: boolean = false;
  trainingProgramList: TrainingProgram[]=[];
  originalTrainingProgramList: TrainingProgram[]=[];
  filterSearchString: string = '';
  linkedId:string;
  positionList:any[] = [];
  trainingProgramTypeList:any[] = [];
  filterForm: UntypedFormGroup;
  constructor(
    public flyPanelService: FlyInPanelService,
    public trainingProgramService:TrainingProgramsService,
    private fb: UntypedFormBuilder,
    private positionService:PositionsService,
    private trainingProgramTypeService:TrainingProgramTypeService
  ) { }

  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.getAllTrainingProgramAsync();
    this.getAllPositions();
    this.getAllTrainingProgramTypes();
    this.initializeFilterForm();
  }

  initializeFilterForm() {
    this.filterForm = this.fb.group({
      positionId: [null],
      trainingProgramTypeId: [null],
      status: [true] 
    });
  
    this.filterForm.valueChanges.subscribe(() => {
      this.filterTrainingProgram();
    });
  }

  async getAllTrainingProgramAsync(){
    this.loader = true;
    await this.trainingProgramService.getAll().then(async (res) => {
     this.trainingProgramList = res;
     this.originalTrainingProgramList = Object.assign(this.trainingProgramList);
     this.filterTrainingProgram();
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
    });
  }

  filterTrainingProgram() {
    const searchTerm = this.filterSearchString.trim().toLowerCase();
    const { status, positionId, trainingProgramTypeId } = this.filterForm.value;
  
    this.trainingProgramList = this.originalTrainingProgramList.filter(item => {
      const matchesSearch = !searchTerm ||
        item?.programTitle?.toLowerCase().includes(searchTerm) ||
        item?.id?.toLowerCase().includes(searchTerm);
  
      const matchesStatus = status == null || item?.active == status;
  
      const matchesPosition = positionId == null || item?.positionId == positionId;
  
      const matchesTrainingProgramType = trainingProgramTypeId == null || item?.trainingProgramTypeId == trainingProgramTypeId;
  
      return matchesSearch && matchesStatus && matchesPosition && matchesTrainingProgramType;
    });
  }

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterTrainingProgram();
  }


  clearSearchString() {
    this.filterSearchString = "";
    this.filterTrainingProgram();
  }

  onChangeTrainingProgram(selected: boolean, tp: TrainingProgram) {
    this.checkListSelection.clear();
    if (selected) {
      this.checkListSelection.select(tp);
      this.linkedId = tp.id;
    }
  }

  linkTrainingProgram() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

  async getAllPositions(){
    this.positionList = await this.positionService.getAllWithoutIncludes();
  }

  async getAllTrainingProgramTypes(){
    this.trainingProgramTypeList = await this.trainingProgramTypeService.getAll();
  }

  clearPosition() {
    this.filterForm.patchValue({ positionId: null});
  }
  
  clearTrainingProgramType() {
    this.filterForm.patchValue({ trainingProgramTypeId: null });
  }
  
  clearStatus() {
    this.filterForm.patchValue({ status: null });
  }

}
