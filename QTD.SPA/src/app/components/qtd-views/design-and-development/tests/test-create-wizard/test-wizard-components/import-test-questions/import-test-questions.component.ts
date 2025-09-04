import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { TestItemLink } from 'src/app/_DtoModels/TestItemLink/TestItemLink';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';

@Component({
  selector: 'app-import-test-questions',
  templateUrl: './import-test-questions.component.html',
  styleUrls: ['./import-test-questions.component.scss']
})
export class ImportTestQuestionsComponent implements OnInit, AfterViewInit {

  ImportTestFormGroup: UntypedFormGroup = new UntypedFormGroup({});
  dataSource: MatTableDataSource<any>;
  subscriptions = new SubSink();
  selectByType: string = "";
  quesLength = 0;
  isLoading: boolean = false;

  isSelectType: boolean = false;
  @Input() testId: any;
  subscription = new SubSink();
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  displayColumns: string[] = ['number', 'question', 'type', 'taxonomyLevel'];
  constructor(private router: Router,
    private route: ActivatedRoute,
    private testService: TestsService,
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private alert: SweetAlertService,
    ) { }

  ngOnInit(): void {
    this.readyImportTestForm();
    // this.subscription.sink = this.route.params.subscribe((res: any) => {
    //   if(res.id !== undefined)
    //   { 
    //     this.testId = res.id;
    //     this.getTestItems();
    //   }
      
    // });
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      if(res.id !== undefined)
      { 
        this.testId = res.id;
      }
      if(this.testId !== undefined)
      {
        this.getTestItems();
      }
      
    });
  }

  sortData(sort: Sort) {

    this.dataSource.sort = this.sort;

  }

  readyImportTestForm() {
    this.ImportTestFormGroup.addControl('byEO', new UntypedFormControl(''));
    this.ImportTestFormGroup.addControl('unlinkQuestions', new UntypedFormControl(''));
    this.ImportTestFormGroup.addControl('randomlyIndEO', new UntypedFormControl(''));
    this.ImportTestFormGroup.addControl('randomlyAllEO', new UntypedFormControl(''))
    this.subscriptions.sink = this.ImportTestFormGroup.statusChanges.subscribe((res: any) => {
      //this.formEvent.emit(res);
    });
  }

  SelectEOByQuestion() {
    this.router.navigate(['/dnd/tests/selectQuestion/'+ this.testId]);
  }

  SelectUnlinkedQuestion() {
    this.router.navigate(['dnd/tests/selectUnlinkedQuestion/' + this.testId]);
  }

  async getTestItems() {
    this.isLoading = true;
    this.testService.GetTestItem(this.testId)
        .then((res: any[]) => {
          this.quesLength = res.length;

          var tempData:any[] = [];
            res.forEach((data) => {
              tempData.push({
                number: data.number,
                id: data.testItemLinkId,
                question: data.question,
                type: data.testItemType,
                taxonomyLevel: data.taxonomyLevel
              });
            });

          this.dataSource = new MatTableDataSource(tempData);
          this.dataSource.paginator =  this.paginator;
          
        })
        .catch(() => {
          this.alert.errorToast("Error fetching test data");
        })
        .finally(() => {
          this.isLoading = false;
        })
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  close() {
    this.flyPanelService.close();
  }

}
