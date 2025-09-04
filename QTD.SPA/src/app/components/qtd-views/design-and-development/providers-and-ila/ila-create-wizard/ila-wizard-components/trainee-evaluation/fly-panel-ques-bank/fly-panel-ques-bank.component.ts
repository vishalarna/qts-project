import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-ques-bank',
  templateUrl: './fly-panel-ques-bank.component.html',
  styleUrls: ['./fly-panel-ques-bank.component.scss']
})
export class FlyPanelQuesBankComponent implements OnInit, OnDestroy, AfterViewInit {
  @Output() question_array_create = new EventEmitter<any>();
  @Output() random_array = new EventEmitter<any>();


  isDropdownVisible: boolean;
  isSelectDropdownVisible: boolean;
  RandomlyisDropdownVisible: boolean;

  obj_topics: any[] = [];
  obj_length: number;
  question_type: any[] = [];
  taxonomy: any[] = [];

  eo_id: any;
  ans: any[]=[];

  dataTransfered: boolean = false;
  generate_random: boolean = true;
  select_check: boolean = true;

  message: TestItem[] = [];
  default_array: any[] = [];

  tax_num = 0;
  type_num = 0;
  ans_1: Push_Items[] = [];

  subscription = new SubSink();
  ILAId: any;
  generate_number:number=0;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private saveStore: Store<{ saveIla: any }>,
    private ilaService: IlaService,
    private testItemTypeService: TestItemTypeService,
    private taxonomyLevelService: TaxonomyLevelService,
    private testItemService: TestItemService,
    private alert: SweetAlertService,
  ) { }

  ngOnInit(): void {



    //question type
 /*    this.question_type = [
      { id: 1, text: 'Multiple choice', checked: false },
      { id: 4, text: 'Matching', checked: false },
      { id: 2, text: 'True/False', checked: false },
      { id: 3, text: 'Fill In The Blank', checked: false },
      { id: 5, text: 'Short Answer', checked: false }
    ] */

    //taxonomy
   /*  this.taxonomy = [
      { id: 1, text: 'Recall', checked: false },
      { id: 2, text: 'Application', checked: false },
      { id: 3, text: 'Analysis', checked: false },

    ]
 */
    // this.obj_topics = [
    //   { id: 1, description: 'Describe the purpose and process responding to substation alarms problems at substations and problems at substations', type: 'checkbox', checked: false },
    //   { id: 2, description: 'Describe the purpose and process responding to substation alarms problems at substations and problems at substations', type: 'checkbox', checked: false },
    //   { id: 3, description: 'Describe the purpose and process responding to substation alarms problems at substations and problems at substations', type: 'checkbox', checked: false },
    // ];

    /* this.message = [
      { id: 1, EO_id: 1, tax: 'Recall', type: 'Multiple choice', ans: 'What color is a spotlight' },
      { id: 2, EO_id: 1, tax: 'Analysis', type: 'Multiple choice', ans: 'What are colors of a spotlight' },
      { id: 3, EO_id: 1, tax: 'Application', type: 'Multiple choice', ans: 'What are colors of a spotlight' },
      { id: 4, EO_id: 2, tax: 'Recall', type: 'Multiple choice', ans: 'What do you do at a red spotlight' },
      { id: 5, EO_id: 2, tax: 'Application', type: 'Multiple choice', ans: 'What are the colors of a spotlight' },
      { id: 6, EO_id: 2, tax: 'Analysis', type: 'Multiple choice', ans: 'What color is a spotlight' },
      { id: 7, EO_id: 3, tax: 'Recall', type: 'Multiple choice', ans: 'What color is a spotlight' },
      { id: 8, EO_id: 3, tax: 'Application', type: 'Multiple choice', ans: 'What color is a spotlight' },
      { id: 9, EO_id: 3, tax: 'Analysis', type: 'Multiple choice', ans: 'What color is a spotlight' },
    ] */
    this.obj_length = this.obj_topics.length;
   /*  this.default_array = this.message; */


  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.saveStore.pipe(select("saveIla")).subscribe((res: any) => {
      if ((res?.saveData?.result !== undefined)) {
        this.ILAId = res['saveData']?.result?.id;
        this.readyEO();
      }
    })
    this.readyTestItems();
    this.readyQuestionTypes();
    this.readyTaxonomyLevels();

  }

  ngOnDestroy(): void {

  }

  async readyEO() {
    if (this.ILAId !== undefined) {
      await this.ilaService.getLinkedEnablingObjectives(this.ILAId).then((res: any) => {
        this.obj_topics = res;
        
      })
    }
  }

  async readyQuestionTypes() {
    await this.testItemTypeService.getAll().then((res: any) => {
      this.question_type = res;
      
    }).catch((err: any) => {
      
    })
  }

  async readyTaxonomyLevels(){
    await this.taxonomyLevelService.getAll().then((res:any)=>{
      this.taxonomy = res
      
    }).catch((err: any) => {
      
    })
  }
  mainSpinner = false;
  async readyTestItems(){
    this.mainSpinner = true;
    this.message = await this.testItemService.getAll();
    this.mainSpinner = false;
  }

  ShowDropDown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  ShowDropDownSelect() {
    this.isSelectDropdownVisible = !this.isSelectDropdownVisible;
    this.generate_random = !this.generate_random;
  }

  ShowDropDownRandomly() {
    this.RandomlyisDropdownVisible = !this.RandomlyisDropdownVisible;
    this.select_check = !this.select_check;
  }

  checkEnablingObjectives(e: any) {
    
    this.eo_id = e;
  }

  //function for table
  OnSelectionChange(e: any,event:any) {
    
    if (event.checked === true) {
      let index = this.ans.findIndex(o => o.id == e.id);
      
      this.ans_1.push(this.ans[index]);
    }
    else{
      var idx = this.ans_1.findIndex(o => o.id);
      this.ans_1.splice(idx,1);
    }

  }

  checkAll(event:any){
    if(event.checked){
      this.ans_1 = Object.assign(this.ans);
    }
    else{
      this.ans_1 = [];
    }
  }

  //apply settings
  OnClick() {
    //filter array for taxonomy level
    let ques_filter = this.question_type.filter(i => i.checked == true).map((data) => data.id);
    let tax_filter = this.taxonomy.filter(i => i.checked == true).map((data) => data.id);
    let eo_filter = this.obj_topics.filter(i => i.checked == true).map((data) => data.id);
    this.ans =[];
    

    var filteredData = this.message.filter((data)=>{
      return (tax_filter.length > 0 ? tax_filter.includes(data.taxonomyId):true) && (eo_filter.length > 0 ? eo_filter.includes(data.eoId):true) && (ques_filter.length > 0 ? ques_filter.includes(data.testItemTypeId):true);
    })

    filteredData.forEach((item)=>{
      this.ans.push({
        id:item.id,
        ans:item.description,
        type:item.testItemType.description,
        tax:item.taxonomyLevel.description,
      })
    })


    // for(let i of this.message){
    //   for(let j of tax_filter){
    //     for(let k of eo_filter){
    //       for(let l of ques_filter){
    //         if(i.testItemTypeId === l.id && i.eoId === k.id && i.taxonomyId === j.id){
    //           this.ans.push({
    //             id:i.id,
    //             ans:i.description.replace(/<[^>]+>/g, '').replace("&nbsp;+",''),
    //             type:l.description,
    //             tax:j.description
    //           })
    //         }
    //       }
    //     }
    //   }
    // }
    
    this.dataTransfered = true;
  }

  OnSave() {
    
    if(this.ans_1.length == 0){
      this.alert.errorToast('Please Select a Question to import');
    }
    else{
      this.question_array_create.emit(this.ans_1.map((o)=> o.id));
      for (let i of this.question_type) {
        i.checked = false;
      }
      for (let i of this.taxonomy) {
        i.checked = false;
      }
      
      
      this.flyPanelSrvc.close();
    }
  }

  //for random generation
  ImportTest() {
    let ques_filter = this.question_type.filter(i => i.checked == true).map((o) => o.id);
    let tax_filter = this.taxonomy.filter(i => i.checked == true).map((o) => o.id);
    let eo_filter = this.obj_topics.filter(i => i.checked == true).map((o) => o.id);

    var filteredData = this.message.filter((data)=>{
      return tax_filter.includes(data.taxonomyId) || eo_filter.includes(data.eoId) || ques_filter.includes(data.testItemTypeId);
    })

    filteredData.forEach((item)=>{
      this.ans.push({
        id:item.id,
        ans:item.description,
        type:item.testItemType.description,
        tax:item.taxonomyLevel.description,
      })
    })

    // for(let i of this.message){
    //   for(let j of tax_filter){
    //     for(let k of eo_filter){
    //       for(let l of ques_filter){
    //         if(i.testItemTypeId === l.id && i.eoId === k.id && i.taxonomyId === j.id){
    //           this.ans.push({
    //             id:i.id,
    //             ans:i.description.replace(/<[^>]+>/g, ''),
    //             type:l.description,
    //             tax:j.description
    //           })
    //         }
    //       }
    //     }
    //   }
    // }
    

     if(this.generate_number <= this.ans.length){
      let i=this.generate_number;
      let ranNums:any = [];
      while (i--) {
        let j = Math.floor(Math.random() * (i+1));
        ranNums.push(this.ans[j]);
        this.ans.splice(j,1);
      }
          
          this.random_array.emit(ranNums.map((o)=> o.id));
          this.flyPanelSrvc.close();
    }
    else{
      this.alert.errorToast('Random Number exceeds the number of records');
    }
  }

  IncreaseNumber() {
    this.generate_number++
  }

  DecreaseNumber() {
    if(this.generate_number > 0){
      this.generate_number--;
    }
    else{
      this.generate_number=0;
      this.alert.errorToast('Random Number can not be below 0');
    }

  }

}

export class Push_Items {
  id: any;
  EO_id: any;
  tax: any;
  type: any;
  ans: any;
  checked?: boolean;
}
