import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe, formatDate } from '@angular/common';
import {
  Component,
  OnInit,
  Output,
  Input,
  EventEmitter,
  OnDestroy,
  AfterViewInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { TemplatePortal } from '@angular/cdk/portal';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { Procedure_Task_LinkOptions } from 'src/app/_DtoModels/Procedure_Task_Link/Procedure_Task_LinkOptions';
import { SubdutyArea } from 'src/app/_DtoModels/SubdutyArea/SubdutyArea';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskCreateOptions } from 'src/app/_DtoModels/Task/TaskCreateOptions';
import { TaskNumberVM } from 'src/app/_DtoModels/Task/TaskNumberVM';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { TaskUpdateOptions } from 'src/app/_DtoModels/Task/TaskUpdateOptions';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskCopyOptions } from 'src/app/_DtoModels/Task/TaskCopyOptions';

@Component({
  selector: 'app-flypanel-add-task',
  templateUrl: './flypanel-add-task.component.html',
  styleUrls: ['./flypanel-add-task.component.scss'],
})
export class FlypanelAddTaskComponent
  implements OnInit, OnDestroy, AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() oldTask: Task;
  @Input() isCopy: boolean = false;
  @Input() isMetaCheck: any;
  @Input() checkChange: boolean = false;
  @Input() shouldNavigate: boolean = false;
  @ViewChild('stepper') stepper: MatStepper;

  taskNumber = new TaskNumberVM();
  showSpinner: boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  addTask: boolean = true;
  addDutyArea: boolean = false;
  addSubdutyArea: boolean = false;
  addPosition: boolean = false;
  procId = '';
  newDutyAreaNumber: any;
  dutyAreas: DutyArea[] = [];
  subDutyAreas: SubdutyArea[] = [];
  positions: Position[] = [];
  positionList: any[] = [];
  step1Form: UntypedFormGroup;
  step2Form: UntypedFormGroup;
  step3Form: UntypedFormGroup;
  datePipe = new DatePipe('en-us');
  /*   dateError = false; */
  sdaPlaceholder = 'Select Sub Duty Area';
  symbol = '';

  subscription = new SubSink();
  positionsControl = new UntypedFormControl([]);
  constructor(
    private daSrvc: DutyAreaService,
    private fb: UntypedFormBuilder,
    private positionService: PositionsService,
    public breakpointObserver: BreakpointObserver,
    public taskService: TasksService,
    public alert: SweetAlertService,
    public route: ActivatedRoute,
    public procService: ProceduresService,
    public dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private labelPipe: LabelReplacementPipe
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.readyStep1Form();
    this.readyStep2Form();
    this.readyStep3Form();
    this.getallDutyAreas().then(() => {
      if (this.oldTask) {


        this.daSrvc.getSubDutyArea(this.oldTask.subdutyAreaId).then((sda) => {

          this.step1Form.patchValue({
            dutyAreaId: sda.dutyAreaId,
            subdutyAreaId: sda.id
          });
          this.getSubDutyAreas(sda.dutyAreaId);
          this.step2Form.patchValue({
            taskNumber: this.oldTask.number,
            taskDesc: this.oldTask.description,
            isMeta: this.oldTask.isMeta,
            isReliability: this.oldTask.isReliability,
          });
          this.step3Form.patchValue({
            effectiveDate: this.datePipe.transform(Date.now(), 'yyyy-MM-dd'),
            reason: '',
          });

          var numbers = new TaskNumberVM();
          numbers.daNumber = sda.dutyArea.number;
          numbers.letter = sda.dutyArea.letter;
          numbers.sdaNumber = sda.subNumber;
          numbers.taskNumber = this.oldTask.number;
          this.taskNumber = numbers;
        });
      }
    });
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; // Validation passes
    } else {
      return { whitespaceOnly: true }; // Validation fails
    }
  }

  selectedChanged(event: any) {
    if (event.selectedIndex === 1 && this.positions.length < 1) {
      this.getPositions().then(() => {
        if (this.oldTask) {
          this.taskService.getLinkedpositions(this.oldTask.id).then((res) => {
            
            let posIds: any[] = [];
            res.forEach((p) => {
              var pos = this.positions.find(x => x.id == p.position.id);
              if(pos){
                posIds.push(pos);
              }
            });
            this.positionsControl.setValue(posIds);
          });
        }
      });
    }
  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      dutyAreaId: new UntypedFormControl("", [Validators.required]),
      subdutyAreaId: new UntypedFormControl("", [Validators.required]),
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      taskNumber: new UntypedFormControl('', [Validators.required]),
      taskDesc: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      positions: new UntypedFormControl([]),
      isMeta: new UntypedFormControl(false),
      isReliability: new UntypedFormControl(false),
    });
  }

  readyStep3Form() {
    this.step3Form = this.fb.group({
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      reason: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      addNew: new UntypedFormControl(false),
    });
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */

  async getallDutyAreas() {
    await this.daSrvc.getAll().then((res) => {
      this.dutyAreas = res;
      this.step1Form.patchValue({ dutyAreaId: '', subdutyAreaId: '' });
      this.newDutyAreaNumber = Math.max(...res.map((x) => x.number)) + 1;
    });
  }

  async getSubDutyAreas(id: any) {
    await this.daSrvc.getSubDutyAreasByDutyArea(id).then((res) => {

      this.subDutyAreas = res;
      if (this.subDutyAreas.length < 1) {
        this.sdaPlaceholder = 'No Subduty Areas Found';
      } else {
        this.sdaPlaceholder = 'Select Sub Dutyarea';
        /*  if (this.oldTask) {
            this.step1Form.patchValue({
              subdutyAreaId: this.oldTask.subdutyAreaId,
            });
          }  */
      }
    });
  }

  async getPositions() {
    //for dynamic position dropdown
    await this.positionService
      .getAllWithoutIncludes()
      .then((i) => {
        this.positions = i;

      });
    this.positionList.push(this.positionsControl);
  }

  removePosition(i: any) {
    const pos = this.positionsControl.value as Position[];
    this.removeFirst(pos, i);
    this.positionsControl.setValue(pos);
  }

  OnClick(selected: boolean) {
    if (selected) {
      this.positionList.push(this.positionsControl);
    }
  }

  private removeFirst(array: Position[], toRemove: Position): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  closeDutyAreaPanel() {
    this.addDutyArea = false;
    this.addSubdutyArea = false;
    this.addTask = true;
  }

  openDutyAreaPanel() {
    this.addDutyArea = true;
    this.addSubdutyArea = false;
    this.addTask = false;
  }

  openSubDutyAreaPanel() {
    this.addDutyArea = false;
    this.addSubdutyArea = true;
    this.addTask = false;
  }

  openFlyPanelPosition(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  closePositionPanel() {
    this.addPosition = false;
    this.addTask = true;
  }

  refreshDutyArea() {
    this.refresh.emit();
    this.getallDutyAreas();
  }

  refreshSubDutyArea() {
    this.refresh.emit();
    this.step1Form.reset();
  }

  async setTaskNumber(event: any) {
    await this.taskService
      .getTaskNumberWithLetter(event)
      .then((res: TaskNumberVM) => {
        this.taskNumber = res;

        this.step2Form.get('taskNumber')?.setValue(res.taskNumber + 1);
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Fetching '+ await this.transformTitle('Task') + ' Data');
      });
  }

  saveTask() {
    this.showSpinner = true;
    var options = new TaskCreateOptions();
    options.subdutyAreaId = this.step1Form.get('subdutyAreaId')?.value;
    options.description = this.step2Form.get('taskDesc')?.value;
    options.number = this.step2Form.get('taskNumber')?.value;
    options.references = '';
    options.standards = '';
    options.taskCriteriaUpload = '';
    options.changeNotes = this.step3Form.get('reason')?.value;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    options.isMeta = this.step2Form.get('isMeta')?.value;
    options.isReliability = this.step2Form.get('isReliability')?.value;

    this.taskService
      .create(options)
      .then(async (res: Task) => {
        this.showSpinner = false;
        this.linkPositions(res.id);
        this.alert.successToast(await this.transformTitle('Task') +' Created Successfully');
        this.refresh.emit();
        if (this.step3Form.get('addNew')?.value) {
          this.resetAll();
        }
        else {
          this.closed.emit('fp-add-task-closed');
        }
        //this.dataBroadcastService.updateProcTaskLink.next();
        if (this.shouldNavigate) {
          this.dataBroadcastService.navigateOnChange.next({ type: "TASK", data: res });
        }
        else {
          this.dataBroadcastService.updateMyDataNavBar.next(null);
        }
      }).finally(() => {
        this.showSpinner = false;
      });
  }

  copyTask() {
    this.showSpinner = true;
    var options = new TaskCopyOptions();
    
    options.subdutyAreaId = this.step1Form.get('subdutyAreaId')?.value;
    options.positionIds = (this.positionsControl.value || []).map((p: any) => p.id);
    if (
      this.isCopy &&
      this.step2Form.get('taskDesc')?.value == this.oldTask.description
    ) {
      options.description = this.step2Form.get('taskDesc')?.value + '- Copy';
    } else {
      options.description = this.step2Form.get('taskDesc')?.value;
    }
    options.number = this.step2Form.get('taskNumber')?.value;
    options.changeNotes = this.step3Form.get('reason')?.value;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    options.isReliability = this.step2Form.get('isReliability')?.value;


    this.taskService
      .createCopy(this.oldTask.id, options)
      .then(async (res: Task) => {
        this.showSpinner = false;
       // this.linkPositions(res.id);
        this.alert.successToast(await this.transformTitle('Task') + ' Copied Successfully');
        this.refresh.emit();
        //this.dataBroadcastService.updateProcTaskLink.next();
        this.closed.emit('fp-add-task-closed');
        this.dataBroadcastService.navigateOnChange.next({type:"TASK",data:res});
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }

  async linkPositions(id: any) {
    var options = new TaskOptions();
    options.positionIds = this.positionsControl.value?.map(
      (data: any) => data.id
    );
    await this.taskService.Linkpositions(id, options).then((res: any) => {
    });
  }

  compareObjects(object1: any, object2: any) {
    return object1 == object2 && object1.id == object2.id;
  }

  editTask() {
    this.showSpinner = true;
    var options = new TaskUpdateOptions();
    options.subdutyAreaId = this.step1Form.get('subdutyAreaId')?.value;
    options.description = this.step2Form.get('taskDesc')?.value;
    options.number = this.step2Form.get('taskNumber')?.value;
    options.references = '';
    options.standards = '';
    options.taskCriteriaUpload = '';
    options.isMeta = this.step2Form.get('isMeta')?.value;
    options.isReliability = this.step2Form.get('isReliability')?.value;
    options.changeNotes = this.step3Form.get('reason')?.value;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    this.taskService.update(this.oldTask.id, options).then((res: Task) => {
      this.showSpinner = false;
      this.linkPositions(res.id).then(async () => {
        this.alert.successToast(await this.transformTitle('Task') + ' updated Successfully');
        this.refresh.emit();
        this.dataBroadcastService.updateProcTaskLink.next(null);
        this.dataBroadcastService.refreshTaskStats.next(null);
        this.closed.emit('fp-add-task-closed');
        if (this.shouldNavigate) {
          this.dataBroadcastService.navigateOnChange.next({ type: "TASK", data: res });
        }
        else if (this.oldTask.subdutyAreaId !== options.subdutyAreaId) {
          this.dataBroadcastService.navigateOnChange.next({ type: "TASK", data: res });
        }
      });
    }).catch((error) => {
      this.showSpinner = false;
  })
  }

  resetAll() {
    // this.step1Form.reset();
    // this.step2Form.reset();
    // this.step3Form.reset();
    this.readyStep1Form();
    this.readyStep2Form();

    this.step3Form.patchValue({
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      reason: new UntypedFormControl(''),
      addNew: new UntypedFormControl(false),
    })
    this.stepper.reset();
    this.step3Form.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
    this.clearPositions();
  }

  clearPositions() {
    this.positionsControl.setValue([]);
    this.step2Form.get('isReliability')?.setValue(false);
  }
}
