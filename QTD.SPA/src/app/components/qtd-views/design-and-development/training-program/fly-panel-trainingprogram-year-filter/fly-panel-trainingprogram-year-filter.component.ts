import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { range } from 'rxjs';

@Component({
  selector: 'app-fly-panel-trainingprogram-year-filter',
  templateUrl: './fly-panel-trainingprogram-year-filter.component.html',
  styleUrls: ['./fly-panel-trainingprogram-year-filter.component.scss']
})
export class FlyPanelTrainingprogramYearFilterComponent implements OnInit {
  filterByYear!: UntypedFormGroup;
  yearnow = new Date().getFullYear();
  range:number[] = [];
  @Input() trainingProgramList:any;
  @Output() closed = new EventEmitter<any>();
  @Output() filterYears = new EventEmitter<{ startYear: string, endYear:string }>();
  @Output() check = new EventEmitter<any>();
  endDateyear:any=[];
  startDateyear:any=[];
  constructor(private fb: UntypedFormBuilder) { }

  ngOnInit(): void {
    this.readytrainingDetailsForm()
    this.getyearsdropdown();
    this.readyYears();
  }
  readytrainingDetailsForm() 
  {
    this.filterByYear = this.fb.group({
      startYear: new UntypedFormControl(),
      endYear: new UntypedFormControl(),
     
    });
  }

  readyYears(){
    let endYear:any;
    let startYear:any;
    this.trainingProgramList.forEach((res)=>{
      endYear = res.endDate.split('-');
      startYear = res.startDate.split('-');
      this.endDateyear.push(Number(endYear[0]));
      this.startDateyear.push(Number(startYear[0]));
    });

    this.endDateyear = [...new Set(this.endDateyear)];
    this.startDateyear = [...new Set(this.startDateyear)];

    this.endDateyear = this.endDateyear.sort((a,b)=>b-a);
    this.startDateyear = this.startDateyear.sort((a,b)=>a-b);

    


  }



  closedfilter()
  {

      this.closed.emit('IA_Proc closed');  

  }

  getyearsdropdown()
  {
    for (let i = 0; i < 10; i++) {
      this.range.push(this.yearnow - i);
    }
  }
  applyFilter()
  {
    var yearStart = this.filterByYear.get('startYear')?.value;
    var yearEnd = this.filterByYear.get('endYear')?.value;
    this.filterYears.emit({ startYear:yearStart,endYear:yearEnd});
    this.check.emit();
  }
}
