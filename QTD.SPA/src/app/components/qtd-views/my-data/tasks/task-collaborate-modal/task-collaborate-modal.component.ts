import { formatDate } from '@angular/common';
import { Component, Inject, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { Person } from 'src/app/_DtoModels/Person/Person';
import { Task_CollaboratorInvitaiton } from 'src/app/_DtoModels/Task_CollaboratorInvitaion/Task_CollaboratorInvitaiton';
import { TaskCollaboratorInvitationOptions } from 'src/app/_DtoModels/Task_CollaboratorInvitaion/Task_CollaboratorInvitationOptions';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-collaborate-modal',
  templateUrl: './task-collaborate-modal.component.html',
  styleUrls: ['./task-collaborate-modal.component.scss']
})
export class TaskCollaborateModalComponent implements OnInit,AfterViewInit,OnDestroy {
  displayedColumns: string[] = ['name', 'email', 'userPermissions', 'actions'];
  dataSource = new MatTableDataSource<any>();
  public Editor = ckcustomBuild;
  employees: Employee[] = [];
  permissions: any[] = [{ key: 0 }, { key: 1 }];
  selectedValue = 0;
  emailField = "";
  hasEmailError = false;
  initial : Person = new Person();
  message = "";
  startLoading = false;

  previousCollaborators : Task_CollaboratorInvitaiton[] = [];

  emailRegex = /[A-Za-z0-9._%-]+@[A-Za-z0-9._%-]+\.[a-z]{2,3}/

  personDataSource: Person[] = [];

  collaborateForm = new UntypedFormGroup({});

  collaboratorList: any[] = [];
  taskId = "";
  subscription = new SubSink();

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private empolyeeService: EmployeesService,
    private route : ActivatedRoute,
    private taskService : TasksService,
    private alert : SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.startLoading = true;
    this.readyEmployeeData();
  }

  ngAfterViewInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyEmployeeData() {
    this.personDataSource = [];
    this.employees = await this.empolyeeService.getAll();
    this.previousCollaborators = await this.taskService.getCollaborators(this.data);
    var ids = this.previousCollaborators.map((data)=> data.inviteeEId);
    this.employees.forEach((data,i)=>{
      if(ids.includes(data.id)){
        this.personDataSource.push(data.person);
      }
    });

    this.previousCollaborators.forEach((data)=>{

      if(data.inviteeEId === null){
        var person = new Person();
        person.firstName = data.inviteeEmail;
        person.lastName = "";
        person.username = data.inviteeEmail;
        person.id = data.inviteeEId ?? "";
        this.personDataSource.push(person);
      }
    })
    this.dataSource.data = this.personDataSource;



    this.startLoading = false;
  }

  onSelectionChanged(event: any) {
    this.personDataSource.push(event.value);
    this.dataSource.data = this.personDataSource;

    this.initial = new Person();
  }
  onPermissionSelectionChanged(event: any, id: any) {
  }

  deletePerson(row: any) {
    this.personDataSource = this.personDataSource.filter((data: Person) => {
      return data.username !== row.username && data.firstName !== row.firstName;
    });
    this.dataSource.data = this.personDataSource;
  }

  inputChanged(event: any) {
    this.hasEmailError = !this.separateAndValidateEmail();
    var email = this.emailField.split(',').filter((element) => element);
    if(event[event.length-1] === "," && !this.hasEmailError){

      email.forEach((data)=>{
        var person = new Person();
        person.username = data;
        person.firstName = data;
        person.lastName = "";
        person.id = ""; // Ml = 0
        this.personDataSource.push(person);
        this.personDataSource = [...new Set(this.personDataSource)];
        this.dataSource.data = this.personDataSource;
      });
      this.emailField = "";
    }
  }

  getData() {
    
    var options:TaskCollaboratorInvitationOptions = new TaskCollaboratorInvitationOptions();
    options.invitedByEId = null;
    options.invitedForTaskId = this.data;
    options.inviteDate = formatDate(Date.now(), 'yyyy-MM-dd', 'en');
    options.inviteeEIds = [];
    options.inviteeEmails = [];
    options.message = this.message;
    this.personDataSource.forEach((person)=>{
      options.inviteeEIds.push(person.id);
      options.inviteeEmails.push(person.username);
    });

    this.taskService.CreateCollaborators(options).then((_)=>{
      this.alert.successToast("Collaborators Invited");
    }).finally(()=>{

    });
  }

  separateAndValidateEmail() {
    var email = this.emailField.split(',').filter((element) => element);

    for (var i = 0; i < email.length; i++) {
      if (email[i].length > 0 && !email[i].match(this.emailRegex)) {
        return false;
      }
    }
    return true;
  }
}

class personData {
  name?: string;
  email?: string;
}
