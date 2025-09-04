import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose, sideBarMode } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-enabling-objectives',
  templateUrl: './enabling-objectives.component.html',
  styleUrls: ['./enabling-objectives.component.scss']
})
export class EnablingObjectivesComponent implements OnInit,OnDestroy {
  url : string = 'My Data / Enabling Objectives'
  constructor(private store: Store<{ toggle: string }>,) { }

  ngOnInit(): void {
    
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

}
