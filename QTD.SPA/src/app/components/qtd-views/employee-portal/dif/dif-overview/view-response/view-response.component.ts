import { Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { DIFSurvey } from '@models/DIFSurvey/DIFSurvey';
import { DIFSurveyResponseVm } from '@models/DIFSurvey/DIFSurveyResponseVM';
import { DIFSurvey_Task } from '@models/DIFSurvey/DIFSurvey_Task';
import { DIFSurveyViewResponseVm } from '@models/DIFSurvey/DifSurveyViewResponseVm';
import { Store } from '@ngrx/store';
import { ApiDifSurveyService } from 'src/app/_Services/QTD/DifSurvey/api.difsurvey.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';

@Component({
  selector: 'app-view-response',
  templateUrl: './view-response.component.html',
  styleUrls: ['./view-response.component.scss']
})
export class ViewResponseComponent implements OnInit {
  viewResponseForm: UntypedFormGroup;
  Editor = ckcustomBuild;
  responseDataSource: MatTableDataSource<DIFSurveyResponseVm>;
  tableColumns:string[];
  id:string;
  difSurveyViewResponse:DIFSurveyViewResponseVm;
  @ViewChild(MatSort) sort: MatSort;
  
  constructor(private router: Router, private store: Store<{ toggle: string }>,
    private dIFSurveyService: ApiDifSurveyService,  private route : ActivatedRoute, private fb: UntypedFormBuilder) { }

  ngOnInit(): void {
    this.initializeCreateDifForm();
    this.store.dispatch(sideBarClose());
    this.tableColumns=['fullNumber','description','difficulty','importance','frequency','textBox'];
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
    this.loadAsync();
  }

  initializeCreateDifForm() {
    this.viewResponseForm = this.fb.group({
      additionalComment: new UntypedFormControl({value:null, disabled: true }),
    });
  }

  goBack(){
    this.router.navigate(['/emp/dif-survey/overview']);
  }

  async loadAsync() {
     await this.dIFSurveyService.getAllEmployeeResponses(this.id).then((res) => {
      this.difSurveyViewResponse=res;
      this.responseDataSource = new MatTableDataSource<DIFSurveyResponseVm>(this.difSurveyViewResponse?.difSurveyResponseVM);     
      setTimeout(()=>{
        this.responseDataSource.sort = this.sort;
      },1); 
    });
    this.responseDataSource.sortingDataAccessor=this.customSortAccessor;
    this.viewResponseForm.get('additionalComment').setValue(this.difSurveyViewResponse?.additionalComments);
  }

  customSortAccessor = (difSurveyResponse: DIFSurveyResponseVm, column: string): string  => {
    switch (column) {
      case 'fullNumber':
        return difSurveyResponse.number;
      case 'description':
        return difSurveyResponse.taskDescription;
      
      default:
        return difSurveyResponse[column];
    }
  };
}
