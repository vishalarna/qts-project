import { Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { AuthMessageCreateOptions } from '@models/AdminMessageAuth/AuthMessageCreateOptions';
import { AuthMessageVM } from '@models/AdminMessageAuth/AuthMessageVM';
import { AuthAdminMesaageService } from 'src/app/_Services/Auth/auth-admin-message-service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';

@Component({
  selector: 'app-auth-admin-messages',
  templateUrl: './auth-admin-messages.component.html',
  styleUrls: ['./auth-admin-messages.component.scss']
})
export class AuthAdminMessagesComponent implements OnInit {
  @ViewChild('adminMessageSort') set adminMessageSort(sorting: MatSort) {
       if (sorting) this.adminMessageDataSource.sort = sorting;
     }
  messageFormGroup: UntypedFormGroup;
  instanceName: string;
  authAdminMessages: AuthMessageVM[] = [];
  adminMessageDataSource =  new MatTableDataSource<AuthMessageVM>([]);
  columnsToDisplay = ['message', 'instance', 'received', 'receivedDate', 'expirationDate']
  constructor(private fb: UntypedFormBuilder, private authAdminMesaageService:AuthAdminMesaageService, private alert: SweetAlertService, ) { }

  ngOnInit(): void {
    this.initializeMessageForm();
    this.readyMessagesAsync();
  }
  initializeMessageForm(){
    this.messageFormGroup = this.fb.group({
      messageToUsers: ['', [Validators.required]],
      expiryDate: ['', [Validators.required]]
    })
  }
  async onSubmit(){
    let messageCreateOptions: AuthMessageCreateOptions = {
      message: this.messageFormGroup.get('messageToUsers').value,
      expiryDate: this.messageFormGroup.get('expiryDate').value
    }
    await this.authAdminMesaageService.createAdminMessage(messageCreateOptions);
    this.readyMessagesAsync();
    this.alert.successToast('Message has been submitted');
    this.messageFormGroup.reset();
  }

  async readyMessagesAsync(){
    this.authAdminMessages = await this.authAdminMesaageService.getAllAdminMessageAuthAsync();
    this.adminMessageDataSource = new MatTableDataSource(this.authAdminMessages)
  }
}
