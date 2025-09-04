import { Component, OnInit } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-procedures',
  templateUrl: './procedures.component.html',
  styleUrls: ['./procedures.component.scss']
})
export class ProceduresComponent implements OnInit {

  pro_category_topics:Procedures_Category_Topics[]=[];
  pro_subtopics:Procedures_Topics[]=[];
  pro_subcategory:Procedures_SubCategory[]=[];
  pro_category:Procedures_Categories[]=[];

  countCheckBoxes=0;
  countSubCheckBoxes=0;

  isProceduresVisible:any;
  CategorySortCheck=false;
  SubCategorySortCheck=false;


  constructor(public flyPanelSrvc: FlyInPanelService) { }

  ngOnInit() {
    this.pro_category = [
      {id:1,description:'Transmission Operation',number:1},
      {id:2,description:'System Operation',number:2},
      {id:3,description:'Operation',number:3}
    ];
  
    this.pro_subcategory = [
      { id:1,description:'System Operation Trainee',category_id:2,number:1},
      { id:2,description:'System Trainee',category_id:1,number:1},
      { id:3,description:'Operation Trainee',category_id:3,number:1}
  
    ];
  
    this.pro_subtopics = [
      {id:1,description:'Describe the purpose and process responding to substation alarms problems at substations and problems at substations',subcategory_id:1,number:1},
      {id:2,description:'Process responding to substation alarms problems at substations and problems at substations',subcategory_id:1,number:2},
      {id:3,description:'Purpose and process responding to substation alarms problems at substations and problems at substations',subcategory_id:2,number:1},
      {id:4,description:'Process responding to substation alarms problems at substations and problems at substations',subcategory_id:2,number:2},
      {id:5,description:'Process responding to substation alarms problems at substations and problems at substations',subcategory_id:3,number:1},
  
    ];
  
    this.pro_category_topics = [
      {id:1,category_id:1,description:'Describe the purpose and the process responding to and problems at the substation and problems at substations for multiple generators',number:1},
      {id:2,category_id:1,description:'Describe the purpose responding substation alarms and problems substations and problems at substations',number:2},
      {id:3,category_id:1,description:'Describe the purpose and process responding to substation alarms problems at substations and problems at substations',number:3},
      {id:4,category_id:2,description:'Describe the purpose and process responding to substation alarms problems at substations and problems at substations',number:1},
      {id:5,category_id:2,description:'Describe the purpose and process responding to substation alarms problems at substations and problems at substations',number:2},
      {id:6,category_id:3,description:'Describe the purpose responding substation alarms and problems substations and problems at substations',number:1},
  
    ];

    this.getIDCount();
    this.getSubCategoryIDCount();
  }

   //counting the number of rows for each category
   getIDCount(){
    let i = 0;
    while (i < this.pro_category.length) {
      let count1 = this.pro_category_topics.filter((item) => item.category_id === this.pro_category[i].id).length; 
      this.pro_category[i].count = count1;
      i++;
      
    }
  }

  //counting the number of rows for each sub category
  getSubCategoryIDCount(){
    let i = 0;
    while (i < this.pro_subcategory.length) {
      let count1 = this.pro_subtopics.filter((item) => item.subcategory_id === this.pro_subcategory[i].id).length; 
      this.pro_subcategory[i].count = count1;
      i++;
      
    }
  }

  //count total number of checked checkboxes for categories
  countCheckBox(e:any){
    this.countCheckBoxes =  this.pro_category_topics.filter(i=>i.checked == true).length;
     
 
     if(this.countCheckBoxes >=1){
       this.CategorySortCheck=true;
     }
     else{
       this.CategorySortCheck=false;
     }
   }
 
  //count total number of checked checkboxes for categories
   countSubCategoryCheckBox(e:any){
     this.countSubCheckBoxes =  this.pro_subtopics.filter(i=>i.checked == true).length;
     
 
     if(this.countSubCheckBoxes >=1){
       this. SubCategorySortCheck=true;
     }
     else{
       this. SubCategorySortCheck=false;
     }
   }

}

//classes
export class Procedures_Categories{
  id:any;
  description:string;
  number:number;
  count?:number;
}

export class Procedures_SubCategory{
  id:any;
  description:any;
  category_id:any;
  number:number;
  count?:number;
}

export class Procedures_Topics{
  id:any;
  description:string;
  subcategory_id:any;
  number:number;
  checked?:boolean;
}

export class Procedures_Category_Topics{
  id:any;
  category_id:any;
  description:string;
  number:number;
  checked?:boolean;
}






