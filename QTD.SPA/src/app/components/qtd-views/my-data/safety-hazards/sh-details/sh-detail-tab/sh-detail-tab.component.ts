import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SaftyHazardCreateOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCreateOptions';
import { SafetyHazard_Set } from 'src/app/_DtoModels/SaftyHazard_Set/SafetyHazard_Set';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { SafetyHazardSetService } from 'src/app/_Services/QTD/safety-hazard-set.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-sh-detail-tab',
  templateUrl: './sh-detail-tab.component.html',
  styleUrls: ['./sh-detail-tab.component.scss']
})
export class ShDetailTabComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() isActive :boolean = true;
  @Input() shDescription: string = "";
  @Input() shImage: string = "";

  safetyHazardSets: SafetyHazard_Set[] | undefined;
  subscription = new SubSink();
  shTools: Tool[];
  shId = "";
  shSet: SafetyHazard_Set | undefined;
  edit = false;
  @Output() taskDeleteCheck  = new EventEmitter<any>();
  description = "";

  @ViewChild("ckeditor") ckeditor: any;
  onEventOrRequest(event: any) {
    this.ckeditor.instance.setData('');
  }
  Editor = ckcustomBuild;

  constructor(
    private shSetService: SafetyHazardSetService,
    private route: ActivatedRoute,
    private shService: SafetyHazardsService,
    private dataBroadcastService: DataBroadcastService,
    private flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private alert : SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
      this.readySetData();
      this.readyToolData();
    });

    this.subscription.sink = this.dataBroadcastService.updateMyDataNavBar.subscribe((_) => {
      this.safetyHazardSets = undefined;
      this.readySetData();
      this.readyToolData();
    })
  }

  ngOnDestroy(): void {

  }

  async readySetData() {
    this.safetyHazardSets = await this.shSetService.getForSpecificSH(this.shId);

    if(this.safetyHazardSets.length > 0){
      this.taskDeleteCheck.emit(true);
    }
    else if(this.safetyHazardSets.length == 0){
      this.taskDeleteCheck.emit(false);
    }
    
  }

  async readyToolData() {
    this.shTools = await this.shService.getToolsLinkedToSH(this.shId);
  }

  async openSHSetFlyPanel(templateRef: any) {
    this.shSet = undefined;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async opeSHPanelInEdit(templateRef: any, set: SafetyHazard_Set) {
    this.shSet = set;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  refreshData() {
    this.safetyHazardSets = undefined;
    this.readySetData();
  }

  async updateDescription(){
    var options = new SaftyHazardCreateOptions();
    options.text = this.description;

    this.shDescription = options.text;
    this.shService.updateOnlyDescription(this.shId,options).then(async (_)=>{
      this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")} Description Updated`);
      this.edit = false;
    })
  }

}
