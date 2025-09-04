import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-instructors-list',
  templateUrl: './fly-panel-instructors-list.component.html',
  styleUrls: ['./fly-panel-instructors-list.component.scss']
})
export class FlyPanelInstructorsListComponent implements OnInit {
  @Input() moduleName:any;
  tasktreeControl = new NestedTreeControl<any>(
    (node: any) => node.children
  );
  treedataSource = new MatTreeNestedDataSource<any>();
  constructor(
    public flyInClose:FlyInPanelService,
    private insService:InstructorService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.getNames();
  }

  name:any;
  async getNames(){
    switch(this.moduleName){
      case 'Catactive':
        this.name = 'Active Categories';
        this.getCategoriesList();

        break;

      case 'Catinactive':
        this.name = 'Inactive Categories';
        this.getCategoriesList();
        break;
      
      case 'Insactive':
        this.name = 'Active ' + await this.labelPipe.transform('Instructor') + 's';
        this.getInstructorsList();

        break;

      case 'Insinactive':
        this.name = 'Inactive ' + await this.labelPipe.transform('Instructor') + 's';
       this.getInstructorsList();
        break;

      case 'Workbook':
        this.name = await this.labelPipe.transform('Instructor') + ' Workbook Admins';
        this.insService.getinsList(this.moduleName).then((data)=>{
          this.treedataSource.data = Object.assign([],data);
          
        }).catch(()=>{
          this.alert.errorToast('Error Fetching Categories');
        });
        break;
    }
  }

  spinner:boolean
  getInstructorsList(){
    this.spinner = true;
    this.insService.getinsList(this.moduleName).then((data)=>{
      this.treedataSource.data = Object.assign([],data);

    }).catch(async ()=>{
      this.alert.errorToast('Error Fetching ' + await this.labelPipe.transform('Instructor') + 's');
    }).finally(()=>{
      this.spinner = false;
    });
  }

  getCategoriesList(){
    this.spinner = true;

    this.insService.getcatList(this.moduleName).then((data)=>{
      this.treedataSource.data = Object.assign([],data);

    }).catch(()=>{
      this.alert.errorToast('Error Fetching Categories');
    }).finally(()=>{
      this.spinner = false;
    });
  }

}
