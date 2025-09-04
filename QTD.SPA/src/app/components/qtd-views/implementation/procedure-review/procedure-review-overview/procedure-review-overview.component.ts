import { ProcedureReviewDeleteOptions } from '@models/Procedure/Procedure_review';
import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarBackDrop, sideBarClose, sideBarDisableClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProcedureReviewOverviewVM } from '@models/Procedure/Procedure_review/ProcedureReviewOverviewVM';

const ELEMENT_DATA: any[] = [
  { procedure: 'SOP-001', title: 'EROCT', name: 'standerd 2', releaseDate: '08/14/2018', reviewDate: '08/14/2013', reviewStatus: false, devStatus: true, },
  { procedure: 'SOP-001', title: 'EROCT', name: 'standerd 2', releaseDate: '08/14/2018', reviewDate: '08/14/2013', reviewStatus: false, devStatus: true, },
  { procedure: 'SOP-001', title: 'EROCT', name: 'standerd 2', releaseDate: '08/14/2018', reviewDate: '08/14/2013', reviewStatus: false, devStatus: true, },
  { procedure: 'SOP-001', title: 'EROCT', name: 'standerd 2', releaseDate: '08/14/2018', reviewDate: '08/14/2013', reviewStatus: false, devStatus: true, },
  { procedure: 'SOP-001', title: 'EROCT', name: 'standerd 2', releaseDate: '08/14/2018', reviewDate: '08/14/2013', reviewStatus: false, devStatus: true, },
  { procedure: 'SOP-001', title: 'EROCT', name: 'standerd 2', releaseDate: '08/14/2018', reviewDate: '08/14/2013', reviewStatus: false, devStatus: true, },

];
@Component({
  selector: 'app-procedure-review-overview',
  templateUrl: './procedure-review-overview.component.html',
  styleUrls: ['./procedure-review-overview.component.scss']
})
export class ProcedureReviewOverviewComponent implements OnInit, AfterViewInit {
  isLoading: boolean = true;
  editProcedureId: any;
  editProcedureTitle: any;
  publishProcedure: any = 0
  draftProcedure: any = 0;
  pendingProcedure: any = 0;
  procedure_review_list: any[] = [];
  datePipe = new DatePipe('en-us');
  modalHeader: string;
  modalDescription: string = `
  `;
  procedureReviewObj: any;
  todayDate: string;
  procedureReviewStatues: any;
  procedureReviewInDrafts: number = 0;
  procedureReviewNumberofEmployeesPending: number = 0;
  procedureReviewPublished: number = 0;

  authority_list: any[] = [];
  initialIssuingAuthorityList: any[] = [];
  isSpinner: boolean = true;
  authFilter: any;
  reviewStatus: string = '';
  devStatus: string = '';
  dateRange: any;
  clearFilter: boolean = false;
  filterText: string = '';

  procedureReviewList: ProcedureReviewOverviewVM[] = [];
  searchTxt = '';
  procedureReviewDataSource = new MatTableDataSource<any>();
  @ViewChild('procedureFilters', { static: true }) procedureFilters: TemplateRef<any>;
  selection = new SelectionModel<any>(true, []);
  isLicenseValid:boolean=true;
  displayedColumns: string[] = [
    'index',
    'customProcedureColumn',
    'procedureReviewTitle',
    'issuingAuthorityTitle',
    'startDateTime',
    'endDateTime',
    'reviewStatus',
    'isPublished',
    'action',
  ];
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.procedureReviewDataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.procedureReviewDataSource.paginator = paginator;
  }
  constructor(
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private store: Store<{ toggle: string }>,
    private _router: Router,
    private procedureService: ProceduresService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private issuingAuthoritiesService: IssuingAuthoritiesService,
    private licenseHelper:LicenseHelperService,
    private labelPipe: LabelReplacementPipe,

  ) { }
  ngAfterViewInit(): void {
    // var searchText = localStorage.getItem('searchText');
    // this.filterText = JSON.parse(searchText);
  }

  @ViewChild(MatSort, { static: false }) sort: MatSort;

  async ngOnInit(): Promise<void> {
    this.checkLicense();
    if(this.isLicenseValid){
      this.todayDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
      this.getProcedureReview(true);
      this.getProcedureReviewStatus();
      this.getAuthority();
      this.procedureReviewDataSource.filterPredicate = (row: any, filter: string) => {
        const filterObject = JSON.parse(filter);
        let startDateTime = this.datePipe.transform(row.startDateTime, 'yyyy-MM-dd').toString().trim().toLowerCase();
        let endDateTime = this.datePipe.transform(row.endDateTime, 'yyyy-MM-dd').toString().trim().toLowerCase();
        return ((
          row.issuingAuthorityId.toString().trim().toLowerCase().includes(filterObject.authFilterId) ||
          filterObject.publish === true ? row.publishedDate === filterObject.publish : '' ||
          filterObject.schedule === true ? row.scheduleDate === filterObject.schedule : '' ||
          startDateTime.includes(this.datePipe.transform(filterObject.startDate, 'yyyy-MM-dd')) ||
          endDateTime.includes(this.datePipe.transform(filterObject.endDate, 'yyyy-MM-dd')) ||
          filterObject.pos !== '' ? (row.isClose.toString().trim().toLowerCase().includes(filterObject.pos) ||
          row.isOpen.toString().trim().toLowerCase().includes(filterObject.pos)) : ''
          ))
        }
    }
    this.modalHeader = 'Delete ' + await this.labelPipe.transform('Procedure') + ' Review';
  }
  checkLicense(){
    var license = this.licenseHelper.getLicenseData();
    if(!license?.deluxe || !license?.hasEmp){
      this.isLicenseValid = false;
    }
  }
  filterName: any;

  convertUtcToLocalDate(val: Date): Date {
    var d = new Date(val); // val is in UTC
    var localOffset = d.getTimezoneOffset() * 60000;
    var localTime = d.getTime() - localOffset;

    d.setTime(localTime);
    return d;
  }

   async getProcedureReview(isPageLoad?:boolean) {
    await this.procedureService.getProcedureReviews().then((result) => {
      this.isLoading = false;
      this.procedureReviewList = result;

      this.procedureReviewList = this.procedureReviewList.map(obj => ({
        ...obj,
        isOpen: this.openStatus(obj) === true ? 'open' : '',
        isClose: this.closeStatus(obj) === true ? 'close' : '',
        publishedDate: this.publishedDate(obj) === true ? true : false,
        isDeletable: this.isDeletable(obj),
        isInUse: this.isInUse(obj),
        isStatusChangeable: this.isStatusChangeable(obj),
        scheduleDate: this.scheduleDate(obj) === true ? true : false
      }))

      this.procedureReviewDataSource.data = Object.assign(this.procedureReviewList);
      if(isPageLoad){
        this.reviewStatus='open';
        this.clearFilter=true;
      }
      this.commonFilterProcedureReview();
      this.isLoading = false;
      if (this.filterText !== '') {
        localStorage.removeItem('searchText');
      }
    });
    
    this.procedureReviewDataSource.sortingDataAccessor = this.customSortDataAccessor;
    this.procedureReviewDataSource.sort = this.sort;
  }

  isDeletable(obj: any) {
    if (obj.isStarted === true) {
      return true;
    }
    return false;
  }



  isInUse(obj: any) {
    if (obj.isStarted === true) {
      return true;
    }
    return false;
  }

  isStatusChangeable(obj: any) {

    let dateNow = new Date();
    let startDate = this.convertUtcToLocalDate(obj.startDateTime);
    if (startDate < dateNow && obj.procedureReview_Employee?.length > 0 && obj.isPublished === true && obj.active === true) {
      return true;
    }
    return false;
  }

  openFlyInPanelProcedureReviewDraft(templateRef: any) {

    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }
  async getProcedureReviewStatus() {
    this.procedureService.getProcedureReviewStatus().then((res) => {
      this.procedureReviewInDrafts = res.procedureReviewInDrafts;
      this.procedureReviewNumberofEmployeesPending = res.procedureReviewNumberofEmployeesPending
      this.procedureReviewPublished = res.procedureReviewPublished
    }).catch((res: any) => {

      this.alert.errorToast(res);
    })
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.procedureReviewDataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {

    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.procedureReviewDataSource.data);
  }

  clearSearch: any;
  clearFilterFunction() {
    this.filterText = '';
    this.commonFilterProcedureReview();
  }

  async getAuthority() {
    this.isSpinner = true;
    this.issuingAuthoritiesService
      .getAll()
      .then((res: any) => {

        this.authority_list = res;
        this.initialIssuingAuthorityList = Object.assign(this.authority_list);
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.isSpinner = false;
      });
  }
  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;


  }
  filterProcedureReview(filteringData:any[]) {

    this.procedureReviewDataSource.data = Object.assign(this.procedureReviewList);
    let filterString = this.filterText ?? '';
    filteringData = filteringData.filter((x) =>
      x?.procedureNumber.toLowerCase()?.includes(String(filterString ?? "")?.toLowerCase()) ||
      x?.procedureTitle?.toLowerCase()?.includes(String(filterString ?? "")?.toLowerCase()) ||
      x?.procedureReviewTitle?.toLowerCase()?.includes(String(filterString ?? "")?.toLowerCase())
    );
      return filteringData
  }


  scheduleDate(element) {
    let currentDate = new Date(this.todayDate)
    let releaseDate = new Date(this.datePipe.transform(this.convertUtcToLocalDate(element.startDateTime), 'yyyy-MM-dd'))
    if (element.isPublished && (currentDate < releaseDate) && element.active) {
      return true;
    } else {
      return false;
    }
  }

  InDevelopmentStatus(element) {
    let currentDate = new Date(this.todayDate)
    let dueDate = new Date(this.datePipe.transform(element.endDateTime, 'yyyy-MM-dd'));
    let releaseDate = new Date(this.datePipe.transform(element.startDateTime, 'yyyy-MM-dd'))
    if (!(currentDate >= releaseDate && currentDate <= dueDate) && element.active) {
      return true;
    } else {
      return false;
    }
  }
  publishedDate(element) {

    let currentDate = new Date(this.todayDate)
    let releaseDate = new Date(this.datePipe.transform(this.convertUtcToLocalDate(element.startDateTime), 'yyyy-MM-dd'));
    let dueDate = new Date(this.datePipe.transform(this.convertUtcToLocalDate(element.endDateTime), 'yyyy-MM-dd'));
    if (element.isPublished && ((currentDate >= releaseDate)) && element.active) {
      return true;
    } else {
      return false;
    }
  }

  closeStatus(element) {
    let dueDate = new Date(this.datePipe.transform(element.endDateTime, 'yyyy-MM-dd'));
    let currentDate = new Date(this.todayDate)
    if ((currentDate > dueDate)) {
      return true;
    } else {
      return false;
    }
  }
  openStatus(element) {
    let dueDate = new Date(this.datePipe.transform(element.endDateTime, 'yyyy-MM-dd'));
    let currentDate = new Date(this.todayDate)
    if ((currentDate <= dueDate)) {
      return true;
    } else {
      return false;
    }
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async addNewProcedureReview() {
    if (this.filterText !== '') {
      localStorage.setItem('searchText', JSON.stringify(this.filterText));
    }
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this._router.navigate(['/procedure/add']);
  }

  async EditProcedureReview(templateRef: any, id: any, title: any, isOpen: any, isInUse: any) {
    this.editProcedureId = id;
    let proc = this.procedureReviewList.find(x => x.id === id);
    this.editProcedureTitle = title;
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    if (isInUse) {
      const dialogRef = this.dialog.open(templateRef, {
        width: '700px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    } else {
      if (this.filterText !== '') {
        localStorage.setItem('searchText', JSON.stringify(this.filterText));
      }
      this._router.navigate(['/procedure/edit/', id]);
    }
  }

  goToViewEnroll(id: any) {
    if (this.filterText !== '') {
      localStorage.setItem('searchText', JSON.stringify(this.filterText));
    }
    this._router.navigate(['/procedure/procedure-enroll/', id]);
  }

  async deleteProcedureReview(templateRef: any, row: any) {
    this.procedureReviewObj = row;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    this.modalDescription = `You are selecting to delete the ` + await this.labelPipe.transform('Procedure') + ` Review  ${row.procedureTitle}. Are you sure you want to continue?`;
  }

  async changeStatus(templateRef: any, row: any) {
    this.procedureReviewObj = row;
    this.modalHeader = "Active and Inactive";
    this.modalDescription = `You are selecting to active/Inactive the ` + await this.labelPipe.transform('Procedure') + ` Review  ${row.procedureTitle}.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  deleteIt(e: any) {

    let mode = this.selection.selected.map(x => x.id) == null || this.selection.selected.map(x => x.id) == undefined || this.selection.selected.map(x => x.id).length == 0 ? [this.procedureReviewObj.id] : this.selection.selected.map(x => x.id);
    var options = new ProcedureReviewDeleteOptions();
    options.actionType = "delete";
    options.procedurereviewIds = [];
    options.procedurereviewIds.push(...mode);

    this.procedureService.deleteProcedureReviewById(this.procedureReviewObj.id, options)
      .then(async (res: any) => {
        this.getProcedureReview();
        this.alert.successToast(await this.labelPipe.transform('Procedure') + " Review is successfully deleted !")
      });
  }
  makeActive(e: any) {
    let mode = this.selection.selected.map(x => x.id) == null || this.selection.selected.map(x => x.id) == undefined || this.selection.selected.map(x => x.id).length == 0 ? [this.procedureReviewObj.id] : this.selection.selected.map(x => x.id);
    var options = new ProcedureReviewDeleteOptions();
    options.actionType = this.procedureReviewObj.active === true ? "inactive" : "active";
    options.procedurereviewIds = [];
    options.procedurereviewIds.push(...mode);

    this.procedureService.deleteProcedureReviewById(this.procedureReviewObj.id, options)
      .then(async (res: any) => {
        this.getProcedureReview();
        if (options.actionType === "active") {
          this.alert.successToast(await this.labelPipe.transform('Procedure') + " Review is successfully activated !")

        } else {
          this.alert.successToast(await this.labelPipe.transform('Procedure') + " Review is successfully inactivated !")

        }
      });
  }


  applyFilters(filteringData:any[]) {
    var temp =filteringData;
    if (this.authFilter !== undefined && this.authFilter !== null) {
      temp = temp.filter(x => x.issuingAuthorityId === this.authFilter?.id);

    }

    if (this.reviewStatus !== undefined && this.reviewStatus !== null && this.reviewStatus !== '' && this.reviewStatus !== "All") {
      temp = temp.filter(x => (x.isClose.toString().trim().toLowerCase().includes(this.reviewStatus) ||
        x.isOpen.toString().trim().toLowerCase().includes(this.reviewStatus)));
    }

    if (this.devStatus !== undefined && this.devStatus !== null && this.devStatus !== '' && this.devStatus !== "All") {

      switch (this.devStatus) {
        case 'publish':
          temp = temp.filter(x => x.publishedDate && !x.isInUse);
          break;
        case 'schedule':
          temp = temp.filter(x => x.isInUse);
          break;
        case 'indevelopment':
          temp = temp.filter(x => x.isPublished === false);

          break;
        case 'inactive':
          temp = temp.filter(x => x.active === false);
          break;
      }
    }

    if (this.dateRange !== undefined && this.dateRange !== null && this.dateRange.startDate !== null && this.dateRange.startDate !== undefined && this.dateRange.endDate !== null && this.dateRange.endDate !== undefined && this.dateRange?.startDate !== '' && this.dateRange?.endDate != '') {
      temp = temp.filter(x => this.datePipe.transform(x.startDateTime, 'yyyy-MM-dd').includes(this.datePipe.transform(this.dateRange?.startDate, 'yyyy-MM-dd')) &&
        this.datePipe.transform(x.endDateTime, 'yyyy-MM-dd').includes(this.datePipe.transform(this.dateRange?.endDate, 'yyyy-MM-dd')));
    }

    let filters: any = [];
    filters = JSON.stringify({
      status: this.authFilter,
      pos: this.reviewStatus,
      org: this.devStatus,
    });
    this.filterData = JSON.parse(filters);
    return temp;
  }
  filterData: any;
  DevStatus: any;
  ReviewStatus: any;
  IAName: any;
  clearFilterData() {
    this.clearFilter = false;
    this.authFilter = null;
    this.devStatus = null;
    this.reviewStatus = null;
    this.dateRange = null;
    this.dateRange = null;
    this.commonFilterProcedureReview();
  }

  public issuingAuthoritySearch(value: any) {
    var filterString = this.searchTxt;
    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    this.authority_list = this.initialIssuingAuthorityList.filter((f) => {
      const title = `${f?.title}`;
      return title.toLowerCase().includes(filterString);
    });
  }

  customSortDataAccessor(data: any, sortHeaderId: string): string | number {
    if (sortHeaderId === 'customProcedureColumn') {
      const customValue = data.procedureNumber + '-' + data.procedureTitle;
      return customValue;
    }
    if (sortHeaderId === 'reviewStatus') {
      if (data.isClose === 'close') {
        return 1;
      } else if (data.isOpen === 'open') {
        return 2;
      }
    }
    const nestedKeys = sortHeaderId.split('.');
    let value = data;
    for (const key of nestedKeys) {
      value = value[key];
    }
    return value;
  }
  commonFilterProcedureReview(){
    var filterData = this.procedureReviewList;
    filterData = this.filterProcedureReview(filterData);
    filterData = this.applyFilters(filterData);
    this.procedureReviewDataSource.data=filterData;
  }

}