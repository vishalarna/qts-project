import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { TaxonomyLevel } from 'src/app/_DtoModels/TaxonomyLevel/TaxonomyLevel';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-flypanel-preview-test-questions',
  templateUrl: './flypanel-preview-test-questions.component.html',
  styleUrls: ['./flypanel-preview-test-questions.component.scss']
})
export class FlypanelPreviewTestQuestionsComponent implements OnInit, AfterViewInit, OnDestroy {

  testItemForm = new UntypedFormGroup({});
  @Output() closed = new EventEmitter<any>();
  @Input() previousQuestion!: any;
  selectedType: TestItemType;
  testItems: TestItem
  testItemTypes: TestItemType[] = [];
  taxonomyLevels: TaxonomyLevel[] = [];
  eoData!: EnablingObjective;
  mode: 'edit';
  eoId = '';

  subscription = new SubSink();

  constructor(public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private testItemTypeService: TestItemTypeService,
    private taxonomyLevelService: TaxonomyLevelService,
    private dataBroadcastService: DataBroadcastService,
    private eoService: EnablingObjectivesService,
    private testItemService: TestItemService) { }

    ngOnInit(): void {
      
      this.readyTestItemForm();
      this.readyTypeData();
      this.readyTaxonomyData();
    }

    ngAfterViewInit(): void {
    }

    ngOnDestroy(): void {
      this.subscription.unsubscribe();
    }

    readyTestItemForm() {
      this.testItemForm.addControl('type', new UntypedFormControl(''));
      this.testItemForm.addControl('taxonomy', new UntypedFormControl(''));
    }

    async readyTypeData() {
      
      this.testItems = await this.testItemService.get(this.previousQuestion.id);
      this.testItemTypes = await this.testItemTypeService.getAll();
      var typeData = this.testItemTypes.find((data) => {
          return data.id == this.previousQuestion.typeId;
        })
        this.testItemForm.patchValue({
          type: typeData,
        });
        this.selectedType = typeData ?? new TestItemType();
    }

    async readyTaxonomyData() {
      this.taxonomyLevels = await this.taxonomyLevelService.getAll();
      var typeData = this.taxonomyLevels.find((data) => {
        return data.id == this.previousQuestion.taxonomyId;
      })
      this.testItemForm.patchValue({
          taxonomy: typeData?.id,
        })
    }

}
