import { TemplatePortal } from '@angular/cdk/portal';
import {
  Component,
  HostListener,
  Input,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { DatePipe } from '@angular/common';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { DocumentDisplayMode } from 'src/app/_DtoModels/Document/DocumentDisplayMode';
import { DocumentViewModel } from 'src/app/_DtoModels/Document/DocumentViewModel';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ApiDocumentTypesService } from 'src/app/_Services/QTD/DocumentTypes/api.documentTypes.Service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { DocumentTypeViewModel } from 'src/app/_DtoModels/DocumentType/DocumentTypeViewModel';
import { ApiDocumentsService } from 'src/app/_Services/QTD/Documents/api.document.Service';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';

@Component({
  selector: 'app-document-table',
  templateUrl: './document-table.component.html',
  styleUrls: ['./document-table.component.scss'],
})
export class DocumentTableComponent implements OnInit {
  @ViewChild(MatSort) sort: MatSort;
  @Input() inputDocumentType: string;
  @Input() inputLinkedDataId: string;
  @Input() inputLinkedDataType: string;
  @Input() handleLoad: (e) => void;
  @Input() handleNewDocumentClick: (e) => void;
  @Input() handleFilterClick: (e) => void;
  @Input() handleSearchUpdate: (e) => void;
  @Input() handleSortClick: (e) => void;
  @Input() handleLinkedDataClick: (e) => void;
  documents: DocumentViewModel[];
  documentTypes: DocumentTypeViewModel[];
  inputDocumentTypeId: string;
  filterDocumentTypes: DocumentTypeViewModel[] = [];
  search: string;
  filteredDocuments: DocumentViewModel[] = [];
  modes = DocumentDisplayMode;
  modalType: DocumentDisplayMode;
  modalDescription: string;
  modalHeader: string;
  public documentStorageList;
  tableColumns: Array<string>;
  isDropdownOpen: boolean = false;
  documentInfo: DocumentViewModel;
  documentToViewEdit: DocumentViewModel;

  constructor(
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    public dialog: MatDialog,
    private docTypeService: ApiDocumentTypesService,
    private docService: ApiDocumentsService,
    private router: Router,
    private DataBroadcastService: DataBroadcastService,
    private employeeService:EmployeesService
  ) {}

  ngOnInit(): void {
    this.tableColumns = [
      'Check Box',
      'fileName',
      'documentTypeName',
      'dateAdded',
      'linkedDataName',
      'Action',
    ];
    this.loadAsync();
    this.search = '';
  }

  _handleNewDocumentClick(e: any) {
    if (
      this.handleNewDocumentClick &&
      typeof this.handleNewDocumentClick === 'function'
    ) {
      this.handleNewDocumentClick(e);
    }
  }

  _handleFilterClick(e: any) {
    if (
      this.handleFilterClick &&
      typeof this.handleFilterClick === 'function'
    ) {
      this.handleFilterClick(e);
    }
  }

  _handleLoad(e: any) {
    if (this.handleLoad && typeof this.handleLoad === 'function') {
      this.handleLoad(e);
    }
  }

  _handleSearchUpdate(e: any) {
    if (
      this.handleSearchUpdate &&
      typeof this.handleSearchUpdate === 'function'
    ) {
      this.handleSearchUpdate(e);
    }
  }

  _handleSortClick(e: any) {
    if (this.handleSortClick && typeof this.handleSortClick === 'function') {
      this.handleSortClick(e);
    }
  }

  _handleLinkedDataClick(e: any) {
    if (
      this.handleLinkedDataClick &&
      typeof this.handleLinkedDataClick === 'function'
    ) {
      this.handleLinkedDataClick(e);
    }
  }

  _filterDocuments() {
    this.filteredDocuments = this.documents;

    if (this.filterDocumentTypes.length > 0) {
      this.filteredDocuments = this.filteredDocuments.filter((x) =>
        this.filterDocumentTypes
          .map((d) => d.id)
          .includes(x.documentTypeId.toString())
      );
    }
    if (this.search) {
      this.filteredDocuments = this.filteredDocuments.filter((item) =>
        item.fileName.toLowerCase().includes(this.search.toLowerCase())
      );
    }
    this.documentStorageList = new MatTableDataSource(this.filteredDocuments);
    this.documentStorageList.sort = this.sort;
  }

  async loadAsync() {
    this.documentTypes = await this.docTypeService.getAllActiveAsync();

    if (this.inputDocumentType) {
      this.inputDocumentTypeId = this.documentTypes.find(
        (x) => x.name === this.inputDocumentType
      ).id;
    }

    if (
      this.inputDocumentType &&
      !this.inputLinkedDataId &&
      !this.inputLinkedDataType
    ) {
      this.documents = await this.docService.getActiveByDocumentTypeAsync(
        this.inputDocumentTypeId
      );
    } else if (
      this.inputLinkedDataId &&
      this.inputLinkedDataType &&
      !this.inputDocumentType
    ) {
      this.documents = await this.docService.getActiveByLinkedDataAsync(
        this.inputLinkedDataType,
        this.inputLinkedDataId
      );
    } else if (
      this.inputLinkedDataId &&
      this.inputLinkedDataType &&
      this.inputDocumentType
    ) {
      this.documents =
        await this.docService.getActiveByLinkedDataAndDocumentTypeAsync(
          this.inputDocumentTypeId,
          this.inputLinkedDataType,
          this.inputLinkedDataId
        );
    } else {
      this.documents = await this.docService.getAllActiveAsync();
    }
    this.documentStorageList = new MatTableDataSource(this.documents);
    this.documentStorageList.sort = this.sort;
    this._handleLoad(null);
  }

  newDocumentClick(templateRef: any) {
    this.openflyPanel(templateRef, DocumentDisplayMode.Add);
    this._handleNewDocumentClick(null);
  }

  filterClick(e: any) {
    this._handleFilterClick(e);
    this.isDropdownOpen = false;
    this._filterDocuments();
  }

  clearFilterSelection() {
    this.filterDocumentTypes = [];
    this.isDropdownOpen = false;
    this._filterDocuments();
  }

  searchUpdate(event: any) {
    this._handleSearchUpdate(event);
    this.search = event.target.value;
    this._filterDocuments();
  }

  sortClick(e: any) {
    this._handleSortClick(e);
  }

 async linkedDataClick(linkedDataType: string, linkedDataId: string) {
    switch (linkedDataType.toLowerCase()) {
      case 'employees':
        this.router
          .navigate(['/implementation/employee/edit/' + linkedDataId])
          .then((_) => this.DataBroadcastService.empFormMode.next('edit'));
        break;
      case 'positions':
        this.router.navigate([`/my-data/positions/details/${linkedDataId}`]);
        break;
      case 'ilas':
        this.router.navigate(['/dnd/ila/create'], {
          queryParams: { data: linkedDataId },
        });
        break;
      case 'trainingprogramreviews':
        this.router.navigate(['/evaluation/trainingprogram-review/create'], {
          queryParams: { data: linkedDataId },
        });
        break;
      case 'tool':
        this.router.navigate([`my-data/tools/detail/${linkedDataId}`], {
        });
        break;
      case 'tasklistreview':
        this.router.navigate([`/evaluation/task-list-review/edit/${linkedDataId}`]);
        break;
      case 'classscheduleemployees':
       var employee = await this.employeeService.getEmployeeByClassScheduleIdAsync(linkedDataId);
       this.router.navigate(['/implementation/employee/edit/' + employee.id])
      .then((_) => this.DataBroadcastService.empFormMode.next('edit'));
      break;
    }
    this._handleLinkedDataClick(null);
  }

  openflyPanel(templateRef: any, mode: DocumentDisplayMode) {
    this.modalType = mode;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  public viewEditDocumentStorage(
    document: DocumentViewModel,
    templateRef: any,
    mode: DocumentDisplayMode
  ) {
    this.documentToViewEdit = document;
    this.openflyPanel(templateRef, mode);
  }

  public deleteDocumentStorage(templateRef: any, event) {
    this.modalHeader = 'Delete Document';
    this.modalDescription = `You are selecting to delete document`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    this.documentInfo = event;
  }

  getFilterCheckedValues(id: string) {
    const checkValue =
      this.filterDocumentTypes &&
      this.filterDocumentTypes.filter((d) => d.id === id);
    if (checkValue && checkValue.length > 0) {
      return true;
    } else {
      return false;
    }
  }

  onFilterValueChange(documentTypeId: string) {
    const index = this.filterDocumentTypes?.findIndex(
      (x) => x.id == documentTypeId
    );
    if (index !== -1) {
      this.filterDocumentTypes.splice(index, 1);
    } else {
      const documentType = this.documentTypes?.find(
        (x) => x.id == documentTypeId
      );
      this.filterDocumentTypes.push(documentType);
    }
  }

  async toggleDropdown(event: Event) {
    event.stopPropagation();
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  @HostListener('document:click', ['$event'])
  closeDropdownOnClickOutside(event: Event): void {
    if (this.isDropdownOpen && !event.target['closest']('.dropdown')) {
      this.isDropdownOpen = false;
    }
  }

  closeFlyPanel = () => {
    this.flyPanelService.close();
  };

  handleAddedDocument = (addedDocument: DocumentViewModel) => {
    this.documents.push(addedDocument);
    this._filterDocuments();
  };

  async handleDeletedDocumentAsync(e: any) {
    let deletedStatusCode = await this.docService.deleteActiveAsync(
      this.documentInfo.id
    );
    if ((deletedStatusCode = 200)) {
      const index = this.documents?.findIndex(
        (x) => x.id == this.documentInfo.id
      );
      if (index !== -1) {
        this.documents.splice(index, 1);
      }
      this._filterDocuments();
    }
  }

  handleUpdatedDocument = (updatedDocument: DocumentViewModel) => {
    const index = this.documents.findIndex(
      (doc) => doc.id === updatedDocument.id
    );
    if (index !== -1) {
      this.documents[index] = updatedDocument;
    }
    this.documentStorageList = new MatTableDataSource(this.documents);
  };

  async getActiveFileAsync(doc: DocumentViewModel) {
    let base64String = await this.docService.getActiveFileAsync(doc.id);
    const blob = this.base64ToBlob(base64String);
    const blobUrl = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = blobUrl;
    a.download = doc.fileName;
    a.click();
    URL.revokeObjectURL(blobUrl);
  }

  private base64ToBlob(base64String: string): Blob {
    const byteCharacters = atob(base64String);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray]);
  }
}
