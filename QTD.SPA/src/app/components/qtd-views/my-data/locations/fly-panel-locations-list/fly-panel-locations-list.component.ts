import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-locations-list',
  templateUrl: './fly-panel-locations-list.component.html',
  styleUrls: ['./fly-panel-locations-list.component.scss']
})
export class FlyPanelLocationsListComponent implements OnInit {
  @Input() moduleName:any;
  treedataSource = new MatTreeNestedDataSource<any>();
  tasktreeControl = new NestedTreeControl<any>(
    (node: any) => node.children
  );

  constructor(
    private locService:LocationService,
    private alert: SweetAlertService,
    public flyInService: FlyInPanelService,
    private labelPipe: LabelReplacementPipe,

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

      case 'Locactive':
        this.name = 'Active ' + await this.labelPipe.transform('Location') +'s';
        this.getLocationsList();
        break;

      case 'Locinactive':
        this.name = 'Inactive ' + await this.labelPipe.transform('Location') +'s';;
        this.getLocationsList();
        break;

    }
  }

  spinner:boolean;
  getLocationsList(){
    this.spinner = true;
    this.locService.getlocList(this.moduleName).then((data)=>{
      this.treedataSource.data = Object.assign([],data);
      }).catch(()=>{
        this.alert.errorToast('Error Fetching Categories');
      }).finally(()=>{
        this.spinner = false;
      });
  }

  getCategoriesList(){
    this.spinner = true;
    this.locService.getcatList(this.moduleName).then((data)=>{
      this.treedataSource.data = Object.assign([],data);

      }).catch(()=>{
        this.alert.errorToast('Error Fetching Categories');
      }).finally(()=>{
        this.spinner = false;
      });
  }

}
