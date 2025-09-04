import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';

@Component({
  selector: 'app-flypanel-filter-tq-emp-by',
  templateUrl: './flypanel-filter-tq-emp-by.component.html',
  styleUrls: ['./flypanel-filter-tq-emp-by.component.scss']
})
export class FlypanelFilterTqEmpByComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() filterSelected = new EventEmitter<any>();
  @Output() positionSelected = new EventEmitter<string>();
  @Input() selectedPos = "";
  @Input() selectedStatus = "";
  @Input() mode: 'Position' | 'Status' | 'All' = 'Position';

  isLoading = false;
  positions: Position[] = [];
  selectedPositionName : string ="";
  statusData = ["Initial Qualification", "Pending", "In Progress", "Overdue","On Time"];

  filterForm = new UntypedFormGroup({});

  constructor(
    private posService: PositionsService,
  ) { }

  ngOnInit(): void {
    this.readyForm();
    if (this.mode === 'Position') {
      this.readyData();
    }
    else{
      this.filterForm.get('status')?.setValue(this.selectedStatus);
    }
  }

  async readyData() {
    this.positions = await this.posService.getAllWithoutIncludes();
    var pos = this.positions.filter((data)=>{
      return data.id === this.selectedPos;
    })[0];
    if(pos){
      this.filterForm.get('pos')?.setValue(pos.id);
    }
  }

  readyForm() {
    switch (this.mode) {
      case 'Position':
        this.filterForm.addControl('pos', new UntypedFormControl(''));
        break;
      case 'Status':
        this.filterForm.addControl('status', new UntypedFormControl('', Validators.required));
        break;
    }

  }

  selectPosition() {
    var pos = this.filterForm.get('pos')?.value;
    this.selectedPositionName = pos === "" ? "All Positions" : this.positions?.find(x => x.id == pos)?.positionTitle;
    this.filterSelected.emit({ for: "Position", data: pos });
    this.positionSelected.emit(this.selectedPositionName);
    
  }

  selectStatus() {
    var status = this.filterForm.get('status')?.value;
    this.filterSelected.emit({ for: "Status", data: status });
  }

}
