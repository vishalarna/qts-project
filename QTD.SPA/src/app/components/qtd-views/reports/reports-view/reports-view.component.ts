import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { ReportSkeletonCategoriesService } from 'src/app/_Services/QTD/ReportSkeletonsCategories/report-skeleton-categories.service';
import { ReportSkeletonCategories } from 'src/app/_DtoModels/ReportSkeleton_Category/ReportSkeletonCategories';
import { ReportSkeleton_Subcategories } from 'src/app/_DtoModels/ReportSkeleton_Category/ReportSkeleton_Subcategories';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ReportTreeControl } from 'src/app/_DtoModels/Report/ReportTreeControl';
import { SelectionModel } from '@angular/cdk/collections';
import { ReportsDeleteOptions } from '@models/Report/ReportsDeleteOption';

@Component({
  selector: 'app-reports-view',
  templateUrl: './reports-view.component.html',
  styleUrls: ['./reports-view.component.scss']
})
export class ReportsViewComponent implements OnInit {

  displayedColumns: string[] = ['select','Official Report Title', 'Internal Report Title', 'Category', 'Last Run'];
  public isPanelOpen: boolean = false;
  public reportSkeletonData;
  public reportsList;
  public reportId: number;
  public reportSkeletonId: number;
  public spinner:boolean;
  public reportCategories: any;
  public isCreateVisible: boolean;
  reportDetails: MatTableDataSource<any>;
  navList: ReportTreeControl[];
  reportSkeletonCategoryId:string;
  reportSkeletonCategory: ReportSkeletonCategories;
  reportSkeleton_CategoryName: string;
  reportSkeleton_Subcategories: ReportSkeleton_Subcategories[];
  selectedId: any;
  selectedItem: ReportTreeControl;
  reportSelection = new SelectionModel<any>(true, []);

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.reportDetails.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.reportDetails.paginator = paginator;
  }

  treeControl = new NestedTreeControl<ReportTreeControl>(
    (node) => node.Children
  );

  dataSource = new MatTreeNestedDataSource<ReportTreeControl>();
  hasChild = (_: number, node: ReportTreeControl) =>
    (node.Children !== null && node.Children !== undefined  && node.Children.length > 0) || (this.checkForChildren(node) && node.Children.length === 0);
  hasNestedChild = (_: number, nodeData: ReportTreeControl) =>
    nodeData.Children.length > 0;

  
  constructor(
    private _router: Router,
    private reportService: ApiReportsService,
    private alertService: SweetAlertService,
    private translate: TranslateService,
    private activatedRoute: ActivatedRoute,
    private reportSkeletonCategoriesService: ReportSkeletonCategoriesService,
    private store: Store<{ toggle: string }>
  ) { 
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  async ngOnInit(): Promise<void> {
     this.activatedRoute.queryParams.subscribe(async (data) => {
       if (data.reportSkeletonCategoryId !== undefined) {
         this.reportSkeletonCategoryId=data.reportSkeletonCategoryId;
         await this.getReportSkeletonCategoryByIdAsync(this.reportSkeletonCategoryId)
        }
    });
    this.isCreateVisible = false;
    this.spinner = false;
    this.reportCategories = [];
    this.reportSkeletonData = await this.reportService.getReportSkeletonsAsync();
    this.reportsList = await this.reportService.getReportsAsync();
    this.reportSkeletonData = this.reportSkeletonData.filter(r => r.active === true);
    this.reportCategories = new Set(this.reportSkeletonData.map(item => item.category));
  }

  public async getReportSkeletonCategoryByIdAsync(reportSkeletonCategoryId:string){
    this.reportSkeletonCategory = await this.reportSkeletonCategoriesService.getReportSkeletonCategoryByIdAsync(reportSkeletonCategoryId);
    this.reportSkeleton_CategoryName = this.reportSkeletonCategory.name;
    this.reportSkeleton_Subcategories = this.reportSkeletonCategory.reportSkeleton_Subcategories.sort((a, b) => a.order - b.order);
    this.createReportTreeList();
  }

  public createReportTreeList(){
    this.navList = [];
    this.reportSkeleton_Subcategories.forEach((res) => {
      const newItem = {
        id: res.id,
        Title: res.name,
        Children: [],
      };
      newItem.Children = Array.from(res.reportSkeleton_Subcategory_Reports
        ).sort((a, b) => a.order - b.order)
        .map((child) => ({
        id: child.reportSkeleton.id,
        Title: child.reportSkeleton.defaultTitle,
      }));
      this.navList.push(newItem);
    });
    this.dataSource.data = this.navList;
  }

  checkForChildren(object: any){
    if (object.hasOwnProperty('Children')) {
      return true;
    } else {
      return false;
    }
  }

  itemClicked(node: ReportTreeControl) {
    this.selectedItem = node;
    this.getSelectedReportSkeletonReports(node);
  }

  public getSelectedReportSkeletonReports(item){
    this.reportSkeletonId = item.id;
    this.isCreateVisible = true;
    const filter= this.reportsList.filter(r => r.reportSkeletonId == item.id) 
    this.reportDetails = new MatTableDataSource(filter)
  }

  public getCategoryReportSkeletons(name: string){
    return this.reportSkeletonData.filter(dd => dd.category === name);
  }

  public openEditReportsPage(mode: string, id: number){
    if(mode === 'update')
    {
      this._router
      .navigate(['/reports/update/' + id]);
    }
    else
    {
      this._router
      .navigate(['/reports/create/' + id]);
    }
  }

  public filterReport(event) {
    let filter = (event.target as HTMLInputElement).value;
    this.reportDetails.filter = filter;
  }
  
  onReportChange(event: any, id: any) {
    if (event.checked) {
      this.reportSelection.select(id);
    } else {
      this.reportSelection.deselect(id);
    }
  }

  masterToggle() {
    if (this.isAllSelected()) {
      this.reportSelection.clear();
    } else {
      this.reportSelection.select(...this.reportDetails.data.map(row => row.id));
    }
  }

  isAllSelected() {
    const data = this.reportDetails.data;  
    const numSelected = this.reportSelection.selected.length;
    const numRows = data.length;
    return numSelected > 0 && numSelected === numRows;
  }

  async deleteReports() {
    const selectedIds: string[] = this.reportSelection.selected;
    const options: ReportsDeleteOptions = { reportIds: selectedIds };
    await this.reportService.deleteReportsAsync(options);
    this.reportSelection.clear();
    this.reportDetails.data = this.reportDetails.data.filter(r=>!selectedIds.includes(r.id));
  }

}