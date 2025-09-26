import { ILA_StudentEvaluation_LinkOption, ILA_StudentEvaluation_LinkOptions } from './../../../../../../../_DtoModels/ILA_StudentEvaluation_Link/ILA_StudentEvaluation_LinkOptions';
import { StudentEvaluationAudiencesService } from './../../../../../../../_Services/QTD/student-evaluation-audiences.service';
import { StudentEvaluationAvaliabilitiesService } from './../../../../../../../_Services/QTD/student-evaluation-avaliabilities.service';
import { StudentEvaluationFormService } from './../../../../../../../_Services/QTD/student-evaluation-form.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnInit,Input, ViewContainerRef, Output, EventEmitter } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { select, Store } from '@ngrx/store';
import { IlaStudentEvaluationLinkService } from 'src/app/_Services/QTD/ila-student-evaluation-link.service';
import { SubSink } from 'subsink';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-ila-evaluation',
  templateUrl: './ila-evaluation.component.html',
  styleUrls: ['./ila-evaluation.component.scss'],
})
export class IlaEvaluationComponent implements OnInit {
  @Input() editIlaCheck:boolean;
  default_students=0;
  employees:any[]=[];
  emp_checkbox:any[]=[];
  length_of_forms:number;
  audience:any[]=[];
  preview_evaluation?:any;
  allStudentEvaluations: any[]=[];
  titleArray:any[]=[];
  audienceArray:any[]=[];
  avaliabilityArray:any[]=[];
  checkedArray:any[]=[];
  undefinedCheck:boolean=false;
  ilaid:any;
  subscriptions = new SubSink();
  updateResult:any;
  updateArray:any;
  value_selected:any =[];
  value_selected_audience: any = [];
  value_selected_employee: any = [];
  value_selected_mandatory: any = [];
  tempSrc: any = [];
  showLoader=false;
  @Output() formChanges = new EventEmitter();
  @Output() loadingEvent = new EventEmitter<boolean>();
  hasAddedEvaluation: boolean = false;
  selectedAudienceName: string = '';
  ilaStudentEvaluationForm :UntypedFormGroup;
  ilaStudentEvaluationLinkId;
  isStudentEvalRemove:boolean = false;
  @Input() mode: string;
  constructor(
    public flyPanelService: FlyInPanelService,
    private fb:UntypedFormBuilder,
    private vcf: ViewContainerRef,
    private studentEvaluationForms : StudentEvaluationService,
    private studentEvaluationAvaliabilityService : StudentEvaluationAvaliabilitiesService,
    private studentEvaluationAudiencesService : StudentEvaluationAudiencesService,
    private alert: SweetAlertService,
    private ilaService : IlaStudentEvaluationLinkService,
    private labelPipe: LabelReplacementPipe,
    private saveStore: Store<{ saveIla: any }>) {}

  ngOnInit(): void {
    this.loadAsync();
    this.emp_checkbox = [
      {id:1, text:'Require Employees to provide response to all Evaluation questions', checked:true}
    ]
  }
  
  async loadAsync() {
    try {
      this.loadingEvent.emit(true);
      await this.readyStudentAudience();
      await this.readyEvaluationForms();
      await this.intializeILAStudentEvaluationForm();
  
      await new Promise<void>((resolve, reject) => {
        const subscription = this.saveStore.pipe(select('saveIla')).subscribe({
          next: async (res: any) => {
            try {
              if (res?.saveData?.result !== undefined) {
                this.ilaid = res.saveData.result.id;
                await this.populateEvauationForm();
              }
              resolve(); 
            } catch (error) {
              reject(error); 
            }
          },
          error: (err) => {
            reject(err); 
          },
          complete: () => {
            subscription.unsubscribe(); 
          },
        });
  
        this.subscriptions.sink = subscription; 
      });
    } catch (error) {
      this.loadingEvent.emit(false);
    } finally {
      this.loadingEvent.emit(false); 
    }
  }
  
  intializeILAStudentEvaluationForm(){
  this.ilaStudentEvaluationForm = this.fb.group({
    linkStudentFormEvauation: ['', Validators.required],
    audience: ['',Validators.required],
    makeEvauationAvailableForEmp: [null],
    isEmployeeRequired: [false],
  });
  if (this.mode === 'view') {
    this.ilaStudentEvaluationForm.get('linkStudentFormEvauation')?.disable();
  }
}

  addEvaluationForm() {
    this.hasAddedEvaluation=true;
    this.isStudentEvalRemove = false;
  }
  deleteEvaluationForm() {
    this.hasAddedEvaluation = false;
    this.isStudentEvalRemove = true;
  }
  async populateEvauationForm(){
    this.showLoader=true;
    if (this.ilaid!=null || this.ilaid!=undefined || this.ilaid!+'') {
      await this.ilaService.getLinkedStudentEvaluationsData(this.ilaid).then((res)=>{
        if (res.length!=0) {
          this.default_students = res.length;
          res.forEach(async (element,index)=>{
            await this.addEvaluationFormWithValues(element.studentEvalFormID,element.studentEvalAvailabilityID,element.isAllQuestionMandatory,element.id,element.studentEvalAudienceID);
          });
        }
        if (res.length>0) {
            this.addEvaluationForm();
       }
      }).catch((err)=>{

      });
    }
    this.showLoader=false;
  }

  async addEvaluationFormWithValues(linkStudentFormEvauations:any,makeEvauationAvailableForEmps:any,EmployeeRequired:any,linkId:any,studentEvalAudienceID:any) {
    const selectedAudience = await this.audience.find(x=>x.id == studentEvalAudienceID)?.name;
    this.ilaStudentEvaluationForm.patchValue({
      linkStudentFormEvauation: linkStudentFormEvauations,
      makeEvauationAvailableForEmp: makeEvauationAvailableForEmps,
      isEmployeeRequired: EmployeeRequired,
      audience : selectedAudience
    });
    this.ilaStudentEvaluationLinkId=linkId;
  }

  onStudentEvalChange(){
    this.ilaStudentEvaluationForm.get('audience').setValue(this.audience[0].name)
  }

  async readyEvaluationForms(){
    await this.studentEvaluationForms.getAll().then((res)=>{
      if(res === undefined){
        this.undefinedCheck = true;
      }
      res=res.filter(x=>x.isPublished===true);
      this.allStudentEvaluations = res;
    }).catch((err)=>{

    });
  }

  readyStudentAvalibility(){
    this.studentEvaluationAvaliabilityService.getAll().then((res)=>{
      this.employees = res;
    }).catch((err)=>{

    });
  }

  readyStudentAudience(){
    this.studentEvaluationAudiencesService.getAll().then((res)=>{
      this.audience = res;
    }).catch((err)=>{
    });
  }

  ChangeData(name:string,i:any,k:any){
    if(this.undefinedCheck === true){
      this.readyEvaluationForms();
    }
    switch(name){
      case 'Title':
        this.titleArray.push({
          id:k.id,
          name:i.name,
          evalformId:i.id
        })
        this.updateResult = i;
        break;
      case 'Audience':
        this.audienceArray.push({
          id:k.id,
          name:i.name,
          audienceId:i.id
        });
        break;
      case 'Avaliability':
        this.avaliabilityArray.push({
          id:k.id,
          name:i.name,
          avaliabilityId:i.id
        })
        break;

      case 'Mandatory':
        this.checkedArray.push({
          id:k.id,
          name:i
        })
    }
  }

  async saveILAStudentEvaluation(){
     const linkStudentFormEvaluationValue = this.ilaStudentEvaluationForm?.get('linkStudentFormEvauation')?.value;
     var audienceValue= this.ilaStudentEvaluationForm?.get('audience')?.value
     const audienceDetail = this.audience.find(x=>x.name == audienceValue);
     if (linkStudentFormEvaluationValue && audienceValue) {
     const options: ILA_StudentEvaluation_LinkOption = {
      studentEvalFormID: this.ilaStudentEvaluationForm?.get('linkStudentFormEvauation')?.value,
      studentEvalAvailabilityID: this.ilaStudentEvaluationForm?.get('makeEvauationAvailableForEmp')?.value,
      isAllQuestionMandatory: this.ilaStudentEvaluationForm?.get('isEmployeeRequired')?.value,
      studentEvalAudienceID: audienceDetail.id,
      ilaStudenEvaluationLinkId:this?.ilaStudentEvaluationLinkId,
      isEvalRemove:this.isStudentEvalRemove,
    };

     await this.ilaService.createLinks(this.ilaid,options).then(async (res)=>{
     this.alert.successToast(await this.labelPipe.transform('ILA') + ' Student Evaluation Links Created Successfully.');
     }).catch((err)=>{
     })
    }
  }

  openFlyInPanelEvaluationSettings(templateRef: any){
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);

  }

  openFlyInPanelCreateNewStudentEvaluation(templateRef: any){
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
    this.preview_evaluation=null;
  }

  openFlyInPanelPreviewStudentEvaluation(templateRef: any){
    const ctrl = this.ilaStudentEvaluationForm.get('linkStudentFormEvauation').value;
      var evaluation = this.allStudentEvaluations.find(x=>x.id === ctrl);
      this.updateResult=evaluation;
      if(this.updateResult){
        this.updateArray = this.updateResult;
        this.preview_evaluation=evaluation;
        const portal = new TemplatePortal(templateRef, this.vcf);
        this.flyPanelService.open(portal);
      }
      else if (!this.updateResult){
        this.alert.errorToast('Please Select Link Student Form Evaluation');
      }
  }
}

export class Student_Form{
  id:any;
  text?:string;
  checked?:boolean;
}
