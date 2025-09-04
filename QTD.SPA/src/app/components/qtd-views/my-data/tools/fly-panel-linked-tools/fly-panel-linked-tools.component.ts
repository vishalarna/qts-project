import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-linked-tools',
  templateUrl: './fly-panel-linked-tools.component.html',
  styleUrls: ['./fly-panel-linked-tools.component.scss']
})
export class FlyPanelLinkedToolsComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Input() Id: any;

  @Input() Title: string;
  @Input() selectedType: string;
  @Input() ilaNumber:any;
  @Input() shNumber:any;
  @Input() eoNumber:any;
  @Input() taskNumber:any;
  @Input() regNumber:any;
  linkedTools: Tool[] = [];
  selectedTypeNumber:any;
  constructor(
    private toolService:ToolsService,
    private alert : SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    switch (this.selectedType) {
      case "Task":
        this.readyTaskData();
        break;
      case "Skill":
        this.readySkillData();
        break;
      case "SH":
        this.readySHData();
        break;
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  readyTaskData() {
    this.selectedTypeNumber = this.taskNumber;
    let tempSrc: any[] = [];
    this.toolService.getToolsLinkedToTask(this.Id).then((res)=>{
       res.forEach((data:any)=>{
        tempSrc.push({number:data.number,description:data.name,id:data.id, active:data.active});
      });
      this.linkedTools = tempSrc;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching Linked " + await this.transformTitle('Task') + "s Data ");
    });
  }

  readySkillData(){
    this.selectedTypeNumber = this.taskNumber;
    let tempSrc: any[] = [];
    this.toolService.getToolsLinkedToSkill(this.Id).then((res)=>{
       res.forEach((data:any)=>{
        tempSrc.push({number:data.number,description:data.name,id:data.id, active:data.active});
      });
      this.linkedTools = tempSrc;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching Linked " + await this.transformTitle('Task') + "s Data ");
    });
  }

  readySHData(){
    this.selectedTypeNumber = this.taskNumber;
    let tempSrc: any[] = [];
    this.toolService.getToolsLinkedToSH(this.Id).then((res)=>{
       res.forEach((data:any)=>{
        tempSrc.push({number:data.number,description:data.name,id:data.id, active:data.active});
      });
      this.linkedTools = tempSrc;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching Linked " + await this.transformTitle('Task') + "s Data ");
    });

  }



  
}
