import {TemplatePortal} from '@angular/cdk/portal';
import {DatePipe, formatDate} from '@angular/common';
import {Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef} from '@angular/core';
import {UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators} from '@angular/forms';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import {Router} from '@angular/router';
import {EmployeePosition} from 'src/app/_DtoModels/EmployeePosition/EmployeePosition';
import {EmployeePositionCreateOptions} from 'src/app/_DtoModels/EmployeePosition/EmployeePositionCreateOptions';
import {EmployeePositionUpdateOptions} from 'src/app/_DtoModels/EmployeePosition/EmployeePositionUpdateOptions';
import {TrainingProgram} from 'src/app/_DtoModels/TrainingProgram/TrainingProgram';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import {EmployeesService} from 'src/app/_Services/QTD/employees.service';
import {PositionsService} from 'src/app/_Services/QTD/positions.service';
import {TrainingProgramsService} from 'src/app/_Services/QTD/training-programs.service';
import {DataBroadcastService} from 'src/app/_Shared/services/DataBroadcast.service';
import {FlyInPanelService} from 'src/app/_Shared/services/flyInPanel.service';
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-employee-position',
  templateUrl: './fly-panel-add-employee-position.component.html',
  styleUrls: ['./fly-panel-add-employee-position.component.scss']
})
export class FlyPanelAddEmployeePositionComponent implements OnInit {
  @Input() isCopy: any;
  @Input() mode: "Add" | "Edit" = "Add"
  @Input() oldPosId: any;
  @Input() empId: any;
  @Input() employeeForm;
  @Input() empPosEditId;
  isEdit = false;
  showSpinner = false;
  AddAnotherposition: boolean = false;
  dateError = false;
  defCategory: any;
  positionempLinkForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  psoitionList: any[] = [];
  originalPositionData:any[] =[];
  id: any;
  tpVersion: any[] = [];
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @ViewChild('posSelect', { static: false }) posSelect!: MatSelect;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private positionService: PositionsService,
    private empSrvc: EmployeesService,
    private dataBroadcastService: DataBroadcastService,
    private route: Router,
    private trainingProgram: TrainingProgramsService,
    private labelPipe: LabelReplacementPipe
    //private proc_issueAuthService: IssuingAuthoritiesService,
    //private alert: SweetAlertService,
    //private dataBroadcastService: DataBroadcastService,
  ) {
  }

  ngOnInit(): void {
    setTimeout(() => {
      this.posSelect._handleKeydown = (event: KeyboardEvent) => {
        if (event.key === 'SPACE')
        return
      };
    });
  
    this.readypositionForm();
    // this.getTrainingProgramVersion();
    if (this.oldPosId != undefined) {

      this.mode = 'Edit';
      // this.getpositonsList();
      // this.readypositionForm();
      this.readypositionFormWithData();

    }
      // if (this.oldIssuingAuthority !== undefined && !this.isCopy) {
      //   this.isEdit = true;
      //   this.readyIssuingAuthorityFormWithData();
      // }
      // else if (this.oldIssuingAuthority && this.isCopy) {
      //   this.isEdit = false;
      //   this.readyIssuingAuthorityFormWithData();
    // }
    else {
      this.isEdit = false;
      //this.psoitionList = ["Category 1","Category 2","Category 3","Category 4","Category 5","Category 6"]
      this.getpositonsList();

    }

  }

  ngAfterViewInit(): void {

  }

  async getTrainingProgramVersion() {
    var tpName = this.positionempLinkForm.get('version')?.value;
    var posId = this.positionempLinkForm.get('position')?.value;
    this.positionempLinkForm.get('version')?.setValue('');
    this.positionempLinkForm.updateValueAndValidity();
    if (posId !== null && posId !== undefined && posId !== "") {
      await this.trainingProgram.getInitialVersionsForPositionIdAsync(posId).then((res: any) => {
        this.tpVersion = res.sort((a, b) => a.tpVersionNo > b.tpVersionNo ? 1 : -1);
        if (this.mode === 'Edit') {
          var data = this.tpVersion.find(x => x.tpVersionNo === tpName);
          if (data != undefined) {
            this.positionempLinkForm.get('version')?.setValue(data?.tpVersionNo ?? "");
            this.positionempLinkForm.updateValueAndValidity();
          }
        }
      });
    }
  }

  closedefinition() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }


  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'MM/dd/yyyy', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  }

  readypositionForm() {
    this.positionempLinkForm = this.fb.group({
      version: new UntypedFormControl(''),
      position: new UntypedFormControl('', Validators.required),
      managaerName: new UntypedFormControl(''),
      Trainee: new UntypedFormControl(false),
      disclaimer: new UntypedFormControl(false),
      startDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), Validators.required),
      posQualtDate: new UntypedFormControl(''),
      endtDate: new UntypedFormControl(''),
      AddAnother: new UntypedFormControl(false),
      certRequired: new UntypedFormControl(false),
      positionTitle: new UntypedFormControl(''),
      searchTxt: new UntypedFormControl(''),
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 
  
  async getpositonsList() {

    await this.positionService
      .getAllWithoutIncludes()
      .then((res: any) => {


        if (res != null) {
          this.psoitionList = res;
          this.originalPositionData = Object.assign(this.psoitionList);
        }
      })
      .catch(async (err: any) => {
        this.alert.errorToast(await this.transformTitle('Position') +' List error');
      });
  }

  async LinkPositiontoEmployee() {

    this.showSpinner = true;
    var options = new EmployeePositionCreateOptions();
    options.startDate = this.positionempLinkForm.get("startDate")?.value;
    options.posQualificationDate = this.positionempLinkForm.get("posQualtDate")?.value;
    options.endDate = this.positionempLinkForm.get("endtDate")?.value;
    options.ManagerName = this.positionempLinkForm.get("managaerName")?.value;
    options.TrainingProgramVersion = this.positionempLinkForm.get("version")?.value;
    options.isTrainee = this.positionempLinkForm.get("Trainee")?.value;
    options.isSignificant = this.positionempLinkForm.get("disclaimer")?.value;
    options.positionId = this.positionempLinkForm.get("position")?.value;
    options.isCertificationRequired = this.positionempLinkForm.get('certRequired')?.value;

    let segments = this.route.url.split('/')

    // if (segments.includes('edit')) {
    //   this.id = segments[segments.length - 1];
    // }
    // else {
    this.id = this.empId;
    // }


    await this.empSrvc.createPosition(this.id, options).then(async (res: any) => {
      // this.saveSHCatHistory(res.id);
      if (this.positionempLinkForm.get('AddAnother')?.value) {
        this.positionempLinkForm.reset();
        this.positionempLinkForm.get('startDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.positionempLinkForm.get('posQualtDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.positionempLinkForm.get('endtDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      } else {
        this.refresh.emit();
        this.closed.emit('fp-add-sh-cat-closed');
      }

      this.alert.successToast(`Successfull Link ` + await this.transformTitle('Position') +`to the ` + await this.transformTitle('Employee'));
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  async readypositionFormWithData() {
    await this.empSrvc.getPositionsByEmpAndPosId(this.empId, this.oldPosId,this.empPosEditId).then((res: EmployeePosition) => {
      this.positionempLinkForm.patchValue({
        version: res.trainingProgramVersion,
        position: res.position.id,
        positionTitle: res.position.positionTitle,
        managaerName: res.managerName,
        Trainee: res.trainee,
        disclaimer: res.isSignificant,
        startDate: this.datePipe.transform((res.startDate), "yyyy-MM-dd"),
        posQualtDate: this.datePipe.transform((res.qualificationDate), "yyyy-MM-dd"),
        endtDate: this.datePipe.transform((res.endDate), "yyyy-MM-dd"),
        certRequired: res.isCertificationNotRequired ?? false,
      });
      this.positionempLinkForm.get('version')?.setValue(res.trainingProgramVersion);
      this.positionempLinkForm.updateValueAndValidity();
      this.getTrainingProgramVersion();
    })

  }

  async UpdateLinkPositiontoEmployee() {

    this.showSpinner = true;
    var options = new EmployeePositionUpdateOptions();
    options.positionId = this.oldPosId;
    options.startDate = this.positionempLinkForm.get("startDate")?.value;
    options.qualificationDate = this.positionempLinkForm.get("posQualtDate")?.value;
    options.endDate = this.positionempLinkForm.get("endtDate")?.value;
    options.managerName = this.positionempLinkForm.get("managaerName")?.value;
    options.trainingProgramVersion = this.positionempLinkForm.get("version")?.value;
    options.trainee = this.positionempLinkForm.get("Trainee")?.value;
    options.isCertificationRequired = this.positionempLinkForm.get('certRequired')?.value;
     options.isSignificant = this.positionempLinkForm.get("disclaimer")?.value;
     options.employeePositionId = this.empPosEditId;
    //options.positionId = this.positionempLinkForm.get("position")?.value;
    // let segments = this.route.url.split('/')
    //
    // if (segments.includes('edit'))
    //  {
    //   this.id = segments[segments.length - 1];
    // }


    await this.empSrvc.updateLinkedPosition(this.empId, options.positionId, options).then(async (res: any) => {
      // this.saveSHCatHistory(res.id);
      if (this.positionempLinkForm.get('AddAnother')?.value) {
        this.positionempLinkForm.reset();
        this.positionempLinkForm.get('startDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.positionempLinkForm.get('posQualtDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.positionempLinkForm.get('endtDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      } else {
        this.refresh.emit();
        this.closed.emit('fp-add-sh-cat-closed');
      }

      this.alert.successToast(`Successfull Link `+ await this.transformTitle('Position') +` to the ` + await this.transformTitle('Employee'));
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  /* selcetedCheckbox(name:string){

    switch(name){
      case 'first':
        this.positionempLinkForm.get('Trainee').setValue(true);
        this.positionempLinkForm.get('disclaimer').setValue(false);
        break;

      case 'second':
        this.positionempLinkForm.get('disclaimer').setValue(true);
        this.positionempLinkForm.get('Trainee').setValue(false);
        break;
    }
  } */

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }
  
  filterData() {
    this.positionempLinkForm.get('searchTxt')?.setValue('');
    this.positionSearch();
  }

  positionSearch() {
      var filterString = this.positionempLinkForm.get('searchTxt')?.value;
      if (filterString !== undefined && filterString !== null) {
        filterString = String(filterString).toLowerCase();
      } else {
        filterString = "";
      }
      this.psoitionList = this.originalPositionData.filter((f) => {
        return f.positionTitle.toLowerCase().includes(filterString);
      });
    }
}



