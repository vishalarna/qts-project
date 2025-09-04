import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-fly-panel-detail-self-registration',
  templateUrl: './fly-panel-detail-self-registration.component.html',
  styleUrls: ['./fly-panel-detail-self-registration.component.scss'],
  animations: [
    trigger('slideToggle', [
      state('closed', style({
        height: '0',
        overflow: 'hidden',
        opacity: 0
      })),
      state('open', style({
        height: '*',
        opacity: 1
      })),
      transition('closed <=> open', [
        animate('300ms ease-in-out')
      ])
    ])
  ]
})
export class FlyPanelDetailSelfRegistrationComponent implements OnInit {

  displayedColumns: string[] = ['type', 'number', 'description'];
  objectives = new MatTableDataSource<any>();

  displayedProcedureColumns: string[] = ['number', 'description'];
  procedures = new MatTableDataSource<any>();
  classId:any;
  iLaId:any;
  courseDetail:any;
  isHourDetailCollapsed:boolean = false;
  loader:boolean = false;
  constructor( private store: Store<{ toggle: string }>,
    private _router: Router, private _empService: EmployeesService,

    private route: ActivatedRoute, private alert: SweetAlertService,) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen())
    this.route.params.subscribe((params: any) => {
      if (params.hasOwnProperty('classId') || params.hasOwnProperty('ilaId')) {
        this.classId = params['classId'];
        this.iLaId = params['ilaId'];

        if (params['classId']) {
          this.getDetail()
        }
      }
    });
  }
  async goBack() {
    this._router.navigate(['emp/self-registration'])
  }


  async getDetail() {
    this.loader = true;
    await this._empService
      .getClassDetail(this.classId, this.iLaId)
      .then((res) => {
        if (res) {
          this.courseDetail=res;
          this.procedures.data= this.courseDetail.procedureDataVM;
          this.objectives.data= this.courseDetail.taskDataVM.concat(this.courseDetail.enablingObjectiveDataVM);;
          this.loader = false;
        }
      }).catch((res: any) => {
        this.alert.errorToast(res);
      }).finally(()=>{
        this.loader = false;
      });;
  }
  copyMessage(val: string){
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = val;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
    this.alert.successToast(`webinar link is copied!`,true);
  }

  toggleDetails(){
    this.isHourDetailCollapsed = !this.isHourDetailCollapsed
  }

}
