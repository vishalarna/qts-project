import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose, sideBarMode } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-positions',
  templateUrl: './positions.component.html',
  styleUrls: ['./positions.component.scss']
})
export class PositionsComponent implements OnInit,OnDestroy,AfterViewInit {

  isBulkEdit: boolean = false;
  constructor(private store: Store<{ toggle: string }>) {}

  ngOnInit(): void {
  }
  ngAfterViewInit(): void {
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this.store.dispatch(sideBarMode({ mode: 'over' }));
  }
  ngOnDestroy(): void {
    //
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: true }));
    this.store.dispatch(sideBarMode({ mode: 'side' }));
  }
}
