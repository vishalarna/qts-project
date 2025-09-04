import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import {
  ModalDismissReasons,
  NgbActiveModal,
  NgbModal,
} from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose, sideBarMode } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-upload-emps',
  templateUrl: './upload-emps.component.html',
  styleUrls: ['./upload-emps.component.scss'],
})
export class UploadEmpsComponent implements OnInit, AfterViewInit,OnDestroy {
  csvLoaded: boolean = false;
  closeResult = '';

  constructor(
    private translate: TranslateService,
    private modalService: NgbModal,
    private alert: SweetAlertService,
    private databroadcastService: DataBroadcastService,
    private store: Store<{ toggle: string }>,
  ) {

    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
    this.databroadcastService.ShowMenuSideBar.next(false);
  }

  ngOnInit(): void {}
  ngAfterViewInit(): void {
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this.store.dispatch(sideBarMode({ mode: 'over' }));
  }
  ngOnDestroy(): void {
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: true }));
    this.store.dispatch(sideBarMode({ mode: 'side' }));
  }
  uploadCSVFile(e: any) {
    this.csvLoaded = true;
  }

  open(content: any) {
    this.modalService.open(content, {
      size: 'lg',
      scrollable: false,
      keyboard: false,
      backdrop: 'static',
    });
  }

  closeModal(modal: NgbActiveModal) {

    modal.close();
    this.csvLoaded = false;
  }
}
