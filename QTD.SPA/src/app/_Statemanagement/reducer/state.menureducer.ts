import { createReducer, on } from '@ngrx/store';
import * as Actions from '../action/state.menutoggle';

export const toggleState = 'open';
export const menubackdropState = false;
export const menubgDisableCloseState = true;
export const freezeState = false;

//matSideNav:MatSidenav
export const toggleReducer = createReducer(
  toggleState,
  on(Actions.sideBarOpen, (state) => {
    //
    return 'open';
  }),
  on(Actions.sideBarClose, (state) => {
    return 'close';
  }),
  on(Actions.sideBarToggle, (state) => {
    if (state === 'open') return 'close';
    return 'open';
  })
);

export const freezeMenuReducer = createReducer(
  freezeState,
  on(Actions.freezeMenu,((state,{doFreeze})=>{
    // if(hasBackDrop){
    //   return false;
    // }
    // else{
    //   return doFreeze;
    // }
    return doFreeze
  }))
);

export const backdropReducer = createReducer(
  menubackdropState,
  on(Actions.sideBarBackDrop, (state, { backdrop }) => {
    return backdrop;
  })
);
export const bgdisableCloseReducer = createReducer(
  menubgDisableCloseState,
  on(Actions.sideBarDisableClose, (state, { disableClose }) => {

    return disableClose;
  })
);

export const sideBarMenuModeReducer = createReducer(
  'side',
  on(Actions.sideBarMode, (state, { mode }) => {
    return mode;
  })
);
