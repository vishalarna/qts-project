import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { MetaILAStatusService } from './../../../../../_Services/QTD/meta-ila-status.service';
/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import { DatePipe } from '@angular/common';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { MetaILAUpdateOptions } from 'src/app/_DtoModels/MetaILA/MetaILAUpdateOptions';

@Component({
  selector: 'app-fly-panel-bulk-edit',
  templateUrl: './fly-panel-bulk-edit.component.html',
  styleUrls: ['./fly-panel-bulk-edit.component.scss']
})
export class FlyPanelBulkEditComponent implements OnInit {
  @Input() selectedCheckboxes:any;
  @Input() selectedCheckboxesMetaILA:any;  
  step1Form: UntypedFormGroup;
  step2Form: UntypedFormGroup;
  datePipe = new DatePipe('en-us');
  category:any[]=[];
  status:any[]=[];
  
  constructor(
    public flyPanelService:FlyInPanelService,
    public providerService : ProviderService,
    public alert: SweetAlertService,
    private fb: UntypedFormBuilder,
    public metaILAStatusService : MetaILAStatusService,
    public metaILAService : MetaILAService) { }

  public Editor = ckcustomBuild;


  displayColumns: string[] = ['number', 'type', 'description'];
  DataSource: MatTableDataSource<any>;
  @ViewChild('stepper') stepper:MatStepper

  ngOnInit(): void {
    this.readyStep1Form();
    this.readyStep2Form();
   /*  this.readyMetaILAStatus();    
    this.readyProviders(); */
    //this.getMetaILAID();
    this.setTableData();
  }

  setTableData(){
    const data = [
      {
        num:'1.1.1.3',
        name:'Intro to QTD',
        action:'YES'
      },
      {
        num:'1.1.1.3',
        name:'Maintaining PER-005',
        action:'NO'
      }
    ]
    this.DataSource = new MatTableDataSource(data);
  }

  publishClicked(){
    var options = new MetaILAUpdateOptions();
    
    this.flyPanelService.close();
  }

 /*  readyProviders(){
    this.providerService.getAll().then((res)=>{
      
      res.forEach((i)=>{
        this.category.push({
          id:i.id,
          name : i.name,
          providerId : i.providerLevelId
        });
      })
   
    }).catch((err)=>{
      this.alert.errorToast('Error Fetching Providers');
    })
  } */

  readyStep1Form(){
    this.step1Form = this.fb.group({
      /* providerId: new FormControl(''),
      changeProvider : new FormControl(false),
      changestatus : new FormControl(false), */
      changeInactiveStatus : new UntypedFormControl(false),
      delete : new UntypedFormControl('')
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      reason: new UntypedFormControl(''),
    });
  }

  /* readyMetaILAStatus(){
    this.metaILAStatusService.getAll().then((res)=>{
      
      res.forEach((i)=>{
        this.status.push({
          id:i.id,
          name:i.name
        })
      })
    }).catch((err)=>{
      this.alert.errorToast('Error Fetching Meta ILA Status');
    })
  } */

  
}
