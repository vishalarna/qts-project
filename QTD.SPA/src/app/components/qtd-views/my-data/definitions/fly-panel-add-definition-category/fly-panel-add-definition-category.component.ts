import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-add-definition-category',
  templateUrl: './fly-panel-add-definition-category.component.html',
  styleUrls: ['./fly-panel-add-definition-category.component.scss']
})
export class FlyPanelAddDefinitionCategoryComponent implements OnInit {
  @Input() mode : "Add" | "Edit" | "Copy" = "Add"
  isCopy: false;
  isEdit = false;
  showSpinner = false;
  AddAnotherInstructorCategory: boolean = false;
  dateError = false;
  insCategoryTitle: any;
  instructorCategoryForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  iADescription = "";
  iaNote = "";

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
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
      this.readyinstructorCategoryForm();
    // }
  }

  ngAfterViewInit(): void {

  }

  closeInstructorCategory() {
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

  readyinstructorCategoryForm() {
    this.instructorCategoryForm = this.fb.group({
      insCategoryTitle: new UntypedFormControl(this.insCategoryTitle, [
        Validators.required,
      ]),
      insCategoryDescription: new UntypedFormControl('', Validators.required),
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      Note: new UntypedFormControl(''),
      AddAnother: new UntypedFormControl(false),
    });
  }

}
