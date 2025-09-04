import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import parse from 'node-html-parser';
import { QuestionBankHistoryCreateOptions } from 'src/app/_DtoModels/QuestionBankHistory/QuestionBankHistoryCreateOptions';
import { QuestionBankService } from 'src/app/_Services/QTD/question-bank.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';

export interface EvaluationQuestions {
  id: string;
  question: string;
}
const ELEMENT_DATA: EvaluationQuestions[] = [
  {id: 'QTD_036', question: 'Here is a question for you'},
  {id: 'QTD_038', question: 'Here is another question for you'}
];
@Component({
  selector: 'app-student-evaluation-question-bank',
  templateUrl: './student-evaluation-question-bank.component.html',
  styleUrls: ['./student-evaluation-question-bank.component.scss']
})
export class StudentEvaluationQuestionBankComponent implements OnInit {
  dataSource :any;
  mode:'Add' | 'Edit' | 'Copy' = 'Add';
  questionId = 0;
  displayedColumns: string[] = ['id','question','active','action'];
  isActive: boolean = true;
  activeStatus: string;
  deleteDescription : string;
  modalHeader = '';
  modalDescription = '';
  filteredDataSource: MatTableDataSource<any> = new MatTableDataSource();
  constructor(public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private questionBankService: QuestionBankService,
    public dialog:MatDialog,
    public alert:SweetAlertService,
    private router:Router,
    private store: Store<{ toggle: string }>,) { }

  ngOnInit(): void
  {
    this.getAllQuestions();
  }
  async openFlyInPanel(templateRef: any,mode:any,id:any)
  {
    this.mode = mode;
    this.questionId = id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  async getAllQuestions()
  {
    await this.questionBankService.getAll().then((res)=>
    {
      this.dataSource = new MatTableDataSource(res);
      this.filteredDataSource =  new MatTableDataSource(res);
    })
  }
  filterQuestions(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.dataSource.filter = filter;
  }
  changeStatus(templateRef: any, row: any) {
    if(row.active === true)
    {
       this.isActive = false;
       this.activeStatus = 'Inactive'
    }
    else
    {
      this.isActive = true;
      this.activeStatus = 'Active'
    }
    this.questionId = row.id;
    
    this.modalHeader = row.active
      ? 'Inactive Student Evaluation Question'
      : 'Active' + ' Student Evaluation Question';

      this.modalDescription =`You are selecting to make this Student Evaluation Question ${row.questionId } ${parse(row.stem).innerText} ${this.activeStatus}`

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  MakeActive(e: any, active: boolean) {
    var options = new QuestionBankHistoryCreateOptions();
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.questionBankNotes = data['reason'];
    options.actionType = this.activeStatus.toLowerCase();
    this.questionBankService
      .makeActiveInactiveOrDelete(this.questionId, options)
      .then((res: any) => {
        this.getAllQuestions();
        this.alert.successToast("Student evaluation question status changed successfully")
      });
  }
  deleteStudentEvaluationQuestion(templateRef:any,row)
{
  this.questionId = row.id;
  this.deleteDescription = `You are selecting to delete the Student Evaluation ${parse(row.stem).innerText}. This cannot be undone.`;
   const dialogRef = this.dialog.open(templateRef, {
     width: '600px',
     height: 'auto',
     hasBackdrop: true,
     disableClose: true,
   });
 }
 Delete(e: any)
  {
    var options = new QuestionBankHistoryCreateOptions();
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.questionBankNotes = data['reason'];
    options.actionType = 'delete';


    this.questionBankService
      .makeActiveInactiveOrDelete(this.questionId, options)
      .then((res: any) => {
        this.getAllQuestions();
        this.alert.successToast("Student evaluation delete successfully")
      });

  }
  async goBack() {

    this.router.navigate(['evaluation/studentevaluation']);
  }

  toggleMainMenu() {
    //this.databroadcastSrvc.ToggleMainMenu.next('');
    this.store.dispatch(sideBarToggle())
  }

  dataSourceStatusFilter(status: any) {
    this.dataSource.data = this.filteredDataSource.data;
    
    
    if (status === 'Inactive') {
      this.dataSource.data = this.dataSource.data.filter(item => item.active === false);
    } else if (status === 'Active') {
      this.dataSource.data = this.dataSource.data.filter(item => item.active === true);
    }
  }
}
