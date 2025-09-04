import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Person } from '@models/Person/Person';
import { PersonsService } from 'src/app/_Services/QTD/persons.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-create-person',
  templateUrl: './fly-panel-create-person.component.html',
  styleUrls: ['./fly-panel-create-person.component.scss'],
})
export class FlyPanelCreatePersonComponent implements OnInit {
  personForm: UntypedFormGroup;
  @Output() closedFlypanel = new EventEmitter<boolean>();
  @Output() newUser = new EventEmitter<Person>();
  constructor(
    private fb: UntypedFormBuilder,
    private personService: PersonsService,
    private alert: SweetAlertService
  ) {}

  ngOnInit(): void {
    this.initializedPersonForm();
  }

  closePersonFlyPanel() {
    this.closedFlypanel.emit(false);
  }

  initializedPersonForm() {
    this.personForm = this.fb.group({
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
    });
  }

  async createPerson() {
    var createdPerson = await this.personService.create(this.personForm.value);
    this.alert.successToast('Person created successfully');
    this.newUser.emit(createdPerson);
    this.initializedPersonForm();
    this.closePersonFlyPanel();
  }
}
