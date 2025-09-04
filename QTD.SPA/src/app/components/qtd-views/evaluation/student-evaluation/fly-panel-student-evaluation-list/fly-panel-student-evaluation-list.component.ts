import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-student-evaluation-list',
  templateUrl: './fly-panel-student-evaluation-list.component.html',
  styleUrls: ['./fly-panel-student-evaluation-list.component.scss']
})
export class FlyPanelStudentEvaluationListComponent implements OnInit {
  @Input() moduleName:any;
  tasktreeControl = new NestedTreeControl<any>(
    (node: any) => node.children
  );
  treedataSource = new MatTreeNestedDataSource<any>();
  constructor(
    public flyInClose:FlyInPanelService,
    private studentEvalService:StudentEvaluationService,
    private alert: SweetAlertService
  ) { }

  ngOnInit(): void {
    this.getNames();
  }

  name:any;
  getNames(){
    switch(this.moduleName){
      case 'Publish':
        this.name = 'Published Student Evaluations';
        this.studentEvalService.getList(this.moduleName).then((data)=>{
          this.treedataSource.data = Object.assign([],data);

          
        }).catch(()=>{
          this.alert.errorToast('Error Fetching Student Evaluations');
        });
        break;

      case 'Development':
        this.name = 'In Development Student Evaluations';
        this.studentEvalService.getList(this.moduleName).then((data)=>{
          this.treedataSource.data = Object.assign([],data);
          

        }).catch(()=>{
          this.alert.errorToast('Error Fetching Student Evaluations');
        });
        break;
      
      case 'Active':
        this.name = 'Inactive Student Evaluations';
        this.studentEvalService.getList(this.moduleName).then((data)=>{
          this.treedataSource.data = Object.assign([],data);
          
        }).catch(()=>{
          this.alert.errorToast('Error Fetching Student Evaluations');
        });

        break;
    }
  }


}
