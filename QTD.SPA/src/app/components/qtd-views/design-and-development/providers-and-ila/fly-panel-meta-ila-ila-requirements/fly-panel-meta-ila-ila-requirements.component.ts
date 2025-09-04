import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import {
  UntypedFormBuilder,
} from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { MetaILAConfigPublishOptionService } from 'src/app/_Services/QTD/meta-ila-config-publish-option.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { VersionMetaILAService } from 'src/app/_Services/QTD/version-meta-ila.service';
import { MetaILAILARequirements_VM } from 'src/app/_DtoModels/MetaILA/MetaILAILARequirements_VM';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ILARequirementsDetailsVM } from 'src/app/_DtoModels/ILA/ILARequirementsDetailsVM';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ClassSchedules } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedules';

@Component({
  selector: 'app-fly-panel-meta-ila-ila-requirements',
  templateUrl: './fly-panel-meta-ila-ila-requirements.component.html',
  styleUrls: ['./fly-panel-meta-ila-ila-requirements.component.scss'],
})
export class FlyPanelMetaIlaIlaRequirementsComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Input() ilaId: string;
  ilaTraineeEvaluations: MetaILAILARequirements_VM;
  ilaRequirementsDetails: ILARequirementsDetailsVM= new ILARequirementsDetailsVM();
  ilaTitle : string;
  detailsDataSource: MatTableDataSource<ClassSchedules>=new MatTableDataSource<ClassSchedules>();
  displayColumns: string[] = ['instructor', 'location', 'startDate'];

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private fb: UntypedFormBuilder,
    public dialog: MatDialog,
    private vcr: ViewContainerRef,
    private metaILAConfigService: MetaILAConfigPublishOptionService,
    private alert: SweetAlertService,
    private metaILAService: MetaILAService,
    private version_metaILA: VersionMetaILAService,
    private iLAService: IlaService
  ) { }

  async ngOnInit(): Promise<void> {
    await this.getMetaILAILARequirements(this.ilaId);
    await this.getILADetailsByILAId(this.ilaId);
    
  }

  closeFlyPanel() {
    this.closed.emit(false);
  }

  async getMetaILAILARequirements(ilaId: string) {
    this.ilaTraineeEvaluations = await this.metaILAService.getMetaILAILARequirements(ilaId);
  }

  async getILADetailsByILAId(ilaId: string) {
    this.ilaRequirementsDetails = await this.iLAService.getILADetailsByILAId(ilaId);
    this.detailsDataSource.data = this.ilaRequirementsDetails.classSchedules;
    this.ilaTitle=this.ilaRequirementsDetails.ilaTitle;
  }
}
