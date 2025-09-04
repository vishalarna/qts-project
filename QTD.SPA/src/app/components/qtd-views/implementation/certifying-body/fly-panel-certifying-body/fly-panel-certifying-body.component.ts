import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CertifyingBody } from 'src/app/_DtoModels/CertifyingBody/CertifyingBody';
import { CertifyingBodyUpdateOptions } from 'src/app/_DtoModels/CertifyingBody/CertifyingBodyUpdateOptions';
import { CertifyingBodiesService } from 'src/app/_Services/QTD/certifying-bodies.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-certifying-body',
  templateUrl: './fly-panel-certifying-body.component.html',
  styleUrls: ['./fly-panel-certifying-body.component.scss'],
})
export class FlyPanelCertifyingBodyComponent implements OnInit {
  showlistCertifyingBody: boolean = true;
  certBodyHeader: string = '';
  certBodyName: string = '';
  CertBodyUpdateOpt!: CertifyingBodyUpdateOptions;
  CertifyingBodyPanelVisible: boolean = false;

  listCertifyingBody: CertifyingBody[] = [];
  tempCertBodyList: CertifyingBody[] = [];
  editCertBodyId: any;

  @Output()
  closed = new EventEmitter<any>();

  constructor(
    private certBodyService: CertifyingBodiesService,
    public flyPanelSrvc: FlyInPanelService,
    public translate: TranslateService,
    private alert: SweetAlertService,
    private DataBroadcastService: DataBroadcastService
  ) {}

  ngOnInit(): void {
    this.getCertifyingBodiesList();
  }

  async getCertifyingBodiesList() {
    if (this.listCertifyingBody.length == 0) {
      await this.certBodyService
        .getAll()
        .then(
          (data) => (this.listCertifyingBody = this.tempCertBodyList = data)
        );
    }
    this.certBodyHeader = 'Certifying Bodies';
    this.showlistCertifyingBody = true;
  }

  filterCertifyingBodies(filter: any) {
    let f = filter.target as HTMLInputElement;
    this.listCertifyingBody = this.tempCertBodyList;
    this.listCertifyingBody = this.listCertifyingBody.filter(
      (item) => item.name.toLowerCase().indexOf(f.value.toLowerCase()) > -1
    );
  }

  addCertBody() {
    this.certBodyName = '';
    this.certBodyHeader = 'Add New Certifying Body';
    this.showlistCertifyingBody = false;
  }
  editCertBody(certBody: CertifyingBody) {
    this.certBodyHeader = 'Edit Certifying Body';

    this.CertBodyUpdateOpt = new CertifyingBodyUpdateOptions();
    this.CertBodyUpdateOpt.name = certBody.name;
    this.editCertBodyId = certBody.id;
    this.showlistCertifyingBody = false;
  }

  async deleteCertBody(id: any) {
    await this.certBodyService.delete(id).then((res) => {
      if (res) {
        this.showlistCertifyingBody = true;
        this.listCertifyingBody = [];
        this.certBodyName = '';
        this.getCertifyingBodiesList();
        this.DataBroadcastService.refreshListName.next('certBody');
        this.alert.successToast(this.translate.instant(`L.recdelete`));
      }
    });
  }

  async createNewCertifyingBody() {
    await this.certBodyService
      .create({ name: this.certBodyName })
      .then((res) => {
        if (res) {
          this.listCertifyingBody = [];
          this.getCertifyingBodiesList();
          this.certBodyName = '';
          this.alert.successToast(this.translate.instant(`L.recAdded`));
          this.DataBroadcastService.refreshListName.next('certBody');
        }
      });
  }

  async updateCertifyingBody() {
    await this.certBodyService
      .update(this.editCertBodyId, this.CertBodyUpdateOpt)
      .then((res) => {
        if (res) {
          // this.certBodyName = '';
          this.listCertifyingBody = [];
          this.alert.successToast(this.translate.instant(`L.recUpdated`));
          this.DataBroadcastService.refreshListName.next('certBody');
        }
      });
  }
}
