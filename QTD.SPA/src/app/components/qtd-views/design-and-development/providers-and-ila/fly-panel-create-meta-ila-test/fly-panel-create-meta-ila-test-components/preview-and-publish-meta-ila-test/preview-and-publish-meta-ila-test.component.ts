import {Component,Input,OnDestroy,OnInit, ViewChild, ViewContainerRef} from '@angular/core';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { TestItemFillBlank } from 'src/app/_DtoModels/TestItemFillBlank/TestItemFillBlank';
import { TestItemTrueFalse } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalse';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { MetaILA_SummaryTest_ViewModel } from 'src/app/_DtoModels/MetaILA_SummaryTest/MetaILA_SummaryTest_ViewModel';

@Component({
  selector: 'app-preview-and-publish-meta-ila-test',
  templateUrl: './preview-and-publish-meta-ila-test.component.html',
  styleUrls: ['./preview-and-publish-meta-ila-test.component.scss'],
})
export class PreviewAndPublishMetaILATestComponent implements OnInit, OnDestroy {
  @Input() metaILASummaryTestVM : MetaILA_SummaryTest_ViewModel;
  @Input() testItemsToPreview :TestItem[]=[];
  constructor(
    private alert: SweetAlertService,
  ) {}
  
   ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }
  
  processFIB(description: string, fib: TestItemFillBlank[]) {
    var previewString = description;
    for (const ans of fib) {
      previewString = previewString.replace(
        '<u>'+ans.correct+'</u>',
        `<u>${'&nbsp'.repeat(15)}</u>`
      );
    }

    return previewString

  }
  getCorrectTF(tfs: TestItemTrueFalse[]) {
    return tfs.find((data) => {
      return data.isCorrect;
    })?.id;
  }
  getCorrectMCQ(mcqs: TestItemMcq[]) {
    return mcqs.find((data) => {
      return data.isCorrect;
    })?.id;
  }
  

}
