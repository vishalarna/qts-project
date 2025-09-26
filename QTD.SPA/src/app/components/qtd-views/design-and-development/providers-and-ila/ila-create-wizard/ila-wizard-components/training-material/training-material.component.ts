import { UploadDialogueComponent } from './upload-dialogue/upload-dialogue.component';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DeleteSegmentComponent } from './delete-segment/delete-segment.component';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { SegmentService } from 'src/app/_Services/QTD/segment.service';
import { Segment } from 'src/app/_DtoModels/Segment/Segment';
import { SegmentOptions } from 'src/app/_DtoModels/Segment/SegmentOptions';
import { select, Store } from '@ngrx/store';
import { SubSink } from 'subsink';
import { skip } from 'rxjs/operators';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { ILA_UploadOptions } from 'src/app/_DtoModels/ILA/ILA_UploadOptions';
import { MatSort } from '@angular/material/sort';
import { IlaResourcesService } from 'src/app/_Services/QTD/ila-resources.service';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-training-material',
  templateUrl: './training-material.component.html',
  styleUrls: ['./training-material.component.scss']
})
export class TrainingMaterialComponent implements OnInit, OnDestroy {
  @ViewChild(MatSort) sort: MatSort;
  @Output() loadingEvent = new EventEmitter<boolean>();
  lsegs!: Array<any>;
  files: any[] = [];
  isCustomEO: boolean = false;
  dataForReview: any;
  isEmpty = true;
  subscriptions = new SubSink();
  deliveryMethod: string = "N/A";
  ilaID: any;
  ilaResourceId: string;
  deleteSpinner:boolean[] = [];
  downloadSpinner:boolean[] = [];
  deliveryMethodBit: boolean = false;
  MAX_SIZE: number = 12000000;
  options = new ILA_UploadOptions();

  uploadedFiles: any = [];
  showSpinner = false;
  showFileLoader = false;
  tableColumns: Array<string>;
  public trainingMaterialList;
  header : string;
  description :string;
  isEditILAResource: boolean = false;
  editILAResourceData: any;
  editILAResourceId: string;

  acceptedTypes = "application/msword," +                        // Word (.doc)
  "application/vnd.openxmlformats-officedocument.wordprocessingml.document," +  // Word (.docx)
  "application/vnd.ms-excel," +                                 // Excel (.xls)
  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet," +  // Excel (.xlsx)
  "application/vnd.ms-powerpoint," +                            // PowerPoint (.ppt)
  "application/vnd.openxmlformats-officedocument.presentationml.presentation," +  // PowerPoint (.pptx)
  "application/zip," +                                          // ZIP
  "application/pdf," +                                          // PDF
  "video/mp4," +                                                // Video (.mp4)
  "video/x-m4v," +                                              // Video (.m4v)
  "application/x-zip-compressed";

  @Output() previewEvent = new EventEmitter<string>();
  @Output() training_material = new EventEmitter<any>();
  @Input() editIlaCheck:any;
  @Input() mode: string;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private getServices: IlaService,
    public vcr: ViewContainerRef,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    public segmentService: SegmentService,
    private saveStore: Store<{ saveIla: any }>,
    private ilaService: IlaService,
    private dataBroadcastService: DataBroadcastService,
    private ilaResourceService:IlaResourcesService,
    private labelPipe: LabelReplacementPipe,
    private store: Store<{ saveIla: any }>,

  ) {

  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();

  }

  ngOnInit(): void {
    this.loadAsync();
  }

  async loadAsync() {
    try {
      this.loadingEvent.emit(true);
      this.tableColumns = ['iLAId', 'title', 'comments', 'edit', 'remove'];
  
      await new Promise<void>((resolve, reject) => {
        const subscription = this.store.select('saveIla').subscribe({
          next: (data) => {
            if (
              data?.saveData?.result !== undefined &&
              data?.saveData?.result !== null
            ) {
              this.ilaID = data.saveData.result.id;
              this.deliveryMethod = data.saveData.result.deliveryMethodName || 'N/A';
              this.deliveryMethodBit = 
                this.deliveryMethod === 'Self-Study (e.g., paperbased)' ||
                this.deliveryMethod === 'Structured self-study activities';
  
              if (this.ilaID !== undefined) {
                this.getSegments();
              }
            }
            resolve(); 
          },
          error: (err) => {
            reject(err); 
          },
        });
  
        this.subscriptions.sink = subscription;
      });
  
      this.subscriptions.sink = this.dataBroadcastService.segmentSaved.subscribe(() => {
        this.getSegments();
      });
  
      if (this.ilaID !== undefined) {
        await this.getUploadedFiles();
        await this.getILaResourceAsync();
      }
    } catch (error) {
      this.loadingEvent.emit(false);
    } finally {
      this.loadingEvent.emit(false);
    }
  }
  

 async getILaResourceAsync(){
    let resources = await this.ilaResourceService.getAllAsync(this.ilaID);
    this.trainingMaterialList = new MatTableDataSource(resources);
    this.trainingMaterialList.sort = this.sort;
  }
  getUploadedFiles() {
    this.showFileLoader = true;
    this.uploadedFiles = [];
    this.deleteSpinner =[];
    this.downloadSpinner = [];
    var index = 0;
    this.ilaService.getUploadedFiles(this.ilaID).then((res: any) => {
      res.forEach((element: any) => {
        this.uploadedFiles.push(element);
        this.deleteSpinner.push(false);
        this.downloadSpinner.push(false);
      });
      this.showFileLoader = false;
    }).catch((error: any) => {
      this.showFileLoader = false;
      this.alert.errorToast("Error Fetching Files");
    })
  }

  buildObject(res: any) {

  }

  async getSegments() {
    // await this.segmentService.getAll().then((res: any) => {
    //   
    //   this.lsegs = res;
    //   this.isEmpty = false;
    // }).catch((err: any) => {
    //   console.error(err);
    // })

    await this.ilaService.getLinkedSegments(this.ilaID).then((res: Segment[]) => {
      
      this.lsegs = res;
      this.isEmpty = false;
    }).catch((err: any) => {
    })
  }

  deleteFile(file: any,index:any) {
    this.deleteSelectedFile(file.ilaId, file.id, file.fileName,index);
  }

  async deleteSelectedFile(ilaId: any, fileId: any, name: any,index:any) {
    this.deleteSpinner[index]= true;
    await this.ilaService.deleteUploadedFile(ilaId, fileId).then((res: any) => {
      this.alert.successToast("File " + name + " Deleted Successfully");
      this.deleteSpinner[index]= false;
      this.getUploadedFiles();
    }).catch((err: any) => {
      this.deleteSpinner[index]= false;
      this.alert.errorToast("Error Deleting File " + name);
    })
  }

  async downloadFile(file: any,index:any) {
    // const linkSource = file.fileAsBase64;
    // const downloadLink = document.createElement("a");
    // const fileName = file.fileName;
    // downloadLink.href = linkSource;
    // downloadLink.download = fileName;
    // downloadLink.click();
    this.downloadSpinner[index] =true;
    await this.ilaService.getDownloadData(this.ilaID, file.id).then((res: any) => {
      const linkSource = res.fileAsBase64;
      const downloadLink = document.createElement("a");
      const fileName = res.fileName;
      downloadLink.href = linkSource;
      this.downloadSpinner[index] =false;
      downloadLink.download = fileName;
      downloadLink.click();
    }).catch((err) => {
      this.downloadSpinner[index] =false;
    })

  }

  openFlyInPanel(templateRef: any, index: any) {
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.dataForReview = this.lsegs[index];
    this.flyPanelSrvc.open(portal);
  }

  openSegmentPanel(templateRef: any) {
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  openFlyPanel(templateRef: any){
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  async drop(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.lsegs, event.previousIndex, event.currentIndex);
    await this.getServices.updateLinkedSegmentsOrder(this.ilaID,this.lsegs.map((x)=>x.id)).then((res: any) => {
      this.alert.successToast("Segments order saved successfully");
    }).catch((err: any) => {
      this.alert.errorAlert(err);
    });
  }
  onSelect(event: any) {
    
    if(!this.acceptedTypes.includes(event.addedFiles[0].type)){
      this.alert.errorAlert("Please Enter A valid file type");
      return;
    }
    if (event.addedFiles[0].size < this.MAX_SIZE) {
      
      
      this.files.push(event.addedFiles[0]);
      this.readAndUploadFile(event.addedFiles[0]);
    }
    else {
      this.alert.errorToast("Please select a valid file with size less then 10MB");
    }
  }

  readAndUploadFile(file: any) {

    this.options.iLAId = this.ilaID;
    let reader = new FileReader();
    reader.onload = () => {
      this.options.uploadFiles.push({ fileAsBase64: reader.result?.toString() ?? "", fileName: file.name, fileSize: file.size, fileType: file.type });

    }

    reader.readAsDataURL(file);

  }

  uploadFiles() {
    this.showSpinner = true;
    var count = this.files.length;
    this.options.uploadFiles.forEach(async (file: any) => {
      var toUpload = new ILA_UploadOptions();
      toUpload.iLAId = this.ilaID;
      toUpload.uploadFiles = [];
      toUpload.uploadFiles.push(file);
      await this.ilaService.uploadFile(this.ilaID, toUpload).then((res: any) => {
        count--;
        if (count === 0) {
          this.files = [];
          this.options.uploadFiles = [];
          this.alert.successToast("Files Uploaded Successfully");
          this.showSpinner = false;
          this.getUploadedFiles();
        }
      }).catch((err: any) => {
        this.alert.errorToast("Failed to upload Files");
        this.showSpinner = false;
      })
    })

  }

  dataURLtoFile(dataurl: any, filename: any) {

    var arr = dataurl.split(','),
      mime = arr[0].match(/:(.*?);/)[1],
      bstr = atob(arr[1]),
      n = bstr.length,
      u8arr = new Uint8Array(n);

    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime });
  }

  onRemove(event: any) {
    
    this.files.splice(this.files.indexOf(event), 1);
    
    this.options.uploadFiles.splice(this.options.uploadFiles.indexOf(event), 1);
    
  }

  deleteEmp(item: any) {
    const dialogRef = this.dialog.open(DeleteSegmentComponent, {
      hasBackdrop: true,
    });
    //dialogRef.componentInstance.empId = id;
    this.subscriptions.sink = dialogRef.afterClosed().subscribe((result: any) => {
      
      if (result === true) {
        var options = new SegmentOptions();
        options.actionType = "delete";
        this.deleteSegment(item.id, options);
      }
    });
  }

  async deleteSegment(id: any, options: SegmentOptions) {
    this.segmentService.delete(id, options).then((res: any) => {
      this.dataBroadcastService.updateSegments.next({id:id,todo:"DELETE"});
      this.getSegments();
      this.alert.successToast(res);
    }).catch((err: any) => {
      this.alert.errorToast(`Error Deleting ${err}`);
    })
  }

  OnClickUpload() {
    const dialogRef = this.dialog.open(UploadDialogueComponent, {
      width: '600px',
      height: '550px',
      hasBackdrop: true,
      disableClose: true,
    });
    this.subscriptions.sink = dialogRef.afterClosed().subscribe((res: any) => {
      
    })
  }

  refreshSegments(event: any) {
    
    this.getSegments();
  }

  searchUpdate(event: any) {
    const searchText = event.target.value.toLowerCase();
    this.trainingMaterialList.filter = searchText.trim().toLowerCase();
  }

  async removeILAResource(row: any, templateRef: any) {
    this.ilaResourceId = row.id;
    this.header = "Remove " + await this.labelPipe.transform('ILA') + " Resource";
    this.description = "You are selecting to remove the following Resource from this " + await this.labelPipe.transform('ILA') + " :" + "\n\n " + row.title ;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    this.subscriptions.sink = dialogRef.afterClosed().subscribe((res: any) => {
    })
  }

  async confirmRemoveResourceILA(){
    await this.ilaResourceService.removeILAResourceByILAId(this.ilaResourceId).then(async (_)=>{
      this.alert.successToast(await this.labelPipe.transform('ILA') + " Resource Deleted Successfully");
      this.getILaResourceAsync();
    })
  }

  addILAResouce(templateRef: any){
    this.isEditILAResource = false;
    this.editILAResourceData = null;
    this.openFlyPanel(templateRef);
  }

  async editILAResource(row: any, templateRef: any){
    this.isEditILAResource = true;
    this.editILAResourceId = row?.id;
    this.openFlyPanel(templateRef);
    this.editILAResourceData = this.trainingMaterialList.data.find(x => x.id == row.id);
  }
}

