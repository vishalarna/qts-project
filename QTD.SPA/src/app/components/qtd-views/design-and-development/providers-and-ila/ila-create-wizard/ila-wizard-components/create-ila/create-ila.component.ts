import { TemplatePortal } from '@angular/cdk/portal';
import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import {
  ControlContainer,
  Form,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { Observable, Subscription } from 'rxjs';
import { DeliveryMethod } from 'src/app/_DtoModels/DeliveryMethod/DeliveryMethod';
import { DeliveryMethodeCreateOption } from 'src/app/_DtoModels/DeliveryMethod/DeliveryMethodeCreateOption';
import { ILACreateOptions } from 'src/app/_DtoModels/ILA/ILACreateOptions';
import { ILAPositionLinkOption } from 'src/app/_DtoModels/ILA_Position_Link/ILA_Position_LinkOptions';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { DeliveryMethodeService } from 'src/app/_Services/QTD/delivery-methode.service';
import { IlaPositionLinkService } from 'src/app/_Services/QTD/ila-position-link.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TopicService } from 'src/app/_Services/QTD/ila_topic.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { EventEmitter} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { saveILA } from 'src/app/_Statemanagement/action/state.componentcommunication';
import { skip } from 'rxjs/operators';
import { SubSink } from 'subsink';
import { Location } from '@angular/common';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ILATopicLinkOption } from '@models/ILA_Topic_Link/ILA_Topic_LinkOptions';
import { ILA_Topic } from '@models/ILA_Topic/ILA_Topic';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';

@Component({
  selector: 'app-create-ila',
  templateUrl: './create-ila.component.html',
  styleUrls: ['./create-ila.component.scss'],
})
export class CreateIlaComponent implements OnInit, OnDestroy {
  createILAForm!: UntypedFormGroup;
  @Output() dynamicName = new EventEmitter<any>();

  @Input() ILAName: string;
  @Input() ILANumber: string;
  @Input() ILADescription: string = '';
  @Input() isMetaILA: boolean = false;
  @Input() tabchange_bool: boolean;
  @Input() ILAdescription: string;
  provider_edit_mode: boolean;
  provider_change_mode: boolean;
  change_topic: boolean;
  edit_topic: boolean;
  editUpdateCheck: boolean = false;
  editILAId!: any;

  @Output() formChanges = new EventEmitter();
  @Output() editILACheck = new EventEmitter();


  imageInBase64: string | undefined = '';
  uploadedImage: string;
  selectedPositionId: string;
  position: Position[] = [];
  positions: Position[] = [];
  positionList: any[] = [];
  topicList: any[] = [];
  GeneralDeliveryMethod: DeliveryMethod[] = [];
  NERCMethods: DeliveryMethod[] = [];
  allDeliveryMethods: DeliveryMethod[] = [];
  NERCMethodsLength: any;
  positionsControl = new UntypedFormControl([]);
  topicsControl = new UntypedFormControl([]);
  provider_list: any[] = [];
  topic_list: any[] = [];
  ilaid:any = 0;
  saveEvent: Observable<any>;
  // savesubscription: Subscription;
  // deletesubscription: Subscription;
  url: any;
  img = '';
  image_name: string = 'No image selected';
  positionIds: any[] = [];
  topicIds:string[]=[];
  positionSelected : any []=[];
  saving: Observable<any>;
  ila: ILACreateOptions = new ILACreateOptions();
  previousPositionIds: any[] = [];
  originalProviderData:any[] =[];
  allowedTypes = [
    'image/jpg',
    'image/jpeg',
    'image/bmp',
    'image/png',
    'image/svg',
  ];
  url_display: any;
  subscription = new SubSink();
  editCheck: boolean = false;
  isNercBit: boolean = false;
  mainSpinner = false;
  shouldLoad = true;
  isIlaPublished: boolean;
  useForEMPILA: boolean;
  @Output() loadingEvent = new EventEmitter<boolean>();
  @Output() isILASaved = new EventEmitter<boolean>();
  @ViewChild('provSelect', { static: false }) provSelect!: MatSelect;
  currentIlaId:any;
  constructor(
    private fb: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private positionService: PositionsService,
    private translate: TranslateService,
    private ilaTopicService: TopicService,
    private providerService: ProviderService,
    private dataBroadcast: DataBroadcastService,
    private saveStore: Store<{ saveIla: any }>,
    private deleteStore: Store<{ deleteIla: any }>,
    private ilaService: IlaService,
    private activatedRoute: ActivatedRoute,
    private deliveryMethodService:DeliveryMethodeService,
    private alert: SweetAlertService,
    private location: Location,
    private router: Router,
    public dialog: MatDialog,
    private labelPipe: LabelReplacementPipe,
  ) {
    this.saveEvent = this.saveStore.select('saveIla');
  }

  ngOnDestroy(): void {

    // this.savesubscription.unsubscribe();
    // this.deletesubscription.unsubscribe();
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    setTimeout(() => {
      this.provSelect._handleKeydown = (event: KeyboardEvent) => {
        if (event.key === 'SPACE')
          return;
      };
    },1000);
    this.loadingEvent.emit(true);
    // this.subscription.sink = this.saveStore.select('saveIla').pipe().subscribe((res) => {
    //   if (res['saveData'] !== undefined && res['saveData']['result'] !== undefined) {
    //     this.ilaid = res['saveData']['result']['id'];
    //     this.readyILAbyId(this.ilaid);
    //   }
    // })

    this.readyCreateILAForm();

    this.subscription.sink = this.activatedRoute.queryParams
      .subscribe(async (params) => {
        this.mainSpinner = this.shouldLoad;
        this.currentIlaId = params?.data;
        await this.getProviders();
        await this.getPositions();
        await this.getTopic();

        if (params.data !== undefined) {
          this.editUpdateCheck = true;
          this.editILACheck.emit(this.editUpdateCheck);

          this.readyDeliveryMethods(params.data);
          this.readyILAbyId(params.data);
        }
        else{
          this.mainSpinner = false;
        }
      });

    this.subscription.sink = this.deleteStore
      .pipe(select('deleteIla'), skip(1))
      .subscribe(async (res) => {
        if (this.ilaid !== 0) {
          this.ilaService
            .delete(this.ilaid)
            .then(async (res) => {
              this.alert.successToast(await this.labelPipe.transform('ILA') +' Delelted Successfully');
              this.createILAForm.reset();
              this.positionsControl.reset();
              this.topicsControl.reset();
              this.url = '';
              this.image_name = '';
              this.formChanges.emit('DELETE');
            })
            .catch((err) => {

            });
        } else {
          this.alert.errorAlert(
            await this.labelPipe.transform('ILA') + ' Not Created Yet',
            'Please Create an ' + await this.labelPipe.transform('ILA') + ' First to delete'
          );
        }
      });

    this.subscription.sink = this.saveStore
      .pipe(select('saveIla'))
      //this.savesubscription = this.saveStore
      //.pipe(select('saveIla'), skip(1))
      .subscribe((res) => {
        // These are conditions to control ngrx event dispatch.
        if (res.tabIndex === 0 && res.update !== undefined) {

          this.ila.description =
            this.createILAForm.get('ILADescription')?.value;
          this.ila.number = this.createILAForm.get('ILANumber')?.value;
          this.ila.name = this.createILAForm.get('ILAName')?.value;
          this.ila.nickName = this.createILAForm.get('nickName')?.value;
          this.ila.image = this.imageInBase64 ? this.imageInBase64 : '';
          this.ila.deliveryMethodId = this.createILAForm.get('deliveryMethode')?.value;
          this.ila.isAvailableForAllILA=this.createILAForm.get('IsAvailableForAllIla')?.value;
          this.ila.ilaDeliveryMethod=this.createILAForm.get('other')?.value;
          this.ila.hasPilotData = false;
          this.ila.isOptional = false;
          this.ila.isProgramManual = false;
          this.ila.isPublished = false;
          this.ila.isSelfPaced = this.createILAForm.get('IsSelfPacedILA')?.value;;
          this.ila.providerId = this.createILAForm.get('provider')?.value;
          // In case user updates positions we need old position ids in order to unlink them.
          //this.previousPositionIds = this.positionIds;
          this.positionIds = [];
          this.topicIds=[];
          this.positionsControl?.value.forEach((element: any) => {
            this.positionIds.push(element.id);
          });
          this.topicsControl?.value.forEach((element: any) => {
            this.topicIds.push(element.id);
          });
          if (this.ila.description !== null) {
            //update
            if (res.update === true) {
              this.isILASaved.emit(true);
              this.ilaService
                .update(this.ilaid, this.ila)
                .then(async (res) => {
                  await this.refreshPositionLink(this.ilaid);
                  await this.refreshTopicLinkAsync(this.ilaid);
                  // this.router.navigate([`/dnd/ila/create`],{queryParams:{data:res.id}});
                  await this.readyDeliveryMethods(this.ilaid);
                  this.createILAForm.patchValue({
                    deliveryMethode: res.deliveryMethodId,
                    other: "",
                    IsAvailableForAllIla:false
                  });
                  this.formChanges.emit('UPDATED');
                  this.deliveryMethodSelected();
                  this.isILASaved.emit(false);
                })
                .catch((err) => {
                  this.formChanges.emit('FAILED');
                });
              //create
            } else if (res.update === false && this.editUpdateCheck === false) {
              this.isILASaved.emit(true);
              this.ilaService
                .create(this.ila)
                .then(async (res) => {
                  this.ilaid = res.ilaId;
                  this.shouldLoad = false;
                  this.router.navigate(['/dnd/ila/create'],{queryParams:{data:this.ilaid,isCreateMode:true}});
                  this.creatPositionLink(res.ilaId);
                   this.refreshTopicLinkAsync(this.ilaid);
                   this.readyDeliveryMethods(this.ilaid);
                  this.createILAForm.patchValue({
                    deliveryMethode: res.deliveryMethodId,
                    other:""
                  });
                  this.isILASaved.emit(false);
                  // this.router.navigate([`/dnd/ila/create`],{queryParams:{data:res.id}});
                })
                .catch(async (err) => {
                  this.formChanges.emit('FAILED');
                  this.alert.errorAlert(
                    'Record Already Exists',
                    'The ' + await this.labelPipe.transform('ILA') + ' you are trying to create already exists'
                  );
                });
            }
            //edit and update
            else {
              this.isILASaved.emit(true);
              this.ilaService
                .update(this.editILAId, this.ila)
                .then(async (res) => {
                 await this.refreshPositionLink(this.editILAId);
                 await this.refreshTopicLinkAsync(this.editILAId);
            await this.readyDeliveryMethods(this.editILAId);
            this.createILAForm.patchValue({
              deliveryMethode: res.deliveryMethodId,
              other: "",
              IsAvailableForAllIla:false
            });
                  this.formChanges.emit('VALID');
                  this.deliveryMethodSelected();
                  this.isILASaved.emit(false);
                })
                .catch((err) => {
                  this.formChanges.emit('FAILED');
                });
            }
          }
        this.dynamicName.emit(this.ila.name + ' - ' + this.ila.number);

        }
       
      });

    this.subscription.sink = this.activatedRoute.data.subscribe((res) => {
      this.allDeliveryMethods = res['deliveryMethods'];
      this.GeneralDeliveryMethod = this.allDeliveryMethods.filter(
        (data) => !data.isNerc || data.isAvailableForAllIlas===true || data.creatorIlaId===this.ilaid
      );
      this.NERCMethods = this.allDeliveryMethods.filter((data) => data.isNerc || data.isAvailableForAllIlas===true || data.creatorIlaId===this.editILAId);
    });

    // This is the logic for enabling and disabling of continue button on parent component
    this.readyContinueButtonEnableLogic();

    // Dynamically fillig providers and topics dropdown
    this.dataBroadcast.providerSaved.subscribe((res) => {

      this.getProviders();
    });
    this.dataBroadcast.topicSaved.subscribe(async (res) => {
      
      await this.getTopic();
    });

    this.subscription.sink = this.dataBroadcast.refreshListName.subscribe(async (res: any) => {
      if (res === 'positions') {
        await this.getPositions();
      }
    });

     this.createILAForm.statusChanges.subscribe((status) => {
    this.formChanges.emit(status === 'VALID' ? 'VALID' : 'INVALID');
  });
    this.loadingEvent.emit(false);
  }
  deliveryMethodSelected(){
    if (this.createILAForm.get('deliveryMethode')?.value!==null) {
      this.createILAForm.get('deliveryMethode')?.setValidators(Validators.required);
      this.createILAForm.get('other')?.clearValidators();
      this.createILAForm.get('other')?.setValue('');
      this.createILAForm.updateValueAndValidity();
    }
  }


  deliveryMethodTextSelected(){
    if (this.createILAForm.get('other')?.value!=='') {
      this.createILAForm.get('other')?.setValidators(Validators.required);
      this.createILAForm.get('deliveryMethode')?.clearValidators();
      this.createILAForm.get('deliveryMethode')?.setValue(null);
    this.createILAForm.updateValueAndValidity();
    }
  }
  originalValues!:any;
  async readyDeliveryMethods(ilaId:any){

   await this.deliveryMethodService.getAll().then(async(res)=>{
    this.allDeliveryMethods = res;
    this.GeneralDeliveryMethod = this.allDeliveryMethods.filter(
      (data) => !data.isNerc || data.isAvailableForAllIlas===true || data.creatorIlaId===ilaId
    );
    this.NERCMethods = this.allDeliveryMethods.filter((data) => data.isNerc || data.isAvailableForAllIlas===true || data.creatorIlaId===ilaId);

    });
  }
  async readyILAbyId(id: any) {
    await this.ilaService.get(id).then(async (res) => {
      if (res) {
        this.createILAForm.patchValue({
          ILAName: res.name,
          nickName: res.nickName,
          ILANumber: res.number,
          ILADescription: res.description,
          provider: res.providerId,
          deliveryMethode: res.deliveryMethodId,
          other:"",
          IsSelfPacedILA:res.isSelfPaced,

        });
        this.originalValues = this.createILAForm.value;
        /*this.dynamicName.emit({name:res.name, Nickname:res.nickName});*/
        this.editILAId = res.id;
        this.isIlaPublished=res.isPublished;
        this.useForEMPILA=res.useForEMP;
        await this.readyPositionByILAId(id);
        await this.readyTopicsByILAIdAsync(id);
        this.url =  (this.ilaService.baseUrlForImage+res.image) ? (this.ilaService.baseUrlForImage+res.image) : '';
        this.saveStore.dispatch(saveILA({ saveData: { result: res }, tabIndex: 1, update: true }));
        this.isNercBit = await this.ilaService.checkIsProviderNerc(this.editILAId);
        this.isProviderNerc.emit(this.isNercBit);
        if(res.isPubliclyAvailable){
          const control = this.createILAForm.get('nickName');
          control.setValidators([Validators.required]);
          control.updateValueAndValidity();
          control.markAsTouched();
          control.markAsDirty();
          
        }
      }
    }).catch((err) => {

    })
  }

  async readyPositionByILAId(id: any) {
    let array: any[] = [];
    this.previousPositionIds = [];
    await this.ilaService.getLinkedPositions(id).then((res) => {
      res.forEach((i) => {
        var position = this.positions.find(x => x.id == i.id);
        if (position !== undefined && position !== null) {
          array.push(position);
          this.previousPositionIds.push(position.id);
        }
      });

      this.createILAForm.patchValue({
        positionsControl: array
      });
      this.positionsControl.setValue(array);
      this.positionsControl.updateValueAndValidity();
      if(this.createILAForm.valid){
        this.formChanges.emit('VALID');
      }
      this.mainSpinner = false;
    });

    //this.positionsControl.patchValue(this.createILAForm.get('positionsControl'));
    //this.positionsControl = this.createILAForm.get('positionsControl')

    //
  }

  async readyTopicsByILAIdAsync(id: any) {
    let array: any[] = [];
    await this.ilaService.getLinkedILATopicsAsync(id).then((res) => {
      res.forEach((i) => {
        var topic = this.topic_list.find(x => x.id == i.id);
        if (topic !== undefined && topic !== null) {
          array.push(topic);
        }
      });

      this.createILAForm.patchValue({
        topicsControl: array
      });
      this.topicsControl.setValue(array);
      this.topicsControl.updateValueAndValidity();
      if(this.createILAForm.valid){
        this.formChanges.emit('VALID');
      }
      this.mainSpinner = false;
    });
  }
  @Output() isProviderNerc = new EventEmitter<boolean>();

  selectProvider(event: any, isNerc: any) {
    if (event.isUserInput) {
      //this.ila.providerId = event.source.value;
      if (isNerc !== this.isNercBit) {
        this.isProviderNerc.emit(true);
        if (this.ila.providerId === event.source.value) {
          this.createILAForm.get('deliveryMethode')?.setValue(this.originalValues.deliveryMethode);
          this.createILAForm.updateValueAndValidity();
        }
        else {
          this.createILAForm.get('deliveryMethode')?.setValue('');
          this.createILAForm.updateValueAndValidity();
        }
      }
      else{
        this.isProviderNerc.emit(false);
      }
      this.isNercBit = isNerc;
    }
    else {
      this.ila.providerId = event.source.value;

    }
  }

  readyContinueButtonEnableLogic() {
    this.positionsControl.statusChanges.subscribe((res) => {
      if (this.createILAForm.valid && res === 'VALID') {
        this.formChanges.emit(res);
      } else {
        this.formChanges.emit('INVALID');
      }
    });

    this.createILAForm.statusChanges.subscribe((res) => {
      if (res === 'VALID' && this.positionsControl.valid) {
        this.formChanges.emit(res);
      } else {
        this.formChanges.emit('INVALID');
      }
    });
  }

  // Unlink previously linked positions and link newly selected positions.
  async refreshPositionLink(ilaId: any) {
    var updateLinkOptions : ILAPositionLinkOption = new ILAPositionLinkOption();
    updateLinkOptions.PositionIds = Array.from(new Set(this.positionIds));
    await this.ilaService.updateLinkedPosition(ilaId,updateLinkOptions).then((res)=>{
        this.formChanges.emit('UPDATED');
        this.saveStore.dispatch(saveILA({ saveData: res, tabIndex: 1 }));
      });
  }
  // Unlink previously linked topic and link newly selected topics.
  async refreshTopicLinkAsync(ilaId: any) {
    var updateLinkOptions : ILATopicLinkOption = new ILATopicLinkOption();
    updateLinkOptions.topicIds = Array.from(new Set(this.topicIds));
    await this.ilaService.updateLinkedILATopicsAsync(ilaId,updateLinkOptions).then((res)=>{
        this.formChanges.emit('UPDATED');
        this.saveStore.dispatch(saveILA({ saveData: res, tabIndex: 1 }));
      });
  }

  async creatPositionLink(ilaId: any) {
    var options: ILAPositionLinkOption = new ILAPositionLinkOption();
    options.PositionIds = this.positionIds;
    await this.ilaService
      .linkPosition(ilaId, options)
      .then((res: any) => {

        this.saveEvent = new Observable<any>();
        this.formChanges.emit('SAVED');

        this.saveStore.dispatch(saveILA({ saveData: res, tabIndex: 1 }));
      })
      .catch((err) => {

      });
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
    if(this.currentIlaId){
      await this.readyPositionByILAId(this.currentIlaId);
    }
  }

  async getTopic() {
    await this.ilaTopicService
      .getAll()
      .then((res: any) => {
        this.topic_list = res;
      })
      .catch((err) => {
        console.error(err);
      });
      this.topicList.push(this.topicsControl);
      if(this.currentIlaId){
        await this.readyTopicsByILAIdAsync(this.currentIlaId);
      }
  }

  isProviderLoading:boolean=false;
  async getProviders() {
    this.isProviderLoading=true;
    this.providerService
      .getWihtoutIncludes()
      .then((res: any) => {
        this.provider_list = res;
        this.originalProviderData = Object.assign(this.provider_list);
      })
      .catch((err) => {
        console.error(err);
      }).finally(() => {
        this.isProviderLoading = false;
      });
  }

  readyCreateILAForm() {
    this.createILAForm = this.fb.group({
      ILANumber: new UntypedFormControl(this.ILANumber, [Validators.required]),
      ILAName: new UntypedFormControl(this.ILAName, [Validators.required]),
      ILADescription: new UntypedFormControl(this.ILADescription),
      topicsControl: new UntypedFormControl(),
      nickName: new UntypedFormControl(),
      provider: new UntypedFormControl('', [Validators.required]),
      positionsControl: new UntypedFormControl(),
      deliveryMethode: new UntypedFormControl(this.allDeliveryMethods,[Validators.required]),
      other: new UntypedFormControl(''),
      IsAvailableForAllIla: new UntypedFormControl(),
      IsSelfPacedILA: new UntypedFormControl(),
      searchTxt: new UntypedFormControl(''),
    });
  }

  selectFile(event: any) {
    //Angular 11, for stricter type
    if (!event.target.files[0] || event.target.files[0].length == 0) {
      this.img = 'You must select an image';
      return;
    }

    var mimeType = event.target.files[0].type;

    if (mimeType.match(/image\/*/) == null) {
      this.img = 'Only images are supported';
      return;
    }

    if (!this.allowedTypes.includes(event.target.files[0].type.toLowerCase())) {
      this.img = 'Inavild Image type selected';
      this.alert.errorToast(
        'Please select valid image type (jpg,jpeg,bnp,png,svg)'
      );
      return;
    }

    let file = event.target.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file);
    // Reads and converts image to base64 type string
    reader.onloadend = () => {
      this.imageInBase64 = file.name+";"+reader.result?.toString();
      this.uploadedImage = file;
      this.url =reader.result?.toString();
    };
    // Reads the name of image
    reader.onload = (_event) => {
      this.img = '';
      this.image_name = event.target.value.substring(12);
    };

    reader.onerror = function (error) {

    };
  }

  async openFlyInPanel(templateRef: any, name: string) {
    switch (name) {
      case 'Topic':
        this.change_topic = false;
        this.edit_topic = true;
        break;
      case 'Provider':
        this.provider_edit_mode = true;
        this.provider_change_mode = false;
        break;
      case 'Position':
        break;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async getTrainingPrograms() {
    await this.positionService
      .getTrainingPrograms(this.selectedPositionId)
      .then((res) => { })
      .catch((err) => {
        console.error(err);
      });
  }

  private removeFirst(array: any[], toRemove: any): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
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

  removePosition(Confirmed:any,i: any) {
    if (this.isIlaPublished && this.useForEMPILA) {
      const dialogRef = this.dialog.open(Confirmed,
        { width: '50%' },)
      dialogRef.afterClosed().subscribe((res) => {
        if (res === true) {
          const pos = this.positionsControl.value as Position[];
          this.removeFirst(pos, i);
          this.positionsControl.setValue(pos);
        }
      })
    }else{
      const pos = this.positionsControl.value as Position[];
      this.removeFirst(pos, i);
      this.positionsControl.setValue(pos);
    }
  }
  removeTopic(i: any) {
      const ilaTopics = this.topicsControl.value as ILA_Topic[];
      this.removeFirst(ilaTopics, i);
      this.topicsControl.setValue(ilaTopics);
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  filterData() {
    this.createILAForm.get('searchTxt')?.setValue('');
    this.providerSearch();
  }

  providerSearch() {
  
      var filterString = this.createILAForm.get('searchTxt')?.value;
      if (filterString !== undefined && filterString !== null) {
        filterString = String(filterString).toLowerCase();
      } else {
        filterString = "";
      }
      this.provider_list = this.originalProviderData.filter((f) => {
        return f.name.toLowerCase().includes(filterString);
      });
    }

  validateNickName(isChecked: boolean) {
  const control = this.createILAForm.get('nickName');
  if (isChecked) {
    control.setValidators([Validators.required]);
    control.updateValueAndValidity();
    control.markAsTouched();
    control.markAsDirty();
  }
  else{
    control.clearValidators();
  }
}

 
}
