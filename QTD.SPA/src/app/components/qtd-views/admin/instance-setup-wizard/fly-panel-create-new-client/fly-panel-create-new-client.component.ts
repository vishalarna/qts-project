import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { CreateClientOption } from '@models/Client/CreateClientOption';
import { ClientService } from 'src/app/_Services/Auth/client.service';

@Component({
  selector: 'app-fly-panel-create-new-client',
  templateUrl: './fly-panel-create-new-client.component.html',
  styleUrls: ['./fly-panel-create-new-client.component.scss']
})
export class FlyPanelCreateNewClientComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() newClientDetail: EventEmitter<any> = new EventEmitter<any>();
  clientForm: UntypedFormGroup;
  createClientOption : CreateClientOption = new CreateClientOption();
  loader: boolean = false;
  constructor( private formBuilder: UntypedFormBuilder,
    private clientService: ClientService,) { }

  ngOnInit(): void {
    this.initializeClientForm();
  }

  initializeClientForm() {
    this.clientForm = this.formBuilder.group({
      clientId: [null], 
    });
  }

 async createClientAsync(){
  this.createClientOption.name  = this.clientForm.get('clientId').value;
  this.clientService.createClient(this.createClientOption).then((res) => {
    this.newClientDetail.emit(res);
     });
    this.closed.emit();
  }

}
