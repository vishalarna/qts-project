import { DatePipe } from '@angular/common';
import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { EmployeePosition } from 'src/app/_DtoModels/EmployeePosition/EmployeePosition';
import { EmployeePositionCreateOptions } from 'src/app/_DtoModels/EmployeePosition/EmployeePositionCreateOptions';
import { EmployeePositionUpdateOptions } from 'src/app/_DtoModels/EmployeePosition/EmployeePositionUpdateOptions';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-fly-panel-emp-position',
  templateUrl: './fly-panel-emp-position.component.html',
  styleUrls: ['./fly-panel-emp-position.component.scss'],
})
export class FlyPanelEmpPositionComponent implements OnInit {
  processing = false;
  @Input() mode: string = 'add';
  isTrainee = false;
  empId!: any;
  showHistory: boolean = false;
  empPosOpts: EmployeePositionCreateOptions =
    new EmployeePositionCreateOptions();
  positions: Position[] = [];
  empPosUpdateOpts: EmployeePositionUpdateOptions =
    new EmployeePositionUpdateOptions();
  empPos: EmployeePosition = new EmployeePosition();

  @Input() empPosEditData!: EmployeePosition;
  vcf: ViewContainerRef;
  showPosPanel: boolean = false;
  form: UntypedFormGroup;
  tqCheck:boolean=true;

  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private posService: PositionsService,
    private databroadcastService: DataBroadcastService,
    private empSrvc: EmployeesService,
    private route: Router,
    private datepipe: DatePipe,
    public flyPanelSrvc: FlyInPanelService
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
    this.empPos = new EmployeePosition();
    this.empPos.position = new Position();
    this.empPos.position.name = 'select';
    this.empId = this.route.url.substring(this.route.url.lastIndexOf('/') + 1);
  }

  async ngOnInit(): Promise<void> {
    this.ConstructForm();

    await this.getAllPositions().then((_) => {
      if (this.empPosEditData && this.mode == 'edit') {

        Object.assign(this.empPos, this.empPosEditData);

        this.form.get('positionId')?.setValue(this.empPos.positionId);
        this.form
          .get('startDate')
          ?.setValue(
            String(this.datepipe.transform(this.empPos.startDate, 'yyyy-MM-dd'))
          );
        this.form
          .get('endDate')
          ?.setValue(
            String(this.datepipe.transform(this.empPos.endDate, 'yyyy-MM-dd'))
          );
        this.form
          .get('qualificationDate')
          ?.setValue(
            String(
              this.datepipe.transform(
                this.empPos.qualificationDate,
                'yyyy-MM-dd'
              )
            )
          );
      }
    });

    this.databroadcastService.refreshListName.subscribe((res) => {
      if (res === 'positions') this.getAllPositions();
    });
  }

  ConstructForm() {
    this.form = new UntypedFormGroup({
      positionId: new UntypedFormControl(undefined, Validators.required),
      startDate: new UntypedFormControl('', Validators.required),
      endDate: new UntypedFormControl({ value: '' }),
      isTrainee: new UntypedFormControl(false, Validators.required),
      trainingProgram: new UntypedFormControl(''),
      qualificationDate: new UntypedFormControl({ value: '' }),
    });
  }

  checkChanged() {
    this.empPos.trainee = !this.empPos.trainee;
  }

  async getAllPositions() {
    await this.posService.getAllWithoutIncludes().then((data) => {
      this.positions = data;
    });
  }

  async AddEmpPositions() {
    if (this.form.invalid) {
      this.alert.warningAlert('Invalid Data', 'Please select valid values');
      return;
    }
    if (this.mode == 'add') {
      this.empPosOpts = {
        isTrainee: this.form.get('isTrainee')?.value,
        positionId: this.form.get('positionId')?.value,
        startDate: this.form.get('startDate')?.value,
        isSignificant: false,
        posQualificationDate : this.form.get('startDate')?.value,
        endDate: this.form.get('startDate')?.value,
        ManagerName : 'No one',
        TrainingProgramVersion : 'QTD1'

      };
      await this.empSrvc
        .createPosition(this.empId, this.empPosOpts)
        .then((res) => {
          if (res) {
            this.alert.successToast(this.translate.instant('L.recAdded'));
          }
        })
        .finally(() => this.databroadcastService.refreshTblName.next('pos'));
    }
    else {
      
      this.empPosUpdateOpts = {
        employeeId: this.empId,
        endDate:
          this.form.get('endDate')?.value == 'null'
            ? undefined
            : this.form.get('endDate')?.value,
        positionId: this.form.get('positionId')?.value,
        qualificationDate:
          this.form.get('qualificationDate')?.value == 'null'
            ? undefined
            : this.form.get('qualificationDate')?.value,
        startDate: this.form.get('startDate')?.value,
        trainee: this.form.get('isTrainee')?.value,
      };

      await this.empSrvc
        .updatePosition(
          this.empId,
          this.empPos.position.id,
          this.empPosUpdateOpts
        )
        .then((res) => {
          if (res) {
            this.alert.successToast(this.translate.instant('L.recUpdated'));
          }
        })
        .finally(() => this.databroadcastService.refreshTblName.next('pos'));
    }
  }

  togglePosPanel() {
    this.showPosPanel = !this.showPosPanel;
  }

  toggleShowHistory() {
    this.showHistory = !this.showHistory;
  }
}
