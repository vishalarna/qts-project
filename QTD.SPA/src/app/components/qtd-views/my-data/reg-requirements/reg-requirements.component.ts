import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import {
  sideBarBackDrop,
  sideBarDisableClose,
  sideBarMode,
  sideBarClose,
  freezeMenu,
} from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-reg-requirements',
  templateUrl: './reg-requirements.component.html',
  styleUrls: ['./reg-requirements.component.scss'],
})
export class RegRequirementsComponent implements OnInit, OnDestroy {
  constructor(private store: Store<{ toggle: string }>) {}
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
}
