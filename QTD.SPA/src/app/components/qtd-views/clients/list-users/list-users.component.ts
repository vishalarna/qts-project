import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { AddEditUserComponent } from './add-edit-user/add-edit-user.component';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.scss'],
})
export class ListUsersComponent implements AfterViewInit, OnDestroy, OnInit {
  dtOptions: DataTables.Settings = {};

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;

  constructor(
    private translate: TranslateService,
    private modalService: NgbModal,
    private alert: SweetAlertService
  ) {
    
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }
  ngAfterViewInit(): void {
    this.dtTrigger.next(null);
  }
  ngOnDestroy(): void {
    
    // this.dtTrigger.unsubscribe();
  }
  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
    };
  }

  open(name?: any) {
    const modalRef = this.modalService.open(AddEditUserComponent, {
     size:'lg',
      scrollable: true,
      keyboard: false,
      backdrop: 'static',
    });

    if (name) {
      modalRef.componentInstance.editID = name;
    }
    modalRef.componentInstance.createEditDone.subscribe((e: any) => {});
  }
}
