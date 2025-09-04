import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { Client } from 'src/app/_DtoModels/Client/ClientViewModel';
import { ClientService } from 'src/app/_Services/Auth/client.service';
import { AddEditClientComponent } from './add-edit-client/add-edit-client.component';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { ModifyTokenModel } from 'src/app/_DtoModels/Auth/ModifyTokenModel';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import {
  ActivatedRoute,
  ActivatedRouteSnapshot,
  Router,
} from '@angular/router';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { CustomClaimTypes } from 'src/app/_Shared/Utils/CustomClaims';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';

@Component({
  selector: 'app-list-clients',
  templateUrl: './list-clients.component.html',
  styleUrls: ['./list-clients.component.scss'],
})
export class ListClientsComponent implements OnInit {
  clients: Client[] = [];

  retunUrl: string = '';

  clientDataSource: MatTableDataSource<any>;
  displayedColumns: string[] = ['name', 'active', 'obj'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private translate: TranslateService,
    private _client: ClientService,
    private alert: SweetAlertService,
    private route: Router,
    private dialog: MatDialog
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
    this.getClients();
  }

  open(name?: any) {
    const dialogRef = this.dialog.open(AddEditClientComponent, {
      hasBackdrop: true,
      disableClose: true,
    });

    if (name) {
      dialogRef.componentInstance.editID = name;
    }

    dialogRef.afterClosed().subscribe((result) => {
      
      this.getClients();
    });
  }

  async getClients() {
    await this._client.getClients().then((res) => {
      this.clients = res;
      let tempSrc: any[] = [];
      this.clients.forEach((c) => {
        tempSrc.push({
          name: c.name,
          active: c.active,
          obj: c,
        });
      });

      this.clientDataSource = new MatTableDataSource(tempSrc);
      this.clientDataSource.sort = this.sort;
      this.clientDataSource.paginator = this.paginator;
    });
  }

  delete(name: string) {
    this.alert.confirmAlert('You want to delete?').then(async (result) => {
      if (result.isConfirmed) {
        await this._client.deactivate(name).then(
          (res) => this.alert.successToast(this.translate.instant('L.' + res)),
          () => {
            this.getClients();
          }
        );
      }
    });
  }

  gotoClientInstances(clientName: string) {
    this.route.navigate(['/clients/instances', clientName]);
  }
  filterData(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.clientDataSource.filter = filter;
  }
}
