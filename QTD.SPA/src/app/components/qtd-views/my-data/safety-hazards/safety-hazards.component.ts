import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import {
  sideBarBackDrop,
  sideBarDisableClose,
  sideBarMode,
  sideBarClose,
  freezeMenu,
} from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-safety-hazards',
  templateUrl: './safety-hazards.component.html',
  styleUrls: ['./safety-hazards.component.scss'],
})
export class SafetyHazardsComponent implements OnInit {
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
