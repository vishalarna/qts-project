import { createReducer,on } from "@ngrx/store";
import * as actions from '../action/state.signIn'

export const isLoggedIn = false;

export const loggedInReducer = createReducer(
  isLoggedIn,
  on(actions.isUserLoggedIn,(state,data)=>{
    if(data.data){
      
      return true
    }
    else{
      
      return false
    }
  })
)
