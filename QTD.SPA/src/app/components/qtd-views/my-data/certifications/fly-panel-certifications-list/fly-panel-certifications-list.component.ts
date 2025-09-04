import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-certifications-list',
  templateUrl: './fly-panel-certifications-list.component.html',
  styleUrls: ['./fly-panel-certifications-list.component.scss']
})
export class FlyPanelCertificationsListComponent implements OnInit {
  @Input() moduleName:any;
  tasktreeControl = new NestedTreeControl<any>(
    (node: any) => node.children
  );
  treedataSource = new MatTableDataSource<any>();
  @ViewChild(MatSort) sort!:MatSort;
  @ViewChild(MatPaginator) paginator!:MatPaginator;
  displayColumns = ["id","name"];
  IADisplayedCols = ["id","name"];
  spinner:boolean;
  constructor(
    public flyInClose:FlyInPanelService,
    private certService:CertificationService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.getNames();
  }

  name:any;
  async getNames(){

    var transformedValue = await this.transformTitle("Certification")
    switch(this.moduleName){
      case 'Catactive':
        this.name = 'Active Issuing Authorities';
        this.getCategoriesList();
        break;

      case 'Catinactive':
        this.name = 'Inactive Issuing Authorities';
        this.getCategoriesList();
        break;

      case 'Certactive':
        this.name = `Active ${transformedValue}s`;
        this.getCertificationsList();
        break;

      case 'Certinactive':
        this.name = `Inactive ${transformedValue}s`;
        this.getCertificationsList();
        break;
    }
  }

  getCategoriesList(){
    this.spinner = true;
    this.certService.getcatList(this.moduleName).then((data)=>{
      this.treedataSource.data = data.map((x,i)=>{
        return {
          id:i+1,
          name:x.name,
        }
      })

      this.spinner = false;
      setTimeout(()=>{
        this.treedataSource.sort = this.sort;
        this.treedataSource.paginator = this.paginator;
      },1)

    }).finally(()=>{
      this.spinner = false;
    });
  }

  getCertificationsList(){
    this.spinner = true;
    this.certService.getcertList(this.moduleName).then((data)=>{
      this.treedataSource.data = data.map((x,i)=>{
        return {
          id:i+1,
          name:x.name
        }
      });

      this.spinner = false;
      setTimeout(()=>{
        this.treedataSource.sort = this.sort;
        this.treedataSource.paginator = this.paginator;
      },1)

    }).finally(()=>{
      this.spinner = false;
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 
}
