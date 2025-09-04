import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { TestOptions } from 'src/app/_DtoModels/Test/TestOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-change-ila',
  templateUrl: './flypanel-change-ila.component.html',
  styleUrls: ['./flypanel-change-ila.component.scss']
})
export class FlypanelChangeIlaComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() filterData = new EventEmitter<any>();
  @Output() ilaChanged = new EventEmitter<any>();
  @Output() ilaIdSelected = new EventEmitter<any>();
  @Output() outputProvider = new EventEmitter<any>();

  @Input() saveHere = false;
  @Input() testId = '';
  @Input() currentIlaId = "";
  providers: any[] = [];
  providerId:any;
  selectedProvider:any;
  @Input() filterProvider:any;
  @Input() fromBulkEdit:any; 
  tempILAList:any; 
  providerFilterTrue:any;

  datePipe = new DatePipe('en-us');

  ilaList: any[] = [];

  selectedILA!: any | undefined;
  reason = "";
  providerIds:any;
  selectedILAArray:any []=[];
  array: any = [];
  isLoading:boolean=false;
  isLoadingILA:boolean=false;

  constructor(
    private ilaService: IlaService,
    private testService : TestsService,
    private alert : SweetAlertService,
    private providerSrvc: ProviderService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
      this.readyILAData();
      this.getProviders();
  }

  async readyILAData() {
    this.isLoadingILA=true;
    this.ilaList = await this.ilaService.getAll().finally(() => this.isLoadingILA = false);
    this.tempILAList = this.ilaList;
    this.selectedILA = this.ilaList.find((x)=>{
      return x.id == this.currentIlaId;
    })
  }

  async getProviders() {
    this.isLoading= true;
    this.providers = await this.providerSrvc.getWihtoutIncludes().finally(() => {
      this.isLoading = false;
    });
    this.providers.forEach((provider) =>{
      provider.ilAs.forEach((ila)=>{
        if(ila.id == this.currentIlaId){
          this.providerId = ila.providerId;
        }
      })
    })
  }

  onSelectionChange(event:any){
    this.selectedILAArray =[];
    this.providers.forEach((provider)=>{
      if(provider.id === event.value){
        
        provider.ilAs.forEach((ilAs)=>{
          this.selectedILAArray.push(ilAs);
        });
      }
    })
  }

  selectILA(event:any){
    this.array.push(event);
 
  }



  async changeILA(){
    var options = new TestOptions();
    options.ilaId = this.selectedILA?.id;
    options.actionType = 'change';
    options.testIds.push(this.testId);
    options.effectiveDate = this.datePipe.transform(Date.now(), "yyyy-MM-dd");
    options.changeNotes = this.reason;
    await this.testService.delete(options).then(async (_)=>{
      this.alert.successToast(await this.labelPipe.transform('ILA') + " Changed for Selected Test");
      this.ilaChanged.emit();
    })
  }

  closeFunction(){
    this.array = [...new Set(this.array)];
    this.filterData.emit(this.array);
    this.outputProvider.emit(this.providerFilterTrue);
    this.closed.emit('fp-add-eo')
  }



}
