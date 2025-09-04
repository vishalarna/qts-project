import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-linked-objectives',
  templateUrl: './fly-panel-linked-objectives.component.html',
  styleUrls: ['./fly-panel-linked-objectives.component.scss']
})
export class FlyPanelLinkedObjectivesComponent implements OnInit {
objectivesList:any=[];
showSpinner:boolean=false;
  @Input() ILAid:any;
    constructor(public flyPanelSrvc: FlyInPanelService ,private ilaService: IlaService) { }

  ngOnInit(): void
  {
  this.readyData();
  }

  async readyData(){

    this.showSpinner=true;
    let data= await this.ilaService.getLinkedEnablingObjectives(this.ILAid);
    this.objectivesList=data.map((x)=>{ return x.fullNumber+" - "+ x.description});
    this.showSpinner=false;
  }
  closeFlyPanel() {
    this.flyPanelSrvc.close();
  }



}
