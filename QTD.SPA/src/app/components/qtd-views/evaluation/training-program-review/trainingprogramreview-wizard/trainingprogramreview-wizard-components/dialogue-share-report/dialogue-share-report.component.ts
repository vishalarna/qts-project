import { Component, Input, OnInit, Optional } from '@angular/core';
import { MatLegacyDialog as MatDialog, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ReportExportOptions, ReportExportType } from 'src/app/_DtoModels/Report/ReportExportOptions';
import { ReportSendOptions } from 'src/app/_DtoModels/Report/ReportSendOptions';

@Component({
  selector: 'app-dialogue-share-report',
  templateUrl: './dialogue-share-report.component.html',
  styleUrls: ['./dialogue-share-report.component.scss']
})
export class DialogueShareReportComponent implements OnInit {

  public Editor = ckcustomBuild;
  public configCKEditor = {
    toolbar: ["|","heading","bold","italic","strikethrough","underline","link","|","outdent","indent","bulletedList","numberedList","|","insertTable","undo","redo"],
  };
  isInvalidEmails: boolean = false;
  shareDialogueForm: UntypedFormGroup;
  reportsendOptions:ReportSendOptions;
  @Input() inputReportSkeletonId: string;
  @Input() inputHeader: string;
  @Input() inputTosLabel: string;
  @Input() inputOptionalTextLabel: string;
  @Input() handleLoad: (e) => void;
  @Input() handleXClick: (e) => void;
  @Input() handleTosChange: (e) => void;
  @Input() handleExportTypeClick: (e) => void;
  @Input() handleOptionalTextChange: (e) => void;
  @Input() handleShareClick: (e) => void;

  constructor( 
    @Optional() public mdDialogRef: MatDialogRef<DialogueShareReportComponent>,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private fb: UntypedFormBuilder) { }

  ngOnInit(): void {
    this.load();
  }

  _handleLoad(e){
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.load();
    }
  }
  
  _handleXClick(e){
    if (this.handleXClick &&typeof this.handleXClick === 'function') {
      this.xClick();
    }
  }

  _handleTosChange(e){
    if (this.handleTosChange &&typeof this.handleTosChange === 'function') {
      this.tosChange();
    }
  }

  _handleExportTypeClick(e){
    if (this.handleExportTypeClick &&typeof this.handleExportTypeClick === 'function') {
      this.exportTypeClick();
    }
  }

  _handleOptionalTextChange(e){
    if (this.handleOptionalTextChange &&typeof this.handleOptionalTextChange === 'function') {
      this.optionalTextChange();
    }
  }

  _handleShareClick(e){
    if (this.handleShareClick &&typeof this.handleShareClick === 'function') {
      this.shareClick();
    }
  }

  load(){
    this.shareDialogueForm = this.fb.group({
      tos: new UntypedFormControl(this.inputTosLabel, [Validators.required]),
      exportType: new UntypedFormControl(ReportExportType.Pdf, [Validators.required]),
      optionalText: new UntypedFormControl(null)
    });
    this.reportsendOptions = new ReportSendOptions();
  }


  xClick(){
    this.dialog.closeAll();
  }

  tosChange(){
    const emailRegex = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
    const emails = this.shareDialogueForm.get("tos").value.split(',').map((email) => email.trim());
    this.isInvalidEmails = emails.some((email) => !emailRegex.test(email));
  }

  exportTypeClick(){

  }

  optionalTextChange(){

  }

  shareClick(){
    const emailAddresses = this.shareDialogueForm.get("tos").value.split(',').map((email) => email.trim());
    this.reportsendOptions.getExportType(ReportExportType.Pdf);
    Array.from(emailAddresses).forEach((element) => {
      // this.reportsendOptions.getTos(element);
    });
    
  }

}
