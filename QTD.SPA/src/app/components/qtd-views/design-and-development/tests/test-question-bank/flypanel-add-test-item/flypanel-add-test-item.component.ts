import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, OnInit, Output, ViewChild, ViewContainerRef, AfterViewInit, OnDestroy, Input } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TaxonomyLevel } from 'src/app/_DtoModels/TaxonomyLevel/TaxonomyLevel';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TrueFalseComponent } from '../../../providers-and-ila/ila-create-wizard/ila-wizard-components/trainee-evaluation/true-false/true-false.component';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SubSink } from 'subsink';
import { AddTrueFalseComponent } from '../add-true-false/add-true-false.component';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';

@Component({
  selector: 'app-flypanel-add-test-item',
  templateUrl: './flypanel-add-test-item.component.html',
  styleUrls: ['./flypanel-add-test-item.component.scss']
})
export class FlypanelAddTestItemComponent implements OnInit, AfterViewInit, OnDestroy {
  @Output() closed = new EventEmitter<any>();
  @Output() addQuestion = new EventEmitter<any>();
  @Output() closeByValue = new EventEmitter<boolean>();
  @Input() list:any;
  @Input() previousQuestion!: any;
  @Input() ilaId!: any;
  @Input() mode: 'add' | 'edit' | 'copy' = 'add';
  @Input() mode1:any;
  @Input() listRow:any;
  @Input() isCloseClick:boolean = false;

  showEO = false;
  selectedType: TestItemType;
  testItemForm = new UntypedFormGroup({});
  eoId:any = null;
  eoDescription = "";
  addEO = false;
  testItemTypes: TestItemType[] = [];
  taxonomyLevels: TaxonomyLevel[] = [];
  eoData!: EnablingObjective;

  subscription = new SubSink();
  testItemData:any;

  // @ViewChild('tf') trueFalse !:AddTrueFalseComponent;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private testItemTypeService: TestItemTypeService,
    private taxonomyLevelService: TaxonomyLevelService,
    private dataBroadcastService: DataBroadcastService,
    private eoService: EnablingObjectivesService,
    private testItemService : TestItemService
  ) { }

  previewCheck:boolean = false;
  ngOnInit(): void {
    this.readyTestItemForm();
    this.readyTypeData();
    this.readyTaxonomyData();
    if(this.mode1){
      this.mode = 'edit';
      this.previewCheck = true;
    } 
    if (this.mode === 'edit' || this.mode === 'copy') {
      this.readyEOData();
    }
  }

  ngAfterViewInit(): void {
    // this.subscription.sink = this.dataBroadcastService.saveQuestion.subscribe((_)=>{
    //   switch(this.selectedType){
    //     case 'true / false':
    //       this.trueFalse.testFunction();
    //       break;
    //   }
    // })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  
  async getTestItemListData (){
   await this.testItemService.get(this.listRow.id).then((res)=>{
    this.testItemData = res;
   });

  } 

  async readyEOData() {
    if (this.previousQuestion.eoId) {
      this.eoData = await this.eoService.get(this.previousQuestion.eoId);
      this.eoDescription = `${this.eoData.number} - ${this.eoData.description}`;
      this.eoId = this.eoData.id;
    }
  }

  readyTestItemForm() {
    this.testItemForm.addControl('type', new UntypedFormControl({value:'',disabled:this.mode !== 'add'}, Validators.required));
    this.testItemForm.addControl('taxonomy', new UntypedFormControl('', Validators.required));
  }

  async readyTypeData() {
    this.testItemTypes = await this.testItemTypeService.getAll();
    if (this.mode !== "add" ) {
      var typeData = this.testItemTypes.find((data) => {
        return data.id == this.previousQuestion.typeId;
      })
      this.testItemForm.patchValue({
        type: typeData,
      });
      this.selectedType = typeData ?? new TestItemType();
    }
  }

  async readyTaxonomyData() {
    this.taxonomyLevels = await this.taxonomyLevelService.getAll();
    if (this.mode !== "add" ) {
      this.testItemForm.patchValue({
        taxonomy: this.previousQuestion.taxonomyId,
      })
    }
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  testQuestionSaved(id:any){
    this.addQuestion.emit(id);
  }
}
