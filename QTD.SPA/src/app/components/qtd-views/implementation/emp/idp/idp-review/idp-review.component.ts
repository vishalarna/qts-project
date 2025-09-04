import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { IDP_Review } from 'src/app/_DtoModels/IDP/IDP_Review';
import { IdpService } from 'src/app/_Services/QTD/idp.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';


@Component({
  selector: 'app-idp-review',
  templateUrl: './idp-review.component.html',
  styleUrls: ['./idp-review.component.scss']
})
export class IdpReviewComponent implements OnInit {
  displayedColumns: string[] = ['title', 'releaseDate', 'completeddDate', 'status', 'response', 'comments'];
  dataSource = new MatTableDataSource<IDP_Review>();
  yearnow = new Date().getFullYear();
  range: number[] = [];
  panelOpenState = true;
  selected = this.yearnow;
  empId = "";
  subscription = new SubSink();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private dataBroadcastService: DataBroadcastService,
    private idpService: IdpService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    // this.getyearsdropdown();
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.empId = res.id;
      this.readyData();
    })
  }

  async readyData() {
    var data = await this.idpService.getAllIDPReviews(this.empId);
    this.dataSource.data = data;
    
    this.dataSource.paginator = this.paginator;
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

}
