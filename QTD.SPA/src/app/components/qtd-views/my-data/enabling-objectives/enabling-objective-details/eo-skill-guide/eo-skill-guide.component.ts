import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjectiveOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveOption';
import { EnablingObjective_Question } from 'src/app/_DtoModels/EnablingObjective_Question/EnablingObjective_Question';
import { EnablingObjective_Step } from 'src/app/_DtoModels/EnablingObjective_Step/EnablingObjective_Step';
import { EnablingObjective_Suggestion } from 'src/app/_DtoModels/EnablingObjective_Suggestion/EnablingObjective_Suggestion';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-eo-skill-guide',
  templateUrl: './eo-skill-guide.component.html',
  styleUrls: ['./eo-skill-guide.component.scss']
})
export class EoSkillGuideComponent implements OnInit {
  @Input() isActive = false;

  steps: EnablingObjective_Step[] = [];
  questions: EnablingObjective_Question[] = [];
  suggestions: EnablingObjective_Suggestion[] = [];
  displayColumns = ['order','number', 'question', 'answer', 'buttons'];
  suggestionDisplay = ['order','number', 'description', 'buttons'];
  Datasource: MatTableDataSource<any> = new MatTableDataSource();
  SuggestionSource: MatTableDataSource<any> = new MatTableDataSource();
  eo: EnablingObjective;
  collapseImage: boolean[] = [];
  tools: Tool[] = [];
  tags: any[] = [];
  subscription = new SubSink();
  eoId = "";
  images: any[] = [];
  originalNumbers: any[] = [];
  savingOrder = false;
  dialogTitle = "";
  dialogDesc = "";

  isStep = false;
  isSuggestion = false;
  stepDeleteId = "";
  questionDeleteId = "";
  suggestionDeleteId = "";
  conditions: any;
  criteria: any ;
  references: any;

  deleteDisable = false;
  originalQANumbers:number[] = [];
  originalSuggNumbers:number[] = [];
  eoQuestion: EnablingObjective_Question | undefined;
  selectedStep: EnablingObjective_Step = new EnablingObjective_Step();
  eoSuggestion: EnablingObjective_Suggestion | undefined;
  enablingObjective: EnablingObjective;

  @ViewChild('suggestionPaginator') suggestionPaginator: MatPaginator;
  @ViewChild('questionPaginator') questionPaginator: MatPaginator;
  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private eoService: EnablingObjectivesService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe(async (res: any) => {
      var t = String(res.id).split('.-');
      this.eoId = String(res.id).split('.-')[1];
      this.getEO();
      this.getToolsData();
      this.closeAndRefreshSteps();
      this.closeAndRefreshQuestions();
      this.closeAndRefreshSuggestion();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getEO(){
    this.enablingObjective = await this.eoService.get(this.eoId);

    this.conditions = this.enablingObjective.conditions;
    this.criteria = this.enablingObjective.criteria;
    this.references = this.enablingObjective.references;

  }

  async dropQA(event:any){

  }

  async dropSuggestion(event:any){

  }

  async drop(event: CdkDragDrop<any[]>) {
    this.savingOrder = true;
    moveItemInArray(this.steps, event.previousIndex, event.currentIndex);
    moveItemInArray(this.images, event.previousIndex, event.currentIndex);
    moveItemInArray(this.collapseImage,event.previousIndex,event.currentIndex);

  }

  async getDeleteData(e: any) {
    this.deleteDisable = true;
    var options = new EnablingObjectiveOptions();
    var data = JSON.parse(e);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    options.actionType = "delete";


    if (this.isStep) {
      this.eoService.deleteSteps(this.eoId, this.stepDeleteId, options).then((_) => {
        this.alert.successToast("SQ Step Deleted");
        this.closeAndRefreshSteps();
      })
    }
    else if (this.isSuggestion) {
      await this.eoService.deleteSuggestion(this.eoId, this.suggestionDeleteId, options).then((_) => {
        this.alert.successToast("Step Deleted");
        this.closeAndRefreshSuggestion();
      }).finally(() => {
        this.deleteDisable = false;
      });
    }
    else {
      this.eoService.removeQuestion(this.eoId, this.questionDeleteId, options).then((_) => {
        this.alert.successToast("Question Deleted");
        this.closeAndRefreshQuestions();
      });
    }
    // else if (this.isStep) {
    //   await this.taskService.deleteSteps(this.taskId, this.stepDeleteId, options).then((_) => {
    //     this.alert.successToast("Step Deleted");
    //     this.closeAndRefreshSteps();
    //   }).finally(() => {
    //     this.deleteDisable = false;
    //   });
    // }
    // else {
    //   this.taskService.removeQuestion(this.taskId, this.questionDeleteId, options).then((_) => {
    //     this.alert.successToast("Question Deleted");
    //     this.refreshQuestions();
    //   });
    // }

  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async closeAndRefreshSteps() {
    this.flypanelSrvc.close()
    this.steps = await this.eoService.getSteps(this.eoId);

    const regex = /<img (.*?)>/;
    this.images = [];
    this.originalNumbers = [];
    this.steps.forEach((data: EnablingObjective_Step) => {
      this.collapseImage.push(false);
      this.originalNumbers.push(data.number);
      this.images.push(data.description?.match(regex)?.[0]);
      var description = data.description.split(regex);
      if (description.length > 1) {
        data.description = description[0] + description[2];
      }
    });

  }

  openStepFlyPanel(templateRef: any, step: EnablingObjective_Step | undefined, index: any) {
    
    if (step !== undefined) {
      this.selectedStep = Object.assign(this.selectedStep, step ?? new EnablingObjective_Step());
      this.selectedStep.description = this.selectedStep.description.concat(this.images[index] ?? "");
    }
    else {
      this.selectedStep = new EnablingObjective_Step();
    }

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async deleteStep(templateRef: any, stepId: any) {
    this.stepDeleteId = stepId;
    this.isStep = true;
    this.isSuggestion = false;
    this.dialogTitle = "Delete " + await this.transformTitle('Enabling Objective') +"Step";

    this.dialogDesc = `This SQ is currently available for SQ Qualification in EMP, Are you sure you wish to delete this Step?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async closeAndRefreshQuestions() {
    this.flypanelSrvc.close();
    this.eoQuestion = undefined;
    this.questions = await this.eoService.getQuestions(this.eoId);
    this.originalQANumbers = [];
    this.questions.forEach((data)=>{
      this.originalQANumbers.push(data.questionNumber);
    })

    this.Datasource.data = this.questions;
    this.Datasource.paginator = this.questionPaginator;
  }

  async deleteQuestion(templateRef: any, question: EnablingObjective_Question) {
    this.questionDeleteId = question.id;
    this.isStep = false;
    this.isSuggestion = false;
    this.dialogTitle = "Delete "+ await this.transformTitle('Enabling Objective') +"Question";

    this.dialogDesc = `This ` + await this.transformTitle('Enabling Objective') + `is currently available for ` + await this.transformTitle('Enabling Objective') + `Qualification in EMP, are you sure you wish to delete this Question?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openQuestionFlyPanel(templateRef: any, question: EnablingObjective_Question | undefined) {
    this.eoQuestion = question;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  async closeAndRefreshSuggestion() {
    this.flypanelSrvc.close();
    this.eoSuggestion = undefined;
    this.suggestions = await this.eoService.getSuggestions(this.eoId);
    this.originalSuggNumbers = [];
    this.suggestions.forEach(element => {
      this.originalSuggNumbers.push(element.number);
    });

    this.SuggestionSource.data = this.suggestions;
    this.SuggestionSource.paginator = this.suggestionPaginator;
  }

  openSuggestionFlyPanel(templateRef: any, suggestion: EnablingObjective_Suggestion | undefined) {
    this.eoSuggestion = suggestion;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  deleteSuggestion(templateRef: any, suggestion: EnablingObjective_Suggestion) {
    this.suggestionDeleteId = suggestion.id;
    this.isStep = false;
    this.isSuggestion = true;
    this.dialogTitle = "Delete SQ Suggestion";

    this.dialogDesc = `This SQ is currently available for SQ Qualification in EMP, are you sure you wish to delete this Suggestion?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getToolsData() {
    this.tools = await this.eoService.getTools(this.eoId);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

}
