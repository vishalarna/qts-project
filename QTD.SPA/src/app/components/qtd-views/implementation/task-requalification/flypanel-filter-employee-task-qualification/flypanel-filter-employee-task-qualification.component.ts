import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-flypanel-filter-employee-task-qualification',
  templateUrl: './flypanel-filter-employee-task-qualification.component.html',
  styleUrls: ['./flypanel-filter-employee-task-qualification.component.scss']
})
export class FlypanelFilterEmployeeTaskQualificationComponent implements OnInit {

  @Input() filterTaskEmpValue;
  filterEmpTaskQualForm : UntypedFormGroup;
  @Output() empTaskQualFilterChange = new EventEmitter<any>();
  public loader:boolean =false;
  constructor(
    private fb: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
  ) { }

  ngOnInit(): void {
    this.initializingEmpTaskQualFilterForm();
  }

  initializingEmpTaskQualFilterForm(){
    this.filterEmpTaskQualForm = this.fb.group({
      TasksLinkedTo: new UntypedFormControl(this.filterTaskEmpValue?.TasksLinkedTo),
      ReliabilityRelated: new UntypedFormControl(this.filterTaskEmpValue?.ReliabilityRelated),
      Status: new UntypedFormControl(this.filterTaskEmpValue?.Status),
      CriteriaMet: new UntypedFormControl(this.filterTaskEmpValue?.CriteriaMet),
      QualificationStatus: new UntypedFormControl(this.filterTaskEmpValue?.QualificationStatus),
    });
  }

  closeFlyPanel() {
    this.flyPanelService.close();
  }

  clearSelection(name: string) {
    this.filterEmpTaskQualForm.get(name)?.setValue(null);
  }

  applyFiltersClick(){
    this.filterTaskEmpValue = this.filterEmpTaskQualForm.value;
    this.empTaskQualFilterChange.emit(this.filterTaskEmpValue);
    this.flyPanelService.close();
  }

}
