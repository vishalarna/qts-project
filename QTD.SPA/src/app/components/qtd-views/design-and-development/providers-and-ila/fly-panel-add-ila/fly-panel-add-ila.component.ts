import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import {
  UntypedFormGroup,
  UntypedFormControl,
  Validators,
  UntypedFormBuilder,
} from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { ILATopicLinkOption } from '@models/ILA_Topic_Link/ILA_Topic_LinkOptions';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DeliveryMethod } from 'src/app/_DtoModels/DeliveryMethod/DeliveryMethod';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { ILACreateOptions } from 'src/app/_DtoModels/ILA/ILACreateOptions';
import { ILAHistory } from 'src/app/_DtoModels/ILA/ILAHistory';
import { ILAPositionLinkOption } from 'src/app/_DtoModels/ILA_Position_Link/ILA_Position_LinkOptions';
import { ILA_Topic } from 'src/app/_DtoModels/ILA_Topic/ILA_Topic';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { Provider } from 'src/app/_DtoModels/Provider/Provider';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DeliveryMethodeService } from 'src/app/_Services/QTD/delivery-methode.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TopicService } from 'src/app/_Services/QTD/ila_topic.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-ila',
  templateUrl: './fly-panel-add-ila.component.html',
  styleUrls: ['./fly-panel-add-ila.component.scss'],
})
export class FlyPanelAddIlaComponent implements OnInit, AfterViewInit {
  addILA: boolean = true;
  addProvider: boolean = false;
  addTopic: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  stepperOrientation: Observable<StepperOrientation>;
  positions: Position[] = [];
  positionList: any[] = [];
  topicList: any[] = [];
  step1Form: UntypedFormGroup;
  step2Form: UntypedFormGroup;
  step3Form: UntypedFormGroup;
  positionsControl = new UntypedFormControl([]);
  topicsControl = new UntypedFormControl([]);
  providers: Provider[] = [];
  topics: ILA_Topic[] = [];
  deliveryMethods: DeliveryMethod[] = [];
  positionIds: any[] = [];
  topicIds: any[] = [];
  datePipe = new DatePipe('en-us');
  ILANumber: Number = 0;
  showSpinner = false;
  providerCheck:any;
  providerLoader = false;
  topicLoader = false;
  @ViewChild('stepper') stepper:MatStepper;
  constructor(
    private fb: UntypedFormBuilder,
    private positionService: PositionsService,
    private deliveryMethodSrvc: DeliveryMethodeService,
    public breakpointObserver: BreakpointObserver,
    private providerSrvc: ProviderService,
    private topicSrvc: TopicService,
    private iLAService: IlaService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.readyStep1Form();
    this.readyStep2Form();
    this.readyStep3Form();
    this.getProviders();
    this.getTopics();
  }

  ngAfterViewInit(): void {
    this.readyILANumber();
  }

  selectedChanged(event: any) {
    if (event.selectedIndex === 1) {
      if (this.positions.length === 0) this.getPositions();
      if (this.deliveryMethods.length === 0) this.getDeliveryMethods();
    }
  }

  async getProviders() {
    this.providerLoader = true;
    await this.providerSrvc.getWihtoutIncludes().then((res) => {
      this.providers = res;
    }).finally(()=>{
      this.providerLoader = false;
    });
  }

  async getTopics() {
    this.topicLoader = true;
    await this.topicSrvc.getAll().then((res) => {
      this.topics = res;
    }).finally(()=>{
      this.topicLoader = false;
    });
    this.topicList.push(this.topicsControl)
  }

  async getPositions() {
    //for dynamic position dropdown
    await this.positionService
      .getAllWithoutIncludes()
      .then((i) => {
        this.positions = i;
      })
      .catch((err) => {
        console.error(err);
      });
    this.positionList.push(this.positionsControl);
  }

  readyILANumber(){
    this.iLAService.getILANumber().then((res: number) => {
        this.ILANumber = 1;
    })
  }

  async getDeliveryMethods() {
   /*  await this.deliveryMethodSrvc.getAll().then((res) => {
      this.deliveryMethods = res;
    }); */
  /*
 */

  this.deliveryMethodSrvc.getIsNerc(false).then((res)=>{
    this.deliveryMethods = res;
  })
  }

 /*  changeDelieveryMethod(){
    this.providers.forEach((res)=>{
      if(res.id === this.providerCheck && res.isNERC === true){
        this.deliveryMethods.forEach((res1:any)=>{
          if(res1.isNerc === true){
            return this.deliveryMethods;
          }
        })
      }
    })
  }
 */
  changeProvider(e:any){
    this.providers.forEach((res)=>{
      if(res.id === e){
        this.providerCheck = res.isNERC;
      }
    })
  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      providerId: new UntypedFormControl('', [Validators.required]),
      topics: new UntypedFormControl([]),
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      number: new UntypedFormControl('', [Validators.required]),
      title: new UntypedFormControl('', [Validators.required]),
      nickname: new UntypedFormControl(),
      positions: new UntypedFormControl([]),
      deliveryMethodId: new UntypedFormControl('',[Validators.required]),
      ILADescription: new UntypedFormControl('')
    });
  }

  readyStep3Form() {
    this.step3Form = this.fb.group({
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl('', Validators.required),
      addNew:new UntypedFormControl(false)
    });
  }

  removePosition(i: any) {
    const pos = this.positionsControl.value as Position[];
    this.removeFirst(pos, i);
    this.positionsControl.setValue(pos);
  }

  removeTopic(i: any) {
    const ilaTopics = this.topicsControl.value as ILA_Topic[];
    this.removeFirst(ilaTopics, i);
    this.topicsControl.setValue(ilaTopics);
}

  OnClick(selected: boolean) {
    if (selected) {
      this.positionList.push(this.positionsControl);
    }
  }

  OnTopicClick(selected: boolean) {
    if (selected) {
      this.topicList.push(this.topicsControl);
    }
  }

  private removeFirst(array: any[], toRemove: any): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  closePanel() {
    this.addProvider = false;
    this.addTopic = false;
    this.addILA = true;
  }

  openProviderPanel() {
    this.addProvider = true;
    this.addTopic = false;
    this.addILA = false;
  }

  openTopicPanel() {
    this.addProvider = false;
    this.addTopic = true;
    this.addILA = false;
  }

  async saveILA(){
    this.showSpinner = true;
    var options = new ILACreateOptions();
    options.providerId = this.step1Form.get("providerId")?.value;
    options.number = this.step2Form.get("number")?.value;
    options.name = this.step2Form.get("title")?.value;
    options.nickName = this.step2Form.get("nickname")?.value;
    if(this.step2Form.get("deliveryMethodId")?.value != "")
    {
      options.deliveryMethodId = this.step2Form.get("deliveryMethodId")?.value;
    }
   // options.deliveryMethodId = this.step2Form.get("deliveryMethodId")?.value;
    options.description = this.step2Form.get("ILADescription")?.value;
    options.effectiveDate = this.step3Form.get("effectiveDate")?.value;
    this.iLAService.create(options)
                .then((res) => {
                  this.showSpinner = false;
                  this.creatPositionLink(res.ilaId);
                  this.refreshTopicLinkAsync(res.ilaId);
                  this.createILAHistory(res.ilaId);
                })
              .catch(async (err: any) => {
      this.alert.errorToast("Error Saving " + await this.labelPipe.transform('ILA') + " Data");
    });
  }

  async createILAHistory(id: any){
    var histOptions = new ILAHistory();
      histOptions.changeEffectiveDate = this.step3Form.get("effectiveDate")?.value;
      histOptions.changeNotes = this.step3Form.get("reason")?.value;
      histOptions.newStatus = true;
      histOptions.oldStatus = false;
      histOptions.ILAId = id;
      await this.iLAService.createILAHistory(histOptions).then(async (res:ILAHistory)=>{
        this.refresh.emit();
        if(this.step3Form.get('addNew')?.value){
          this.resetAll();
        }
        else{
          this.closed.emit('fp-add-sh-cat-closed');
        }
        this.alert.successToast(await this.labelPipe.transform('ILA') + " and "+ await this.labelPipe.transform('ILA') + " History Saved Successfully.");

      }).catch(async (err:any)=>{
        this.alert.errorToast("Error Saving " + await this.labelPipe.transform('ILA') + " History");
      })
  }

  resetAll(){
    // this.step1Form.reset();
    // this.step2Form.reset();
    // this.step3Form.reset();
    this.readyStep1Form();
    this.readyStep2Form();

    this.step3Form.patchValue({
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      reason: new UntypedFormControl(''),
      addNew:new UntypedFormControl(false),
    })
    this.stepper.reset();
    this.step3Form.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'MM-dd-yyyy'));
  }


  async creatPositionLink(ilaId: any) {
    var options: ILAPositionLinkOption = new ILAPositionLinkOption();
    this.positionIds = [];
          this.positionsControl?.value.forEach((element: any) => {
            this.positionIds.push(element.id);
          });
    options.PositionIds = this.positionIds;
    await this.iLAService
      .linkPosition(ilaId, options)
      .then((res: any) => {

      })
      .catch((err) => {

      });
  }
  async refreshTopicLinkAsync(ilaId: any) {
    var updateLinkOptions : ILATopicLinkOption = new ILATopicLinkOption();
    this.topicIds = [];
          this.topicsControl?.value.forEach((element: any) => {
            this.topicIds.push(element.id);
          });
    updateLinkOptions.topicIds = Array.from(new Set(this.topicIds));
    await this.iLAService.updateLinkedILATopicsAsync(ilaId,updateLinkOptions).then((res)=>{
      });
  }
}
