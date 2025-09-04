import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewContainerRef, ViewEncapsulation } from '@angular/core';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-ila',
  templateUrl: './ila.component.html',
  styleUrls: ['./ila.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ILAComponent implements OnInit {

  //default tab name
  activeTab: string = 'prerequisites';

  //Prerequisites
  prerequisites_list:Prerequisites_List[] = [];
  prerequisites_list_length: number;
  isPrerequisiteschecked = false;

   //ILA Details
   isILADropdownVisible: any;
   isCEHDropdownVisible: any;
   isAdditionalCEHHoursDropdownVisible: any;
   isTrainingDropdownVisible: any;
   isAdditionalCEHDropdownVisible: any;
   isSave = false;
   isEdit = true;
   ILATitle='Blackout 2003 : A Lesson In Emergency Preparedness';
   ILANumber='LK_001_360_9200';
   ILAType='Self-Paced-Training';
   checkbox1=true;
   checkbox2:boolean;
   isCEHSave:boolean;
   isCEHEdit=true;
   standard_related_hours_details=2;
   ceh_hours=0;
   other=0;
   operating_topic_hours_details=2;
   emerg=2;
   total_training_hours=2;
   simulation_hours_details=2;
   regional_hours=2;
   professional_hours=2;
   regional_2_hours=2;

  //Objectives
  objectivesList : Objectives[] = [];
  objectivesLength: number;
  isObjectivesChecked = false;

  //Procedures
  procedures_list: Procedures[]=[];
  procedure_list_length:number;
  isProcedureCheck=false;

  //safety hazards
  safety_hazard_list:Safety_Hazard[]=[];
  safety_hazard_list_length:number;
  isSafetyHazardCheck=false;

  //training plan
  isIndividualDropdownVisible:boolean;
  isTargetDropdownVisible:boolean;
  isTrainingPlanDropdownVisible:boolean;
  isEvaluationMethodDropdownVisible:boolean;

   //ILA Application
   isCourseDetailsDropdownVisible:boolean;
   isAppDetailsDropdownVisible:boolean;
   isOperatorDropdownVisible:boolean;
   isCourseSave=false;
   isCourseEdit=true;
   isAppEdit=true;
   isAppSave=false;
   check1=true;
   check2=false;
   check3=false;
   nerc_id='PWERCO_123456';
   title:string;
   number:string;
   standard_related_hours='4';
   operating_topic_hours='4';
   simulation_hours='0';
   start_date='12/03/2021';
   applicable='Yes';
   assessment:any[];
   ila_checkbox:any[];
   default_value=true;


  //resources
  isPlayButtonClicked=false;
  activeTabResource = 'welcome';

  constructor( public flyPanelService: FlyInPanelService,private vcf: ViewContainerRef,private labelPipe: LabelReplacementPipe,) { }

  async ngOnInit(): Promise<void> {
    this.toggleTab('prerequisites');
   /*  this.toggleTab('objectives');
    this.toggleTab('procedures');
    this.toggleTab('safety_hazards');
      ``   */
      this.title='GMD ' + await this.transformTitle('Procedure') + 's';
      this.number='2015 GMD ' + await this.transformTitle('Procedure') + 's';
  }

  selectedTabValue(event : any)
  {
    
    var label  = event.tab.textLabel;
    this.toggleTab(label.toLowerCase());
  }

  //toggle tabs
  async toggleTab(tabName:string){
    
    if (this.activeTab !== tabName){
      this.activeTab = tabName;
    }

    switch(tabName){
      case 'prerequisites':
        this.prerequisites_list = [
          {id:'1.1.1.3',description:'Identify acceptable and unacceptable ACE deviations',type:'checkbox'},
          {id:'1.1.1.4',description:'Describe automated systems used for controlling substation equipment',type:'checkbox'},
          {id:'1.1.1.5',description:'List typical substation alarms and the required response for each',type:'checkbox'},
          {id:'1.1.1.6',description:'Describe relay alarms and the required response for each',type:'checkbox'},
          {id:'1.1.1.7',description:'State and explain the requirements for any protective system supporting system reliability',type:'checkbox'},
          {id:'1.1.1.8',description:'Describe the basic components of a protective system and explain how they work together in response....',type:'checkbox'}
        ]
        this.prerequisites_list_length = this.prerequisites_list.length;

        this.isObjectivesChecked=false;
        this.isProcedureCheck=false;
        this.isSafetyHazardCheck=false;
        break;

      case 'objectives':
        this.objectivesList = [
          {id:'1.1.1.3', type:'EO', description:'Identify acceptable and unacceptable ACE deviations', isCheckbox:'checkbox'},
          {id:'1.1.1.4', type:'EO', description:'Describe automated systems used for controlling substation equipment', isCheckbox:'checkbox'},
          {id:'1.1.1.5', type:'EO', description:'List typical substation alarms and the required response for each', isCheckbox:'checkbox'},
          {id:'1.1.1.6', type:'Task', description:'Describe relay alarms and the required response for each', isCheckbox:'checkbox'},
          {id:'1.1.1.7', type:'Task', description:'State and explain the requirements for any protective system supporting system reliability', isCheckbox:'checkbox'},
          {id:'1.1.1.8', type:'Task', description:'Describe the basic components of a protective system and explain how they work together in response....', isCheckbox:'checkbox'}
        ]
        this.objectivesLength = this.objectivesList.length;

        this.isPrerequisiteschecked=false;
        this.isProcedureCheck=false;
        this.isSafetyHazardCheck=false;
        break;

        case 'procedures':
          this.procedures_list = [
            {number:'DOE-HBK',procedure_title:'Identify acceptable and unacceptable ACE deviations',type:'checkbox'},
            {number:'10',procedure_title:'Describe automated systems used for controlling substation equipment',type:'checkbox'},
            {number:'1',procedure_title:await this.transformTitle('Procedure') + ' 1',type:'checkbox'},
          ];
          this.procedure_list_length = this.procedures_list.length;

          this.isPrerequisiteschecked=false;
          this.isObjectivesChecked=false;
          this.isSafetyHazardCheck=false;
          break;

          case 'safety hazards':
            this.safety_hazard_list = [
              {number:'DOE-HBK',procedure_title:'Identify acceptable and unacceptable ACE deviations',type:'checkbox'},
              {number:'10',procedure_title:'Describe automated systems used for controlling substation equipment',type:'checkbox'},
              {number:'1',procedure_title:await this.transformTitle('Procedure') + ' 1',type:'checkbox'},
            ];
            this.safety_hazard_list_length = this.safety_hazard_list.length;

            this.isPrerequisiteschecked=false;
            this.isObjectivesChecked=false;
            this.isProcedureCheck=false;
            break;

            case 'ila application':
              this.ila_checkbox = [
                {id:1,label:'Written/Online Exam',isChecked:true},
                {id:2,label:'Performance Demonstration',isChecked:false},
                {id:3,label:'Other Type Assessment ' + await this.labelPipe.transform('Tool'),isChecked:false},
              ]
              break;

            default:
              this.isPrerequisiteschecked=false;
              this.isObjectivesChecked=false;
              this.isProcedureCheck=false;
              this.isSafetyHazardCheck=false;

    }
  }
   //prerequisites
   isAllPrerequisitesCheckBoxChecked(){
    return this.prerequisites_list.every(p => p.checked);
  }

  checkAllPrerequisitesCheckBox(e:any){
    this.prerequisites_list.forEach(x => x.checked = e.target.checked);
    this.prerequisitesChange(e);
  }

  prerequisitesChange(e:any){
    if(this.prerequisites_list.length == 0)
    {
      this.isPrerequisiteschecked = false;
    }
    else if(e.target.checked == true){
      this.isPrerequisiteschecked = true;
    }
    else{
      let check = this.prerequisites_list.find(p=>p.checked == true)
      if(check?.checked != true){
        this.isPrerequisiteschecked = false;
      }
    }

  }

  //opening and closing ILA Details Tab dropdown starts
  ShowILADetailsDropDown(){
    this.isILADropdownVisible = !this.isILADropdownVisible;
  }

  ShowCEHDropDown(){
    this.isCEHDropdownVisible = !this.isCEHDropdownVisible;
  }

  ShowAdditionalCEHHoursDropDown(){
    this.isAdditionalCEHHoursDropdownVisible = !this.isAdditionalCEHHoursDropdownVisible;
  }

  ShowTrainingDropDown(){
    this.isTrainingDropdownVisible = !this.isTrainingDropdownVisible;
  }

  ShowAdditionalCEHDropDown(){
    this.isAdditionalCEHDropdownVisible = !this.isAdditionalCEHDropdownVisible;
  }

  ShowSaveDropDown(){
    this.isSave = true;
    this.isEdit = false;
  }

  ShowEditDropDown(){
    this.isEdit = true;
    this.isSave = false;
  }

  ShowCEHSaveDropDown(){
    this.isCEHSave=true;
    this.isCEHEdit=false;
  }

  ShowCEHEditDropDown(){
    this.isCEHSave=false;
    this.isCEHEdit=true;
  }
  //opening and closing ILA Details Tab dropdown ends

  isAllObjectivesCheckBoxChecked() {
		return this.objectivesList.every(p => p.checked);
	}

  checkAllObjectivesCheckBox(event: any) { // Angular 13
    this.objectivesList.forEach(x => x.checked = event.target.checked);
    this.objectiveChange(event);
	}

  objectiveChange(event: any)
  {
    
    if(this.objectivesList.length == 0)
    {
      this.isObjectivesChecked = false;
    }
    else if(event.target.checked == true){
      this.isObjectivesChecked = true;
    }
    else{
      let check = this.objectivesList.find(p=>p.checked == true)
      if(check?.checked != true){
        this.isObjectivesChecked = false;

      }
    }
  }

  removeObjective(event: any)
  {
    
    for(let i = 0; i < this.objectivesList.length; i++)
    {
      if(this.objectivesList[i].checked == true)
      {
        this.objectivesList.splice(i, 1);
        i = i - 1;
      }

    }
    this.checkAllObjectivesCheckBox(event);
    this.isAllObjectivesCheckBoxChecked();
  }

  removePrerequisites(event : any)
  {
    
    for(let i = 0; i < this.prerequisites_list.length; i++)
    {
      if(this.prerequisites_list[i].checked == true)
      {
        this.prerequisites_list.splice(i, 1);
        i = i - 1;
      }

    }
    this.checkAllPrerequisitesCheckBox(event);
    this.isAllObjectivesCheckBoxChecked();
  }

  removeProcedure(event : any)
  {
    
    for(let i = 0; i < this.procedures_list.length; i++)
    {
      if(this.procedures_list[i].checked == true)
      {
        this.procedures_list.splice(i, 1);
        i = i - 1;
      }

    }
    this.checkAllProcedureCheckBox(event);
    this.isAllProcedureCheckBoxChecked();
  }

   //procedure
   isAllProcedureCheckBoxChecked(){
    return this.procedures_list.every(i=>i.checked);
  }

  checkAllProcedureCheckBox(e:any) {
    this.procedures_list.forEach(x => x.checked = e.target.checked);
    this.proceduresChange(e);
  }

  proceduresChange(e:any){
    if(this.procedures_list.length == 0)
    {
      this.isProcedureCheck = false;
    }
    else if(e.target.checked == true){
      this.isProcedureCheck = true;
    }
    else{
      let check = this.procedures_list.find(p=>p.checked == true)
      if(check?.checked != true){
        this.isProcedureCheck = false;
      }
    }
  }

  removeSafetyHazard(event : any)
  {
    
    for(let i = 0; i < this.safety_hazard_list.length; i++)
    {
      if(this.safety_hazard_list[i].checked == true)
      {
        this.safety_hazard_list.splice(i, 1);
        i = i - 1;
      }

    }
    this.checkAllSafetyHazardCheckBox(event);
    this.isAllSafetyHazardCheckBoxChecked();
  }

  //safety hazards
  isAllSafetyHazardCheckBoxChecked(){
    return this.safety_hazard_list.every(i=>i.checked)
  }

  checkAllSafetyHazardCheckBox(e:any){
    
    this.safety_hazard_list.forEach(x => x.checked = e.target.checked);
    this.safetyHazardChange(e);
  }

  safetyHazardChange(e:any){
    
    if(this.safety_hazard_list.length == 0)
    {
      this.isSafetyHazardCheck = false;
    }
    else if(e.target.checked == true){
      this.isSafetyHazardCheck = true;
    }
    else{
      let check = this.safety_hazard_list.find(p=>p.checked == true)
      if(check?.checked != true){
        this.isSafetyHazardCheck = false;
      }
    }
  }


  //training plan
  ShowIndividualDropDown(){
    this.isIndividualDropdownVisible = !this.isIndividualDropdownVisible;
  }

  ShowTargetDropDown(){
    this.isTargetDropdownVisible = !this.isTargetDropdownVisible;
  }

  ShowTrainingPlanDropDown(){
    this.isTrainingPlanDropdownVisible=!this.isTrainingPlanDropdownVisible;
  }

  ShowEvaluationMethodDropDown(){
    this.isEvaluationMethodDropdownVisible = !this.isEvaluationMethodDropdownVisible;
  }

  //ILA Application
  ShowCourseDetailsDropDown(){
    this.isCourseDetailsDropdownVisible=!this.isCourseDetailsDropdownVisible;
  }

  ShowAppDetailsDropDown(){
    this.isAppDetailsDropdownVisible = !this.isAppDetailsDropdownVisible;
  }

  ShowOperatorDropDown(){
    this.isOperatorDropdownVisible=!this.isOperatorDropdownVisible;
  }

  ShowCourseSaveDropDown(){
    this.isCourseSave = true;
    this.isCourseEdit = false;
  }

  ShowCourseEditDropDown(){
    this.isCourseEdit = true;
    this.isCourseSave = false;
  }

  ShowAppSaveDropDown(){
    this.isAppSave=true;
    this.isAppEdit=false;
  }

  ShowAppEditDropDown(){
    this.isAppSave=false;
    this.isAppEdit=true;
  }

  OnlyOneCheckbox(e:any){
    if(e.target.id == 'check1id'){
      this.check1=true;
      this.check2=false;
      this.check3=false;
      this.applicable = 'Yes';
    }
    else if(e.target.id == 'check2id'){
      this.check1=false;
      this.check2=true;
      this.check3=false;
      this.applicable = 'No';
    }
    else if(e.target.id == 'check3id'){
      this.check1=false;
      this.check2=false;
      this.check3=true;
      this.applicable = 'N/A';
    }

  }


  openFlyInPanel(templateRef: any)
  {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  OpenResourcePanel(){
    this.isPlayButtonClicked=true;
  }
  CloseResourcePanel(){
    this.isPlayButtonClicked=false;
  }
  // opening of flyin panel ends

  //toggle tab for resource panel
  toggleTabResource(reference : string){
    if (this.activeTabResource !== reference){
      this.activeTabResource = reference;
    }
  }

  ChangeOption(e:any){
    
    if(e.target.id == 'self'){
      this.ILAType='Self-Paced-Training';
      this.checkbox2=false;
    }
    else if(e.target.id == 'optional'){
      this.ILAType='Optional';
      this.checkbox1=false;
    }
  }

  //getting the array for assessment tools
  getAssessmentToolArray(){
    this.assessment = this.ila_checkbox.filter(i=>i.isChecked);
    
     this.assessment = this.assessment.map(i=>{
      return i.label;
    })

    
  }

  onSelectionChange(){
    this.getAssessmentToolArray();
    this.default_value=false;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

}

export class Prerequisites_List{
  id:any;
  description:string;
  type:string;
  checked?:boolean;
}

export class Objectives {
  id: any;
  type: string;
  description: string;
  isCheckbox: string;
  checked?: boolean;
}

export class Procedures{
  number:string;
  procedure_title:string;
  type:string;
  checked?:boolean
}

export class Safety_Hazard{
  number:string;
  procedure_title:string;
  type:string;
  checked?:boolean;
}


