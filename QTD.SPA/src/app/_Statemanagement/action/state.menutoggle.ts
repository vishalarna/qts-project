import { Action, createAction, props } from '@ngrx/store';

export const sideBarOpen = createAction('[MENU TOGGLE] Open');
export const sideBarClose = createAction('[MENU TOGGLE] Close');
export const sideBarToggle = createAction('[MENU TOGGLE] Toggle');
export const freezeMenu = createAction('[MENU TOGGLE] Freeze', props<{doFreeze:boolean}>());
export const sideBarBackDrop = createAction(
  '[MAIN MENU] BACKDROP',
  props<{ backdrop: boolean }>()
);
export const sideBarDisableClose = createAction(
  '[MAIN MENU] DISABLE BG CLICK CLOSE',
  props<{ disableClose: boolean }>()
);

export const sideBarMode = createAction(
  '[MAIN MENU] MODE',
  props<{ mode: 'over' | 'side' }>()
);
