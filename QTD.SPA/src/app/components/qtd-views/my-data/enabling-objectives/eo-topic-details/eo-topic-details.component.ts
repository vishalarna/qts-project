import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { EO_CategoryDeleteOptions } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_CategoryDeleteOptions';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-eo-topic-details',
  templateUrl: './eo-topic-details.component.html',
  styleUrls: ['./eo-topic-details.component.scss']
})
export class EoTopicDetailsComponent implements OnInit,OnDestroy {
  isActive = true;
  isLoading = false;
  subscription = new SubSink();
  prevNumbers = "";
  topicId = "";
  topic: EnablingObjective_Topic;

  hasLinks = false;

  header = "";
  description = "";
  showReason = false;
  action = "";

  constructor(
    private route:ActivatedRoute,
    private eoCatService : EnablingObjectivesCategoryService,
    private dataBroadcastService : DataBroadcastService,
    private router : Router,
    public dialog : MatDialog,
    private alert : SweetAlertService,
    public flyPanelService : FlyInPanelService,
    private vcf : ViewContainerRef,
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      var splitString = String(res.id).split('-');
      this.prevNumbers = splitString[0];
      this.topicId = splitString[1];
      
      this.getTopicData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getTopicData(){
    this.topic = await this.eoCatService.getTopic(this.topicId);
    
    this.hasLinks = await this.eoCatService.checkTopicForLinks(this.topic.id);
  }

  deleteModal(templateRef:any){
    this.header = "Delete Topic";
    this.description = `You are selecting to delete Topic, "${this.prevNumbers}${this.topic.number} ${this.topic.title}".`;
    this.showReason = false;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  activeModal(templateRef:any,makeActive : boolean){
    this.header = `Make ${makeActive ? "Active" : "Inactive"}`;
    this.action = `${makeActive ? "Active" : "Inactive"}`;
    this.showReason = true;
    this.description = `You are selecting to make Topic "${this.prevNumbers}${this.topic.number} - ${this.topic.title}" ${makeActive ? "Active" : "Inactive"}.`
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getDataWithReason(e:any){
    var data = JSON.parse(e);
    var options = new EO_CategoryDeleteOptions();
    options.actionType = this.action;
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.eoCatService.deleteTopic(this.topicId,options).then((_)=>{
      this.alert.successToast(`Topic Successfully Made ${this.action}`);
      this.getTopicData();
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }).finally(()=>{
    });
  }

  async getData(){
    var options = new EO_CategoryDeleteOptions();
    options.actionType = "delete";
    await this.eoCatService.deleteTopic(this.topicId,options).then((_)=>{
      this.alert.successToast("Topic Successfully Deleted");
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.router.navigate(['my-data/enabling-objectives/overview']);
    }).finally(()=>{

    });
  }

  openFlyPanel(templateRef:any){
    const portal = new TemplatePortal(templateRef,this.vcf);
    this.flyPanelService.open(portal);
  }
}
