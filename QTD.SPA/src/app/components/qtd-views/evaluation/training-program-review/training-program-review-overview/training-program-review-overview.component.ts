import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { EvaluationStatsCount } from 'src/app/_DtoModels/EvaluationMethod/EvaluationStatsCount';
import { TrainingProgramReview_OverviewReviewViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_OverviewReviewViewModel';
import { TrainingProgramReview_OverviewViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_OverviewViewModel';
import { ApiTrainingProgramReviewService } from 'src/app/_Services/QTD/TrainingProgramReview/api.trainingProgramReview.Service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose, sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { DatePipe } from '@angular/common';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { TrainingProgramTypeService } from 'src/app/_Services/QTD/training-program-type.service';
import { TrainingProgramType } from 'src/app/_DtoModels/TrainingProgramType/TrainingProgramType';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-training-program-review-overview',
  templateUrl: './training-program-review-overview.component.html',
  styleUrls: ['./training-program-review-overview.component.scss']
})
export class TrainingProgramReviewOverviewComponent implements OnInit {
  @Input() handleLoad: () => void;
  @Input() handleFilterClick: () => void
  @Input() handleSearchUpdate: (e) => void
  @Input() handleEditClick: () => void
  @Input() handleCopyClick: () => void
  @Input() handleDeleteClick: () => void
  @Input() handleMakeActiveClick: () => void
  @Input() handleMakeInactiveClick: () => void
  spinner: boolean = false;
  isCreateVisible: boolean = false;
  stats = new EvaluationStatsCount();
  selection = new SelectionModel<any>(true, []);
  displayedColumns: Array<string>;
  dataSourceTrainingPrograms;
  isFilterPanelOpen: boolean = false;
  filterText: string = '';
  modalHeader = '';
  modalDescription = '';
  trainingProgramReviewId: string;
  datePipe = new DatePipe('en-US');
  overviewStats: TrainingProgramReview_OverviewViewModel;
  trainingProgramReview: TrainingProgramReview_ViewModel;
  positionIdFilter: number;
  trainingProgramTypes:TrainingProgramType[]=[];
  trainingProgramTypeIdFilter: string;
  startDateMaxfilter: Date;
  endDateMinFilter: Date;
  publishedfilter: string;
  activeFilter: string;
  search: string;
  overviewReviews: TrainingProgramReview_OverviewReviewViewModel[];
  filteredOverviewReviews: TrainingProgramReview_OverviewReviewViewModel[];

  @ViewChild(MatSort) sort: MatSort;

  constructor(public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef, private trainingProgramReviewService: ApiTrainingProgramReviewService,
    private trainingProgramTypeService : TrainingProgramTypeService,
    private _router: Router,
    public dialog: MatDialog,
    private store: Store<{ toggle: string }>,
    private labelPipe: LabelReplacementPipe) {

  }

  async ngOnInit(): Promise<void> {
    this.store.dispatch(sideBarOpen());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.displayedColumns = [
      'index',
      'positionAbbreviation',
      'trainingProgramType',
      'startDate',
      'reviewers',
      'reviewDate',
      'published',
      'active',
      'id',
    ];
    this.dataSourceTrainingPrograms = new MatTableDataSource<TrainingProgramReview_OverviewReviewViewModel>();
    await this.loadAsync();
  }

  createTPReview() {
    this.store.dispatch(freezeMenu({ doFreeze: false }))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/evaluation/trainingprogram-review/create']);
  }

  filterTrainingPrograms(e: Event) { }

  filterClick = () => {
    this._handleFilterClick();
    this.isFilterPanelOpen = !this.isFilterPanelOpen;
  }

  async openFlyInPanelList(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSourceTrainingPrograms.data.length;
    return numSelected === numRows;
  }
  _handleLoad() {
    if (this.handleLoad && typeof this.handleLoad === 'function') {
      this.handleLoad();
    }
  }
  _handleFilterClick() {
    if (this.handleFilterClick && typeof this.handleFilterClick === 'function') {
      this.handleFilterClick();
    }
  }
  _handleEditClick() {
    if (this.handleEditClick && typeof this.handleEditClick === 'function') {
      this.handleEditClick();
    }
  }
  _handleCopyClick() {
    if (this.handleCopyClick && typeof this.handleCopyClick === 'function') {
      this.handleCopyClick();
    }
  }
  _handleSearchUpdate(e: any) {
    if (this.handleSearchUpdate && typeof this.handleSearchUpdate === 'function') {
      this.handleSearchUpdate(e);
    }
  }
  _handleDeleteClick() {
    if (this.handleDeleteClick && typeof this.handleDeleteClick === 'function') {
      this.handleDeleteClick();
    }
  }

  _handleMakeActiveClick() {
    if (this.handleMakeActiveClick && typeof this.handleMakeActiveClick === 'function') {
      this.handleMakeActiveClick();
    }
  }

  _handleMakeInactiveClick() {
    if (this.handleMakeInactiveClick && typeof this.handleMakeInactiveClick === 'function') {
      this.handleMakeInactiveClick();
    }
  }


  public editClick(trainingProgramReviewld) {
    this._handleEditClick();
    this.store.dispatch(freezeMenu({ doFreeze: false }))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/evaluation/trainingprogram-review/create'], {
      queryParams: { data: trainingProgramReviewld },
    });
  }


  searchUpdate(event: any) {
    this._handleSearchUpdate(event);
    this.search = event.target.value;
    this._filterOverviewReviews();
  }

  async loadAsync() {
    this._handleLoad();
    this.overviewStats = await this.trainingProgramReviewService.getOverviewAsync();
    this.dataSourceTrainingPrograms = new MatTableDataSource<TrainingProgramReview_OverviewReviewViewModel>(this.overviewStats.trainingProgramReviewOverviewReviews.filter(review => review.active === true).map((item)=>{
      var revDate = new Date(item.reviewDate + "Z");
      var reviewDate = new Date(revDate).toLocaleString();
      var newStartDate= new Date(item.startDate + "Z");
      var startDate = new Date(newStartDate).toLocaleString();
      var newEndDate= new Date(item.endDate + "Z");
      var endDate = new Date(newEndDate).toLocaleString();
      return {...item,reviewDate:reviewDate,startDate:startDate,endDate:endDate }
    }));
    this.activeFilter = 'active';
    this.dataSourceTrainingPrograms.sort = this.sort;
    this.trainingProgramTypes=await this.trainingProgramTypeService.getAll();
  }

  async copyClickAsync(trainingProgramReviewId: string) {
    this._handleCopyClick();
    this.trainingProgramReview = null;
    this.trainingProgramReview = await this.trainingProgramReviewService.copyAsync(trainingProgramReviewId);
    this.editClick(this.trainingProgramReview.id);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async makeActiveClick(templateRef: any, row: any) {
    this._handleMakeActiveClick();
    this.trainingProgramReviewId = row.trainingProgramReviewId;
    this.modalHeader = "Make Active";
    let displayedHeader = this.modalHeader.replace("Make ", "");
    this.modalDescription = `You are selecting to make the training Program Review for the ` + await this.transformTitle('Position') +`: 
    <strong><i>${row.positionName} </i></strong> to <strong><i>${displayedHeader}</i></strong>. `;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

 async makeInactiveClick(templateRef: any, row: any) {
    this._handleMakeInactiveClick();
    this.trainingProgramReviewId = row.trainingProgramReviewId;
    this.modalHeader = "Make Inactive";
    let displayedHeader = this.modalHeader.replace("Make ", "");
    this.modalDescription = `You are selecting to make the training Program Review for the ` + await this.transformTitle('Position') +`: <strong><i>${row.positionName}</i></strong> to <strong><i>${displayedHeader}</i></strong>.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getDataActiveInactiveAsync() {
    if (this.modalHeader === 'Make Inactive') {
      await this.trainingProgramReviewService.inactivateAsync(this.trainingProgramReviewId);
      this.overviewStats.trainingProgramReviewOverviewReviews.find(
        review => review.trainingProgramReviewId === this.trainingProgramReviewId
      ).active = false;
    }
    else {
      await this.trainingProgramReviewService.activateAsync(this.trainingProgramReviewId);
      this.overviewStats.trainingProgramReviewOverviewReviews.find(
        review => review.trainingProgramReviewId === this.trainingProgramReviewId
      ).active = true;
    }
   await this.loadAsync();
  }

  deleteClick(templateRef: any, id: string) {
    this._handleDeleteClick();
    this.trainingProgramReviewId = id;
    this.modalHeader = 'Delete Training Program Review';
    this.modalDescription = `Are you sure you want to delete this Training Program Review?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getDataDeletedAsync() {
    await this.trainingProgramReviewService.deleteAsync(this.trainingProgramReviewId);
    const index = this.overviewStats.trainingProgramReviewOverviewReviews.findIndex(r => r.trainingProgramReviewId === this.trainingProgramReviewId);
    if (index !== -1) {
      const removedReview = this.overviewStats.trainingProgramReviewOverviewReviews.splice(index, 1)[0];
      const hasEntry = this.overviewStats.trainingProgramReviewOverviewReviews.some(entry => entry.trainingProgramId === removedReview.trainingProgramId);
      if (!hasEntry) {
        this.overviewStats.noReviewTrainingPrograms = (parseInt(this.overviewStats.noReviewTrainingPrograms, 10) + 1).toString();
      }
      this._filterOverviewReviews();
    }
  }

  _filterOverviewReviews = () => {
    this.dataSourceTrainingPrograms = this.overviewStats.trainingProgramReviewOverviewReviews.map(item => {
      let startDate = new Date(item.startDate + "Z").toLocaleString();
      let endDate = new Date(item.endDate + "Z").toLocaleString();
      let reviewDate = new Date(item.reviewDate + "Z").toLocaleString();
      return {...item, startDate, endDate, reviewDate};
    });
    this.filteredOverviewReviews = this.dataSourceTrainingPrograms;
    if (this.search) {
      this.filteredOverviewReviews = this.filteredOverviewReviews.filter((item) =>
        item.positionAbbreviation.toLowerCase().includes(this.search.toLowerCase()) ||
        item.positionName.toLowerCase().includes(this.search.toLowerCase()) ||
        item.trainingProgramType.toLowerCase().includes(this.search.toLowerCase()) ||
        item.reviewers.some(reviewer => reviewer.employeePersonFullName.toLowerCase().includes(this.search.toLowerCase()))
      )
    }
    if (this.positionIdFilter) {
      this.filteredOverviewReviews = this.filteredOverviewReviews.filter(x => x.positionId === this.positionIdFilter.toString());
    }
    if (this.trainingProgramTypeIdFilter) {
      this.filteredOverviewReviews = this.filteredOverviewReviews.filter(x => x.trainingProgramTypeId === this.trainingProgramTypeIdFilter);
    }
    if (this.publishedfilter === "published") {
      this.filteredOverviewReviews = this.filteredOverviewReviews.filter(x => x.published);
    } else if (this.publishedfilter === "draft") {
      this.filteredOverviewReviews = this.filteredOverviewReviews.filter(x => !x.published);
    }
    if (this.activeFilter === "active") {
      this.filteredOverviewReviews = this.filteredOverviewReviews.filter(x => x.active);
    } else if (this.activeFilter === "inactive") {
      this.filteredOverviewReviews = this.filteredOverviewReviews.filter(x => !x.active);
    }
    if (this.endDateMinFilter && this.startDateMaxfilter) {
      this.filteredOverviewReviews = this.filteredOverviewReviews.filter(x => {
        const startDate = new Date(x.startDate);
        const endDate = new Date(x.endDate);
        return startDate <= this.startDateMaxfilter && endDate >= this.endDateMinFilter;
      });
    }
    this.dataSourceTrainingPrograms = new MatTableDataSource(this.filteredOverviewReviews);
    this.dataSourceTrainingPrograms.sort = this.sort;
  }

  filterByTilesClick(value: string) {
    this.clearFlypanelFilters();
    if (value === 'activeInitial') {
      this.trainingProgramTypeIdFilter = this.trainingProgramTypes.find(x=>x.trainingProgramTypeTitle=="Initial Training Program").id;
      this.activeFilter = 'active';
    }
    else if (value === 'inactiveInitial') {
      this.trainingProgramTypeIdFilter = this.trainingProgramTypes.find(x=>x.trainingProgramTypeTitle=="Initial Training Program").id;
      this.activeFilter = 'inactive';
    }
    else if (value === 'activeContinuing') {
      this.trainingProgramTypeIdFilter = this.trainingProgramTypes.find(x=>x.trainingProgramTypeTitle=="Continuing Training Program").id;
      this.activeFilter = 'active';
    }
    else if (value === 'inactiveContinuing') {
      this.trainingProgramTypeIdFilter = this.trainingProgramTypes.find(x=>x.trainingProgramTypeTitle=="Continuing Training Program").id;
      this.activeFilter = 'inactive';
    }
    else if (value === 'activeCycle') {
      this.trainingProgramTypeIdFilter =this.trainingProgramTypes.find(x=>x.trainingProgramTypeTitle=="Cycle Training Program").id;
      this.activeFilter = 'active';
    }
    else if (value === 'inactiveCycle') {
      this.trainingProgramTypeIdFilter = this.trainingProgramTypes.find(x=>x.trainingProgramTypeTitle=="Cycle Training Program").id;
      this.activeFilter = 'inactive';
    }
    else if (value === 'draft') {
      this.publishedfilter = 'draft';
      this.activeFilter = 'active';
    }
    this._filterOverviewReviews();
  }

  clearFlypanelFilters(){
    this.positionIdFilter=null;
    this.trainingProgramTypeIdFilter=null;
    this.startDateMaxfilter=null;
    this.publishedfilter=null;
    this.activeFilter=null;
    this.endDateMinFilter=null;
  }
}
