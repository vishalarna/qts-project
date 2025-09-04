import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeeWithPositionVM } from 'src/app/_DtoModels/Employee/EmployeeWithPositionVM';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose, sideBarMode } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-idp',
  templateUrl: './idp.component.html',
  styleUrls: ['./idp.component.scss']
})
export class IDPComponent implements OnInit,OnDestroy {
  empWithPos!: EmployeeWithPositionVM;
  empId = "";
  subscription = new SubSink();
  placeHolder = '/assets/img/ImageNotFound.jpg';
  employeeName:any;

  constructor(
    private empService: EmployeesService,
    private route : ActivatedRoute,
    private store: Store<{ toggle: string }>,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.empId = res.id;
      this.readyData();
    })
  }
  ngAfterViewInit(): void {
    // this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    // this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    // this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    // this.store.dispatch(sideBarMode({ mode: 'over' }));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: true }));
    this.store.dispatch(sideBarMode({ mode: 'side' }));
  }

  async readyData(){
    this.empWithPos = await this.empService.getEmployeeWithPosition(this.empId);

    this.employeeName = this.empWithPos.employee.person.firstName + " " + this.empWithPos.employee.person.lastName;

  }
  async goBack() {
    this.router.navigate(['implementation/employees']);
  }
}
