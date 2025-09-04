import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { isUserLoggedIn } from 'src/app/_Statemanagement/action/state.signIn';
import { EmployeeCertification } from '../../_DtoModels/EmployeeCertification/EmployeeCertification';
import { EmployeePosition } from '../../_DtoModels/EmployeePosition/EmployeePosition';
import { CustomClaimTypes } from '../Utils/CustomClaims';
import { jwtAuthHelper } from '../Utils/jwtauth.helper';

@Injectable({
  providedIn: 'root',
})
export class DataBroadcastService {
  ShowMenuSideBar: BehaviorSubject<any> = new BehaviorSubject(true);
  ToggleMainMenu: Subject<any> = new Subject();
  isUserLoggedIn: BehaviorSubject<any> = new BehaviorSubject(false);

  empFormMode: BehaviorSubject<string> = new BehaviorSubject('edit');
  refreshTblName: Subject<string> = new Subject();
  refreshListName: Subject<string> = new Subject();
  providerSaved:Subject<any> = new Subject();
  topicSaved:Subject<any> = new Subject();
  segmentSaved:Subject<any> = new Subject();
  selectInstance: Subject<any> = new Subject();
  positionsToLink: Subject<any[]> = new Subject();

  isLoggedIn:Observable<boolean>;
  refreshTaskLinks:Subject<any> = new Subject();
  saveTrainingPlan:Subject<any> = new Subject();
  saveTestItem:Subject<any> = new Subject();
  testItemSaved:Subject<any> = new Subject();
  saveMatchColumn:Subject<any> = new Subject();
  matchColumnSaved:Subject<any> = new Subject();
  saveTrueFalse:Subject<any> = new Subject();
  trueFalseSaved:Subject<any> = new Subject();
  saveShortAnswer:Subject<any> = new Subject();
  shortAnswerSaved:Subject<any> = new Subject();
  fillBlankSaved:Subject<any> = new Subject();
  saveFillBlank:Subject<any> = new Subject();

  updateQuestion:Subject<any> = new Subject();
  updateAnswerList:Subject<any> = new Subject();

  addSingleQuestion:Subject<any> = new Subject();
  addMultipleQuestions:Subject<any> = new Subject();
  updateSegments:Subject<any> = new Subject();

  // Subjects for my-data component
  updateMyDataNavBar:Subject<any> = new Subject();
  updateProcTaskLink: Subject<any> = new Subject();
  updatePosTaskLink: Subject<any> = new Subject();
  updateProcRRLink: Subject<any> = new Subject();
  updateProcILALink: Subject<any> = new Subject();
  updateProcEOLink: Subject<any> = new Subject();
  updateProcSHLink: Subject<any> = new Subject();
  updateProcedureInNavBar:Subject<any> = new Subject();
  updateProcIAData:Subject<any> = new Subject();
  refreshProcedureData:Subject<any> = new Subject();
  refreshPositionData:Subject<any> = new Subject();
  updateRRIA:Subject<any> = new Subject();
  updateRR:Subject<any> = new Subject();
  updatePosition: Subject<any> = new Subject();
  refreshOverviewData:Subject<any> = new Subject();
  refreshTaskStats:Subject<any> = new Subject();
  refreshPositionStats:Subject<any> = new Subject();
  updateSHRRLink: Subject<any> = new Subject();
  updatePositionInNavBar:Subject<any> = new Subject();
  refreshStats : Subject<any> = new Subject();
  navigateOnChange : Subject<any> = new Subject();
  questionSaved : Subject<any> = new Subject();
  refreshMeta : Subject<any> = new Subject();
  eoDeleted: Subject<any> = new Subject();

  // Test Item Subjects
  saveQuestion: Subject<any> = new Subject();
  refreshTestItem: Subject<any> = new Subject();
  getTestItem:Subject<any> = new Subject();

  filterByEmp:Subject<any> = new Subject();
  refreshTQStats:Subject<any> = new Subject();

  updateTime:Subject<any> = new Subject();

  refreshRosterData:Subject<any> = new Subject();
  refreshProcedureReviewData:Subject<any> = new Subject();
  refreshSideNav:Subject<any> = new Subject();

  refreshEvalQualification:Subject<any> = new Subject();

  qtdMenuItemSelected:Subject<any> = new Subject();

  publicClassEnabled: BehaviorSubject<boolean> = new BehaviorSubject(false);
  ///// Special for Test Copy mode
  insertQuestionWhileCopying:BehaviorSubject<any> = new BehaviorSubject([]);


  constructor(private store:Store<{data:boolean}>) {
    this.isLoggedIn = store.select('data')

  }


  UserLoggedIn() {

    let jwt = jwtAuthHelper.unPackJWTToken;
    if (
      jwtAuthHelper.ValidAuthToken !== '' &&
      jwtAuthHelper.isRefreshTokenValid
      // &&
      // !localStorage.getItem('2FAUsr') &&
      // !jwt[CustomClaimTypes.TfaRequired]
    ) {
      this.isUserLoggedIn.next(true);
      this.store.dispatch(isUserLoggedIn({data:true})) //use this instead of detecting changes in broadcast service above
      return true;
    } else {
      this.isUserLoggedIn.next(false);
      this.store.dispatch(isUserLoggedIn({data:false}))
      return false;
    }
  }
}
