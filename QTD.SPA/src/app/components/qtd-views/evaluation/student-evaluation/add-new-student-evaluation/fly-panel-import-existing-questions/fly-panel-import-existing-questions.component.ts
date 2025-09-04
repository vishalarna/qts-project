import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { StudentEvaluation_Question_LinkCreateOptions } from 'src/app/_DtoModels/StudentEvaluationQuestions/StudentEvaluation_Question_LinkCreateOptions';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { QuestionBankService } from 'src/app/_Services/QTD/question-bank.service';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-import-existing-questions',
  templateUrl: './fly-panel-import-existing-questions.component.html',
  styleUrls: ['./fly-panel-import-existing-questions.component.scss']
})
export class FlyPanelImportExistingQuestionsComponent implements OnInit {
  toolList : any[] = [];
  questions : any[];
  dataQuestions:any[]=[];
  toolControl = new UntypedFormControl();
  spinner = false;
  selection = new SelectionModel<any>(true, []);
  DataSource: MatTableDataSource<any>;
  unlinkIds: any[] = [];
  displayColumns: string[] = ['id','questionId','stem'];
  linkTool = false;
  showTool = true;
  dialogTitle:string;
  dialogDesc = "";
  toolIds:any[] = [];
  showActive: boolean = true;
  filteredList:any[]=[];
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() studentEvaluationId =0;
  @Input() data :any[] = [];
  @ViewChild(MatSort) tblSort: MatSort/* ) {
    if (sort) this.DataSource.sort = sort;
  } */
  @Input() alreadyLinked: any[] = [];

  constructor(private quesstionBankService : QuestionBankService,
    public flyPanelSrvc : FlyInPanelService,
    private vcf : ViewContainerRef,
    private alert : SweetAlertService,
    public dialog : MatDialog,
    private studentEvaluationService : StudentEvaluationService,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe,
    private labelPipe: LabelReplacementPipe,
) { }
  async ngOnInit(): Promise<void> {
    this.readyQuestionData();
    this.dialogTitle=  "Unlink " + await this.labelPipe.transform('Tool') + "s From Task";
  }

  async readyQuestionData(){
    // this.unlinkIds = [];
    this.selection.clear();
    this.questions = await this.quesstionBankService.getAll();
    
    this.dataQuestions = this.questions.filter(q => q.active)
    .map(q => ({...q, isLinked: this.alreadyLinked.includes(q.id?.toString())
    }));
    
    this.DataSource = new MatTableDataSource(this.dataQuestions);
    setTimeout(()=>{
      this.DataSource.sort = this.tblSort;
    },1) 
  
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }
  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  openLinkPanel(templateRef:any){
    const portal = new TemplatePortal(templateRef,this.vcf);
    this.flyPanelSrvc.open(portal);
  }
  ImportQuestionsToEvaluation()
{
 var options = new StudentEvaluation_Question_LinkCreateOptions();
 options.questionIds = this.unlinkIds;
 options.studentEvaluationId = this.studentEvaluationId

 this.studentEvaluationService.LinkQuestions(this.studentEvaluationId,options).then((res)=>
 {
  this.closed.emit();
  this.refresh.emit();
  this.alert.successToast('Successfully import Questions to the Evaluation');
  //this.getLinkedQuestions(this.studentEvaluationId);
 })

}
filterData(e: Event) {
  let filterString = (e.target as HTMLInputElement).value;
  this.DataSource.filter = filterString;
  // this.DataSource.filter = [
  //   ...this.questions.filter((x) =>
  //     x.stem.toLowerCase().includes(String(filterString).toLowerCase())
  //   ),
  // ];
}
filterStatus(active: boolean) {
  this.DataSource.data = [...this.questions.filter((x) => x.active == active)];
  //this.DataSource.data.filter((x) => x.active == active);
  this.showActive = active;
}
}
