import { createReducer, on } from "@ngrx/store";
import * as actions from '../action/state.componentcommunication'

const data:any = {};
const del:any = [];
const evalData:any = {};
const navigateOptions:any = {};

export const saveReducer = createReducer(
  data,
  on(actions.saveILA,(state,{saveData,tabIndex,update})=>{
    
    return {saveData:saveData,tabIndex:tabIndex,update:update};
  })
)

export const deleteReducer = createReducer(
  del,
  on(actions.deleteILA,(state,{tabIndex})=>{
    
    return {tabIndex:tabIndex};
  })
)

export const getTestInfoReducer = createReducer(
  data,
  on(actions.getTestInfo,(state,{saveData,tabIndex,update})=>{
    
    return {saveData:saveData,tabIndex:tabIndex,update:update};
  })
)

export const deleteTrainingReducer = createReducer(
  del,
  on(actions.deleteTraining,(state,{tabIndex})=>{
    
    return {tabIndex:tabIndex};
  })
)

export const evalationInformationReducer = createReducer(
  evalData,
  on(actions.evalationInformation,(state,{evalData})=>{
    // 
    return {evalData};
  })
)

export const navigateTQReducer = createReducer(
  navigateOptions,
  on(actions.navigateTQ,(state,{shouldNavigate,filterOptions})=>{
    return {shouldNavigate:shouldNavigate,options:filterOptions};
  })
)
