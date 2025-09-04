import {Component,EventEmitter,Input,OnDestroy,OnInit, Output, ViewChild} from '@angular/core';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { MetaILA_SummaryTest_ViewModel } from 'src/app/_DtoModels/MetaILA_SummaryTest/MetaILA_SummaryTest_ViewModel';
import { TestsService } from 'src/app/_Services/QTD/tests.service';

@Component({
  selector: 'app-add-and-sequence-test-questions',
  templateUrl: './add-and-sequence-test-questions.component.html',
  styleUrls: ['./add-and-sequence-test-questions.component.scss'],
})
export class AddAndSequenceTestQuestionsComponent implements OnInit, OnDestroy {
  @Input() addedQuestionsData: MatTableDataSource<TestItem>;
  @Input() metaILASummaryTestVM:MetaILA_SummaryTest_ViewModel;
  @Output() openAddTestItemFlyPanel  = new EventEmitter<boolean>();
  @Output() openImportTestItemFlyPanel  = new EventEmitter<boolean>();
  @Output() deleteTestItem  = new EventEmitter<string>();
  testItemSequence:boolean;
  randomDistractor:boolean;
  dragDisabled: boolean = true;
  displayColumns: string[] = ['cb', 'number', 'question', 'type', 'taxonomyLevel', 'del'];
  constructor(private testService:TestsService
  ) {}
  
  async ngOnInit(): Promise<void> {
   await this.loadAsync();
  }

  ngOnDestroy(): void {
  }

  async loadAsync(){
    const res = await this.testService.GetTestItem(this.metaILASummaryTestVM?.test?.id);
    this.testItemSequence = Array.isArray(res) && res.some(item => item.sequence > 0);
    this.randomDistractor=this.metaILASummaryTestVM?.test?.randomizeDistractors;
  }
  
  drop(event: CdkDragDrop<string>) {
    this.dragDisabled = true;
    const prevIndex = this.addedQuestionsData.data.findIndex((d) => d === event.item.data);
    moveItemInArray(this.addedQuestionsData.data, event.previousIndex, event.currentIndex);
    this.addedQuestionsData = new MatTableDataSource(this.addedQuestionsData.data);
  }
 
  
}
