import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjectiveCreateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveCreateOptions';
import { EnablingObjectiveOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveOption';
import { EnablingObjectiveUpdateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveUpdateOptions';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { EnablingObjectivesTopicService } from 'src/app/_Services/QTD/enabling-objectives-topic.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-enabling-objective',
  templateUrl: './fly-panel-enabling-objective.component.html',
  styleUrls: ['./fly-panel-enabling-objective.component.scss'],
})
export class FlyPanelEnablingObjectiveComponent implements OnInit {
  AddEditEOMenu: boolean = false;
  EOSearchStr: string;
  EOMenuMode: string = 'add';

  EOToLink: any[] = [];
  EOToUnLink: any[] = [];
  eo: EnablingObjective = new EnablingObjective();

  EOCategories: EnablingObjective_Category[] = [];
  public tempEOCategories: EnablingObjective_Category[] = [];

  selectedCategoryId: any | undefined;
  selectedSubCategoryId: any | undefined;

  eoCreateOpt: EnablingObjectiveCreateOptions =
    new EnablingObjectiveCreateOptions();
  EOSubCategories: EnablingObjective_SubCategory[] = [];

  EOTopics: EnablingObjective_Topic[] = [];
  eoId!: any;
  taskId!: any;

  constructor(
    private eoSrvc: EnablingObjectivesService,
    private eoCatSrvc: EnablingObjectivesCategoryService,
    private eoTopicService: EnablingObjectivesTopicService,
    private alert: SweetAlertService,
    private translate: TranslateService,
    private taskService: TasksService,
    public flyPanelSrvc: FlyInPanelService,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe,
  ) {
    this.eo.number = 1;
    this.eo.topicId = 0;
  }

  ngOnInit(): void {
    this.getEnablingObjective_Categories();
  }

  //filtering for different levels
  filterEnablingObjectives(field: EventTarget | null) {
    let f = field as HTMLInputElement;
    this.EOCategories = [...JSON.parse(JSON.stringify(this.tempEOCategories))];
    if (f.value != '') {
      this.EOCategories.map((i) => {
        if (i.description.toLowerCase().includes(f.value.toLowerCase())) {
          i = i;
        } else {
          i.enablingObjective_SubCategories.map((j) => {
            if (j.description.toLowerCase().includes(f.value.toLowerCase())) {
              j = j;
            } else {
              j.enablingObjective_Topics.map((k) => {
                if (
                  k.description.toLowerCase().includes(f.value.toLowerCase())
                ) {
                  k = k;
                } else {
                  k.enablingObjectives = k.enablingObjectives.filter((l) =>
                    l.statement.toLowerCase().includes(f.value.toLowerCase())
                  );
                }
              }); //third map function
            }
          }); //second map function
        }
        return i;
      }); // first map function
    } // main if
  }
  async getEnablingObjective_Categories() {
    await this.eoCatSrvc.getAll().then((res) => {
      this.EOCategories = res;
      this.tempEOCategories = res;
    });
  }
  async openEOAddEditPanel(mode: string, id?: any) {
    this.AddEditEOMenu = true;
    this.EOMenuMode = mode;
    this.eo = new EnablingObjective();
    this.eo.number = 1;
    this.eo.topicId = undefined;
    this.selectedCategoryId = undefined;
    if (!this.EOCategories) this.getEnablingObjective_Categories();

    if (id) {
      await this.eoSrvc.get(id).then((res) => {
        Object.assign(this.eo, res);
        this.eoId = this.eo.id;

        this.selectedCategoryId =
          this.eo.enablingObjective_Topic.enablingObjectives_SubCategory.enablingObjectives_Category.id;
        this.getSubcategories();
        this.selectedSubCategoryId =
          this.eo.enablingObjective_Topic.enablingObjectives_SubCategory.id;
        this.getTopics();
      });
    }
  }

  async modifyEOStatus(item: EnablingObjective, action: string) {
    let opt: EnablingObjectiveOptions = {
      actionType: action,
      isSignificant: false,
      changeNotes:"",
      effectiveDate:new Date(),
      eoIds : [],
    };
    await this.eoSrvc.delete(item.id, opt).then((res) => {
      if (res) {
        this.getEnablingObjective_Categories();
        this.alert.successToast(this.translate.instant('L.rec' + action));
      }
    });
  }

  async linkEOtoTask() {
    let opt: TaskOptions = {
      isSignificant: false,
      enablingObjectiveIds: this.EOToLink,
      taskIds:[]
    };
    await this.taskService
      .LinkEnablingObjective(this.taskId, opt)
      .then(async (res) => {
        if (res) {
          this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.EOLinkedToTask')));
          this.EOToLink = [];
        }
      });
  }

  async UnlinkEOFromTask() {
    let opt: TaskOptions = {
      isSignificant: false,
      enablingObjectiveIds: this.EOToUnLink,
      taskIds:[]
    };
    await this.taskService
      .UnlinkEnablingObjective(this.taskId, opt)
      .then(async (res) => {
        if (res) {
          this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.EOUnlinkedToTask')));
          this.EOToUnLink = [];
        }
      });
  }

  AddToLinkList(id: any) {
    const index = this.EOToLink.indexOf(id, 0);
    if (index == -1) this.EOToLink.push(id);
    else this.EOToLink.splice(index, 1);
  }

  RemoveFromLinkList(id: any) {
    const index = this.EOToUnLink.indexOf(id, 0);
    if (index == -1) this.EOToUnLink.push(id);
    else this.EOToUnLink.splice(index, 1);
  }

  async saveEO() {
    if (this.eo.topicId == 0) {
      this.alert.errorAlert('Error', 'Please Select valid Topic');
      return;
    }
    let EOList = await this.eoSrvc.getAll();
    this.eo.number =
      EOList.filter((x) => x.topicId == this.eo.topicId).pop()?.number ?? 0;

    if (this.EOMenuMode == 'add') {
      // let opt: EnablingObjectiveCreateOptions = {
      //   categoryId: this.eo.categoryId,
      //   subCategoryId: this.eo.subCategoryId,
      //   topicId: this.eo.topicId,
      //   number: this.eo.number + 1,
      // };
      // await this.eoSrvc.create(opt).then((res) => {
      //   if (res) {
      //     this.alert.successToast(this.translate.instant('L.recAdded'));
      //     this.eoCreateOpt = new EnablingObjectiveCreateOptions();
      //     this.eoCreateOpt.number = 1;
      //     this.eoCreateOpt.topicId = 0;
      //     this.getEnablingObjective_Categories();
      //     this.AddEditEOMenu = false;
      //   }
      // });
    } else {
      let opt:EnablingObjectiveCreateOptions = new EnablingObjectiveCreateOptions();
      await this.eoSrvc.update(this.eoId, opt).then((res) => {
        if (res) {
          this.alert.successToast(this.translate.instant('L.recUpdated'));
          this.eoCreateOpt = new EnablingObjectiveCreateOptions();
          this.eoCreateOpt.number = 1;
          this.eoCreateOpt.topicId = 0;
          this.getEnablingObjective_Categories();
          this.AddEditEOMenu = false;
        }
      });
    }
  }

  async getSubcategories() {
    if (!this.selectedCategoryId) {
      this.EOSubCategories = [];
      return;
    }
    await this.eoCatSrvc
      .getSubCategories(this.selectedCategoryId)
      .then((res) => {
        if (res) this.EOSubCategories = res;
      });
  }

  async getTopics() {
    if (!this.selectedSubCategoryId) {
      this.EOTopics = [];
      return;
    }
    await this.eoTopicService
      .getAllSimplifiedTopics(this.selectedCategoryId, this.selectedSubCategoryId)
      .then((res) => {
        if (res) {
          this.EOTopics = res;
        }
      });
  }

}
