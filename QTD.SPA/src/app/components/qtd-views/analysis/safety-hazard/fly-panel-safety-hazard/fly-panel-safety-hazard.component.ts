import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { SaftyHazardCreateOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCreateOptions';
import { SaftyHazardOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardOptions';
import { SaftyHazardUpdateOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardUpdateOptions';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { LinkSaftyHazardOptions } from 'src/app/_DtoModels/Task/LinkSaftyHazardOptions';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { SafetyHazardCategoryService } from 'src/app/_Services/QTD/safety-hazard-category.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-safety-hazard',
  templateUrl: './fly-panel-safety-hazard.component.html',
  styleUrls: ['./fly-panel-safety-hazard.component.scss'],
})
export class FlyPanelSafetyHazardComponent implements OnInit {
  AddEditSHMenu: boolean = false;
  SHMenuMode: string = 'add';

  shToLink: any[] = [];
  shToUnLink: any[] = [];

  SHwithCategories: SaftyHazard_Category[] = [];
  tempSHwithCategories: SaftyHazard_Category[] = [];

  shID: any;
  shObj: SaftyHazard = new SaftyHazard();

  @Input() taskId: any;
  @Output()
  TaskLinked = new EventEmitter<Event>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,

    private translate: TranslateService,
    private alert: SweetAlertService,
    private saftyHazardSrvc: SafetyHazardsService,
    private saftyHazardCatSrvc: SafetyHazardCategoryService,
    private taskService: TasksService,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.getSHWithCategories();
  }

  //filtering for safety hazards and SH Categories
  filterSaftyHazard(filter: any) {
    let f = (filter.target as HTMLInputElement).value;
    this.tempSHwithCategories = [
      ...JSON.parse(JSON.stringify(this.SHwithCategories)),
    ];

    if (f != '') {
      this.tempSHwithCategories.map((i) => {
        if (i.description.toLowerCase().includes(f.toLowerCase())) {
          i = i;
        } else {
          i.saftyHazards = i.saftyHazards.filter((c) =>
            c.title.toLowerCase().includes(f.toLowerCase())
          );
        } //inner if else
      });
    } // outer if
  }
  openSafetyHazardPanel(mode: string, editData?: SaftyHazard) {
    this.AddEditSHMenu = true;
    this.SHMenuMode = mode;
    this.shObj = new SaftyHazard();
    this.shObj.saftyHazardCategoryId = 0;
    if (editData) {
      Object.assign(this.shObj, editData);
      this.shID = this.shObj.id;
    }
    if (!this.SHwithCategories) this.getSHWithCategories();
  }

  async LinkSaftyHazardToTask() {
    let opt: TaskOptions = new TaskOptions();
    opt.safetyHazardIds = this.shToLink;
    opt.isSignificant = false;
    opt.taskIds.push(this.taskId);
    await this.taskService.LinkSaftyHazards(this.taskId, opt).then(async (res) => {
      if (res) {
        this.TaskLinked.emit();
        this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.SHLinkedToTask')));
        this.shToLink = [];
      }
    });
  }

  async getSHWithCategories() {
    await this.saftyHazardCatSrvc.getAll().then((res) => {
      this.SHwithCategories = res;
      this.tempSHwithCategories = [...this.SHwithCategories];
    });
  }
  AddToLinkList(id: any) {
    const index = this.shToLink.indexOf(id, 0);
    if (index == -1) this.shToLink.push(id);
    else this.shToLink.splice(index, 1);
  }

  RemoveFromLinkList(id: any) {
    const index = this.shToUnLink.indexOf(id, 0);
    if (index == -1) this.shToUnLink.push(id);
    else this.shToUnLink.splice(index, 1);
  }

  async modifySaftyHazardStatus(item: SaftyHazard, action: string) {
    let opt: SaftyHazardOptions = {
      actionType: action,
      isSignificant: false,
    };
    await this.saftyHazardSrvc.delete(item.id, opt).then((res) => {
      if (res) {
        this.getSHWithCategories();
        this.alert.successToast(this.translate.instant('L.rec' + action));
      }
    });
  }

  async onSubmit() {
    if (this.SHMenuMode == 'add') {
      let safetyHazards = await this.saftyHazardSrvc.getAll();
      let shNumber =
        safetyHazards
          .filter(
            (x) => x.saftyHazardCategoryId == this.shObj.saftyHazardCategoryId
          )
          .pop()?.number ?? 0;
      let opt: SaftyHazardCreateOptions = {
        title: this.shObj.title,
        description: this.shObj.description,
        number: '',
        personalProtectiveEquipment: this.shObj.personalProtectiveEquipment,
        saftyHazardCategoryId: this.shObj.saftyHazardCategoryId,
      };
      await this.saftyHazardSrvc.create(opt).then((res) => {
        if (res) {
          this.getSHWithCategories();
          this.alert.successToast(this.translate.instant('L.recAdded'));
          this.AddEditSHMenu = false;
          this.shObj = new SaftyHazard();
          this.shObj.saftyHazardCategoryId = 1;
        }
      });
    } else {
      let opt: SaftyHazardUpdateOptions = {};
      await this.saftyHazardSrvc.update(this.shID, opt).then((res) => {
        if (res) {
          this.getSHWithCategories();
          this.alert.successToast(this.translate.instant('L.recUpdated'));
          this.AddEditSHMenu = false;
          this.shObj = new SaftyHazard();
          this.shObj.saftyHazardCategoryId = 1;
        }
      });
    }
  }
}
