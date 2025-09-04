import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-definition',
  templateUrl: './fly-panel-add-definition.component.html',
  styleUrls: ['./fly-panel-add-definition.component.scss']
})
export class FlyPanelAddDefinitionComponent implements OnInit {
  @Input() isCopy: any;
  @Input() mode : "Add" | "Edit" | "Copy" = "Add"
  isEdit = false;
  showSpinner = false;
  AddAnotherdefinitionCategory: boolean = false;
  dateError = false;
  defCategory: any;
  definitionForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  iADescription = "";
  iaNote = "";
  definitionCategoryList : any;
  fileUploaded = false;
  fileName = "";
  fileData = "";

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService
    //private proc_issueAuthService: IssuingAuthoritiesService,
    //private alert: SweetAlertService,
    //private dataBroadcastService: DataBroadcastService,
  ) { }

  ngOnInit(): void {
    // if (this.oldIssuingAuthority !== undefined && !this.isCopy) {
    //   this.isEdit = true;
    //   this.readyIssuingAuthorityFormWithData();
    // }
    // else if (this.oldIssuingAuthority && this.isCopy) {
    //   this.isEdit = false;
    //   this.readyIssuingAuthorityFormWithData();
    // }
    // else {
      this.isEdit = false;
      this.definitionCategoryList = ["Category 1","Category 2","Category 3","Category 4","Category 5","Category 6"]
      this.readydefinitionCategoryForm();
    // }
  }

  ngAfterViewInit(): void {

  }

  closedefinition() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }

  
  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  }

  readydefinitionCategoryForm() {
    this.definitionForm = this.fb.group({
      defCategoryTitle: new UntypedFormControl(this.defCategory, [
        Validators.required,
      ]),
      defNumber: new UntypedFormControl('', Validators.required),
      defName: new UntypedFormControl('', Validators.required),
      defEmail: new UntypedFormControl(''),
      defCategory : new UntypedFormControl('', Validators.required),
      defDescription: new UntypedFormControl(''),
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      

    });
  }
  fileChange(file: any) {
    
    if (!file[0].type.toLowerCase().includes("application/pdf")) {
      this.alert.errorToast("Please Upload a valid pdf file");
      return;
    }
  }
  removeFile() {
    this.fileName = "";
    this.fileData = "";
    this.fileUploaded = false;
  }
}
