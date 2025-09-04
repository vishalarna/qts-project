import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';
import { GetTestItemsByILAsOption } from 'src/app/_DtoModels/MetaILA_SummaryTest/GetTestItemsByILAsOption';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { MetaILASummaryTestService } from 'src/app/_Services/QTD/meta-ila-summary-test.service';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-import-meta-ila-test-questions',
  templateUrl: './import-meta-ila-test-questions.component.html',
  styleUrls: ['./import-meta-ila-test-questions.component.scss']
})
export class ImportMetaILATestQuestionsComponent implements OnInit, OnDestroy, AfterViewInit {
  @Output() closeByValue = new EventEmitter<boolean>();
  @Output() questionsToAdd = new EventEmitter<any>();
  @Output() random_array = new EventEmitter<any>();
  @Input() metaILAId:string;
  @Input() metaILAs: MetaILAVM;
  selectionMode:string='';
  isOptionsVisible:boolean=false;
  question_type: any[] = [];
  taxonomy: any[] = [];
  selectedQuestionTypeIds:number[]=[];
  selectedLevelIds:number[]=[];
  selectedLevelEoIds:any[]=[];
  testItems:TestItem[]=[];
  randomCount:number=0;
  isDropdownVisible: boolean;
  eosLinkedToMetaILA: any[] = [];
  filteredDataToPass:any[]=[];
  testQuesItems:any[]=[];
  isTestItemTableVisible:boolean=false;
  constructor(
    private testItemTypeService: TestItemTypeService,
    private taxonomyLevelService: TaxonomyLevelService,
    public metaILASummaryTestService: MetaILASummaryTestService,
    private alert: SweetAlertService,
    public metaILAService: MetaILAService,
  ) { }

  ngOnInit(): void {
  }

  showOptions(mode:string){
    this.selectionMode=mode;
    this.isOptionsVisible=true;
  }

  ShowDropDown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }
  
  async ngAfterViewInit(): Promise<void> {
    await this.getLinkedEnablingObjectivesAsync();
    await this.readyTestItems();
    await this.readyQuestionTypes();
    await this.readyTaxonomyLevels();
  }

  ngOnDestroy(): void {
  }

  async readyQuestionTypes() {
    await this.testItemTypeService.getAll().then((res: any) => {
      this.question_type = res;
    }).catch((err: any) => {
    })
  }

  async readyTaxonomyLevels(){
    await this.taxonomyLevelService.getAll().then((res:any)=>{
      this.taxonomy = res;
    }).catch((err: any) => {
    })
  }

  async getLinkedEnablingObjectivesAsync() {
    if (this.metaILAId !== undefined) {
      await this.metaILAService.getEnablingObjectivesForMetaILA(this.metaILAId).then((res: any) => {
        this.eosLinkedToMetaILA = res;
      })
    }
  }

  async readyTestItems(){
    const ilaIDs = this.metaILAs.metaILA_ILAMemberVM.map(x=>x.ilaId.toString());
    let option = new GetTestItemsByILAsOption(ilaIDs);
    this.testItems=await this.metaILASummaryTestService.getTestItemsFromILAs(option);
  }
  
  OnQuestionTypeSelectionhange(questionTypeId: number) {
    const index = this.selectedQuestionTypeIds?.indexOf(questionTypeId);
    if (index !== -1) {
        this.selectedQuestionTypeIds.splice(index, 1);
    } else if (!this.selectedQuestionTypeIds.includes(questionTypeId)) {
        this.selectedQuestionTypeIds.push(questionTypeId);
    }
  }

  getQuestionTypeCheckedValues(id: number) {
    const checkValue = this.selectedQuestionTypeIds && this.selectedQuestionTypeIds.filter(dd => dd === id);
    if ((checkValue && checkValue.length > 0)) {
        return true;
    } else {
        return false;
    }
  }

  OnLevelSelectionhange(levelId: number) {
    const index = this.selectedLevelIds?.indexOf(levelId);
    if (index !== -1) {
        this.selectedLevelIds.splice(index, 1);
    } else if (!this.selectedLevelIds.includes(levelId)) {
        this.selectedLevelIds.push(levelId);
    }
  }
  getLevelCheckedValues(id: number) {
    const checkValue = this.selectedLevelIds && this.selectedLevelIds.filter(dd => dd === id);
    if ((checkValue && checkValue.length > 0)) {
        return true;
    } else {
        return false;
    }
  }

  OnEoSelectionchange(levelId: number) {
    const index = this.selectedLevelEoIds?.indexOf(levelId);
    if (index !== -1) {
        this.selectedLevelEoIds.splice(index, 1);
    } else if (!this.selectedLevelEoIds.includes(levelId)) {
        this.selectedLevelEoIds.push(levelId);
    }
  }

  getEoLevelCheckedValues(id: number) {
    const checkValue = this.selectedLevelEoIds && this.selectedLevelEoIds.filter(dd => dd === id);
    if ((checkValue && checkValue.length > 0)) {
        return true;
    } else {
        return false;
    }
  }

  //apply settings
  applySettings() {
    this.filteredDataToPass = [];
    var filteredData =[]
    filteredData = this.testItems.filter((data)=>{
      return (this.selectedLevelIds.length > 0 ? this.selectedLevelIds.includes(data.taxonomyId):true) && (this.selectedQuestionTypeIds.length > 0 ? this.selectedQuestionTypeIds.includes(data.testItemTypeId):true) && (this.selectedLevelEoIds.length > 0 ? this.selectedLevelEoIds.includes(data.eoId):true);
    })
    if(this.selectionMode == 'random'){
      if(this.randomCount <= filteredData.length){
        let i=this.randomCount;
        let ranNums:any = [];
        while (i--) {
          let j = Math.floor(Math.random() * (i+1));
          ranNums.push(filteredData[j]);
          filteredData.splice(j,1);
        }
        filteredData=ranNums;
      }
      else{
        this.alert.errorToast('Random Number exceeds the number of records');
        return;
      }
    }
    filteredData.forEach((item)=>{
      this.filteredDataToPass.push({
        id:item.id,
        ques:item.description,
        type:item.testItemType.description,
        tax:item.taxonomyLevel.description,
      })
    })
    this.isTestItemTableVisible = true;
    
  }

  IncreaseNumber() {
    this.randomCount++
  }

  DecreaseNumber() {
    if(this.randomCount > 0){
      this.randomCount--;
    }
    else{
      this.randomCount=0;
      this.alert.errorToast('Random Number can not be below 0');
    }
  }

  OnSelectionChange(e: any,event:any) {
    if (event.checked === true) {
      let index = this.filteredDataToPass.findIndex(o => o.id == e.id);
      
      this.testQuesItems.push(this.filteredDataToPass[index]);
    }
    else{
      var idx = this.testQuesItems.findIndex(o => o.id);
      this.testQuesItems.splice(idx,1);
    }
  }

  checkAll(event:any){
    if(event.checked){
      this.testQuesItems = Object.assign(this.filteredDataToPass);
    }
    else{
      this.testQuesItems = [];
    }
  }

  OnSave(){
    let questionIDs= this.testQuesItems.map(x=>x.id.toString());
    this.questionsToAdd.emit(questionIDs);
    this.closeByValue.emit(false);
    this.alert.successToast('Test Items imported successfully');
  }

}


