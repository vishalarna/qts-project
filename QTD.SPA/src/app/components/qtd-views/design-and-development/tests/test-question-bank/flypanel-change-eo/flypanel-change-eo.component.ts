import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { TestItemChangeOptions } from 'src/app/_DtoModels/TestItem/TestItemChangeOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-change-eo',
  templateUrl: './flypanel-change-eo.component.html',
  styleUrls: ['./flypanel-change-eo.component.scss']
})
export class FlypanelChangeEoComponent implements OnInit {
  @Input() eoId:any = null;
  @Input() testItemId:any = "";
  eoData!:EnablingObjective;
  length = 0;

  addEO = false;
  @Output() closed = new EventEmitter<any>();
  @Output() changed = new EventEmitter<any>();
  reason:string = "";
  datepipe = new DatePipe('en-us');

  constructor(
    private EOService : EnablingObjectivesService,
    private testItemService : TestItemService,
    private alert : SweetAlertService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    if(this.eoId !== null){
      this.getEOData();

    }
}

  async getEOData(){
    this.eoData = await this.EOService.get(this.eoId);
  }

  changeEO(){
    var options = new TestItemChangeOptions();
    options.eoId = this.eoId === '' ? null:this.eoId;
    options.reason = this.reason;
    options.effectiveDate = this.datepipe.transform(Date.now(), "yyyy-MM-dd");
    
    this.testItemService.changeEO(this.testItemId,options).then(async (_)=>{
      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Changed For Selected EO");
      this.changed.emit();
    }).finally(()=>{

    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
