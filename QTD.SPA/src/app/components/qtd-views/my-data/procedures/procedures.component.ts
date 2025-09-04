import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import {
  freezeMenu,
  sideBarBackDrop,
  sideBarClose,
  sideBarDisableClose,
  sideBarMode,
  sideBarToggle,
} from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-procedures',
  templateUrl: './procedures.component.html',
  styleUrls: ['./procedures.component.scss'],
})
export class ProceduresComponent implements OnInit, OnDestroy {
  issuingAuthorityName : string = '';
  url : string = 'My Data / Procedures'
  constructor(private store: Store<{ toggle: string }>,) {}
  ngOnDestroy(): void {
    
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: true }));
    this.store.dispatch(sideBarMode({ mode: 'side' }));
  }

  ngOnInit(): void {
    
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this.store.dispatch(sideBarMode({ mode: 'over' }));
  }
  recieveIssuingAuthorityName(e:any){
    this.url = 'My Data / Procedures'
    this.issuingAuthorityName = e;
    this.url = this.url.concat('/ ',this.issuingAuthorityName);
  }
}
