import {  Component, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatLegacyTable as MatTable, MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ImportDatum_VM } from '@models/ImportCSV/ImportDatum_VM';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { saveAs } from 'file-saver';
import { ImportService } from 'src/app/_Services/QTD/import.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { cloneDeep } from 'lodash';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-import-csv-wizard',
  templateUrl: './import-csv-wizard.component.html',
  styleUrls: ['./import-csv-wizard.component.scss']
})
export class ImportCsvWizardComponent implements OnInit {
  @Input() type: string;
  @Input() validationResult: any;
  @Input() confirmResult: any[];
  @Input() uploadColumns :string[]=[]
  @Input() template : any;
  @Output() OnAttachFile: EventEmitter<any> = new EventEmitter();
  @Output() OnConfirmUpload: EventEmitter<any> = new EventEmitter();
  @Output() OnBackToUpload: EventEmitter<any> = new EventEmitter();
  isImportVisible:boolean;
  file:File;
  fileError: string = "";
  columnsForTable:string[];
  fileName: string;
  searchString: string='';
  isValidFile: boolean;
  isReturnFile :boolean=false;
  public dragAreaClass: String;
  isConfirmed :boolean;
  @ViewChild(MatTable) table!: MatTable<any>; 
 
  constructor(
    private translate: TranslateService,
    private databroadcastService: DataBroadcastService,
    private store: Store<{ toggle: string }>,
    private importService: ImportService,
    public dialog: MatDialog,
    private dynamicLabelPipe: DynamicLabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
    this.databroadcastService.ShowMenuSideBar.next(false);
  }

  ngOnInit(): void {
    this.dragAreaClass = "dragarea";
     this.isValidFile = false;
     this.isImportVisible = true;
     this.isConfirmed = false;
     this.initializeExtraColumns();
     this.store.dispatch(sideBarClose());
  }
  @HostListener("dragover", ["$event"]) onDragOver(event: any) {
    this.dragAreaClass = "droparea";
    event.preventDefault();
    event.stopPropagation();
  }
  @HostListener("dragenter", ["$event"]) onDragEnter(event: any) {
    this.dragAreaClass = "droparea";
    event.preventDefault();
  }
  @HostListener("dragend", ["$event"]) onDragEnd(event: any) {
    this.dragAreaClass = "dragarea";
    event.preventDefault();
  }
  @HostListener("dragleave", ["$event"]) onDragLeave(event: any) {
    this.dragAreaClass = "dragarea";
    event.preventDefault();
  }
  @HostListener("drop", ["$event"]) onDrop(event: any) {
    this.dragAreaClass = "dragarea";
    event.preventDefault();
    event.stopPropagation();
    if (event.dataTransfer.files) {
      let file: File = event.dataTransfer.files[0];
      this.getFileData(file);
    }
  }

  initializeExtraColumns(){
    this.columnsForTable = cloneDeep(this.uploadColumns);
    this.columnsForTable.push("ValidationErrors");
  }

    onFileSelected(event: any){
      if(event.target.files.length>0){
        this.fileName  = "";
        const file = event.target.files[0];
        this.getFileData(file);
        event.target.value = '';
    }
  }

  getFileData(file: File){
    this.file = file;
    this.fileName = this.file.name;
    if (this.fileName.endsWith('.csv')) {
      this.fileError = null;
      this.isValidFile = true;
    } else {
      this.fileError = 'Please select a CSV file.';
      this.isValidFile = false;
    }
  }

  validateCSVFile(){
    this.OnAttachFile.emit(this.file);
    this.searchString="";
    this.isReturnFile=false;
  }

  async getTemplate(){
    let name = await this.dynamicLabelPipe.transform(this.type);
    saveAs(
      this.template,
      `${name} Import Template`
    );
  }
  openDialog(templateRef: any){
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  setDataSource(){
    var filtereData = this.filterData();
    return new MatTableDataSource(filtereData);
  }

   getColumnValue(row:ImportDatum_VM,column:string){
    for (const key in row) {
      if (key.toLowerCase() === column.replace(/\s/g, '').toLowerCase()) {
        if(column.toLowerCase() == "validationerrors"){
          var errors = row[key].map(x=>x.error);
          return errors.length>0 ? row[key].map(x=>x.error).toString() : "No Errors";
        }
        else{
          return row[key].toString();
        }
      }
    }
    return null;
  }


  searchFilter(event: any) {
    this.searchString = event.target.value;
    this.table.dataSource= new MatTableDataSource(this.filterData());
  }
  clearFilter() {
    this.searchString='';
    this.table.dataSource= new MatTableDataSource(this.filterData());
  }
  filterData(){
    var dataToFilter = this.isConfirmed?this.confirmResult : this.validationResult?.data;
    return dataToFilter.filter(obj =>{
        var validationErrors =obj.validationErrors.length>0 ? obj.validationErrors.map(x=>x.error).toString() : "No Errors";
        var isFiltered = validationErrors.toLowerCase().includes(this.searchString?.toLowerCase()) || Object.values(obj).some(value =>value?.toString().toLowerCase().includes(this.searchString?.toLowerCase()));
        return this.searchString == '' || isFiltered;
      } 
    );
  }
  getErrorRowsCount(){
    var rowsHavingError = this.validationResult?.data?.filter(x=>x.validationErrors.length>0).length;
    return rowsHavingError;
  }
  csvDownloadChange(event:any){
    this.isReturnFile=event.checked;
  }
  goToUpload(){
    this.dialog.closeAll();
    this.isImportVisible=true ;
    this.isConfirmed=false;
    this.validationResult =null;
    this.confirmResult=[];
    this.isValidFile=false;
    this.file=null;
    this.fileName='';
    this.OnBackToUpload.emit();
  }
  
}
