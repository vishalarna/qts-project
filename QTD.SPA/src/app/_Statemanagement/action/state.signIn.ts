import { Action, createAction, props } from '@ngrx/store';

export const isUserLoggedIn = createAction("[LOGIN] isLoggedIn",props<{data:boolean}>())
