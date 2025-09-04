import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-version-history-details',
  templateUrl: './fly-panel-version-history-details.component.html',
  styleUrls: ['./fly-panel-version-history-details.component.scss'],
})
export class FlyPanelVersionHistoryDetailsComponent implements OnInit {
  @Input() ilaId;
  @Input() versionDate;
  @Input() versionNumber;
  ilaVersionDetails:any[];
  filteredVersionDetails: any[] = [];
  unlinkedDeletedDetails: any[] = [];
  changeByUsernames:any[] = [];
  linkedDetails:any[]=[];
  @Output () closed = new EventEmitter();
  publishedNotes:any[] = [];
  spinner:boolean;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private ilaService: IlaService
  ) {}

  async ngOnInit(): Promise<void> {
    this.spinner = true;
    await this.getILADetails();
    await this.getVersionDetails();
    this.spinner = false;
  }

  async getILADetails() {
    await this.ilaService.getIlaVersions(this.ilaId,true).then((res) => {
      this.ilaVersionDetails = res.filter(x=>x.effectiveDate==this.versionDate);
    });
  }

  getVersionDetails() {
    this.filteredVersionDetails = this.ilaVersionDetails.filter(
      (version) => version.effectiveDate === this.versionDate
    );
    this.changeByUsernames = [...new Set(this.filteredVersionDetails.map(item => item.userName))];
    const linkedDetails =   this.filteredVersionDetails.filter(
      (detail) => detail.state === 1 || detail.state ===2
    );
    this.linkedDetails =linkedDetails;

    this.unlinkedDeletedDetails = this.filteredVersionDetails.filter(
      (detail) => detail.state === 0
    );
    this.publishedNotes =this.filteredVersionDetails.filter((detail)=>detail.state === 4);
  }

}
