import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnInit,
  HostListener,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { ApiDocumentTypesService } from 'src/app/_Services/QTD/DocumentTypes/api.documentTypes.Service';
import { DocumentDisplayMode } from 'src/app/_DtoModels/Document/DocumentDisplayMode';
import { DocumentTypeViewModel } from 'src/app/_DtoModels/DocumentType/DocumentTypeViewModel';
import { DocumentViewModel } from 'src/app/_DtoModels/Document/DocumentViewModel';
import { LinkToDataOption } from 'src/app/_DtoModels/DocumentType/LinkToDataOption';
import { ApiDocumentsService } from 'src/app/_Services/QTD/Documents/api.document.Service';
import { DocumentToUpload } from 'src/app/_DtoModels/Document/DocumentToUpload';
import { DocumentCreateOptions } from 'src/app/_DtoModels/Document/DocumentCreationOptions';
import { DocumentUpdateOptions } from 'src/app/_DtoModels/Document/DoumentUpdateOptions';
import { TreeItemViewModel } from 'src/app/_DtoModels/TreeVMs/TreeItemViewModel';

@Component({
  selector: 'app-fly-panel-document-storage',
  templateUrl: './fly-panel-document-storage.component.html',
  styleUrls: ['./fly-panel-document-storage.component.scss'],
})
export class FlyPanelDocumentStorageComponent implements OnInit {
  @Input() inputDocumentType: string;
  @Input() inputLinkedDataId: string;
  @Input() inputLinkedDataType: string;
  @Input() documentDisplayMode: DocumentDisplayMode;
  @Input() document: DocumentViewModel;
  @Input() handleDocumentTypeClick: (e) => void | undefined;
  @Input() handleLinkToDataClick: (e) => void | undefined;
  @Input() handleSaveClick: (e) => void | undefined;
  @Input() handleSaveAndAddNewClick: (e) => void | undefined;
  @Input() handleCloseClick: () => void | undefined;
  documentTypes: DocumentTypeViewModel[];
  inputDocumentTypeId: string;
  hasInputLinkedData: boolean;
  inputLinkedDataName: string;
  modes = DocumentDisplayMode;
  attachedDocument: DocumentToUpload = new DocumentToUpload();
  isFileAttached: boolean = false;
  linkedDataId: string;
  editedLinkedData: boolean;
  originalLinkedDataId: string = null;
  isLinkedToData: boolean = false;
  public documentStorageForm: UntypedFormGroup;
  public dragAreaClass: string = 'dragarea';
  error: string;
  documentTypeChanged = new EventEmitter<any>();
  linkToDataTreeItem: TreeItemViewModel = null;
  public spinner: boolean;
  saveSpinner : boolean;
  saveAndNewSpinner : boolean;

  constructor(
    private fb: UntypedFormBuilder,
    private docTypesService: ApiDocumentTypesService,
    private documentService: ApiDocumentsService
  ) {}

  ngOnInit(): void {
    this.initializeDocumentStorageForm();
    this.loadData();
    this.saveSpinner = false;
    this.saveAndNewSpinner = false;
  }

  _handleDocumentTypeClick(e: any) {
    if (
      this.handleDocumentTypeClick &&
      typeof this.handleDocumentTypeClick === 'function'
    ) {
      this.handleDocumentTypeClick(e);
    }
  }

  _handleLinkToDataClick(e: any) {
    if (
      this.handleLinkToDataClick &&
      typeof this.handleLinkToDataClick === 'function'
    ) {
      this.handleLinkToDataClick(e);
    }
  }

  _handleSaveClick(e: any) {
    if (this.handleSaveClick && typeof this.handleSaveClick === 'function') {
      this.handleSaveClick(e);
    }
  }

  _handleSaveAndAddNewClick(e: any) {
    if (
      this.handleSaveAndAddNewClick &&
      typeof this.handleSaveAndAddNewClick === 'function'
    ) {
      this.handleSaveAndAddNewClick(e);
    }
  }

  _handleCloseClick() {
    if (this.handleCloseClick && typeof this.handleCloseClick === 'function') {
      this.handleCloseClick();
    }
  }

  initializeDocumentStorageForm() {
    this.documentStorageForm = this.fb.group({
      documentType: new UntypedFormControl(null, [Validators.required]),
      comments: new UntypedFormControl(null),
      linkedDataName: new UntypedFormControl(null),
    });
  }

  async loadData() {
    await this.docTypesService.getAllActiveAsync().then(async (res) => {
      this.documentTypes = res;
    });

    if (this.inputDocumentType) {
      this.inputDocumentTypeId = this.documentTypes.find(
        (x) => x.name === this.inputDocumentType
      ).id;
    }

    this.hasInputLinkedData =
      this.inputLinkedDataId !== undefined &&
      this.inputLinkedDataType !== undefined;

    if (this.hasInputLinkedData) {
      this.documentTypes = this.documentTypes.filter(
        (x) => x.linkedDataType === this.inputLinkedDataType
      );

      this.inputLinkedDataName =
        await this.documentService.getLinkedDataNameAsync(
          this.inputLinkedDataType,
          this.inputLinkedDataId
        );
    }

    this.initializeDocumentStorageFormState();
  }

  async initializeDocumentStorageFormState() {
    this.isFileAttached = this.documentDisplayMode !== DocumentDisplayMode.Add;

    switch (this.documentDisplayMode) {
      case DocumentDisplayMode.Add:
        if (this.inputDocumentTypeId) {
          this.documentStorageForm.get('documentType').disable();
          this.documentStorageForm
            .get('documentType')
            .setValue(this.inputDocumentTypeId);
          await this.documentTypeClick(this.inputDocumentTypeId);
        }

        if (this.hasInputLinkedData) {
          this.documentStorageForm.get('linkedDataName').disable();
          this.documentStorageForm
            .get('linkedDataName')
            .setValue(this.inputLinkedDataName);
          this.linkToDataClick(this.inputLinkedDataId);
        }
        break;

      case DocumentDisplayMode.Edit:
        if (this.inputDocumentTypeId) {
          this.documentStorageForm.get('documentType').disable();
          this.documentStorageForm
            .get('documentType')
            .setValue(this.inputDocumentTypeId);
          this._handleDocumentTypeClick(this.inputDocumentTypeId);
        } else {
          this.documentStorageForm
            .get('documentType')
            .setValue(this.document.documentTypeId);
          this._handleDocumentTypeClick(this.document.documentTypeId);
        }

        if (this.hasInputLinkedData) {
          this.documentStorageForm.get('linkedDataName').disable();
          this.documentStorageForm
            .get('linkedDataName')
            .setValue(this.inputLinkedDataName);
          this.linkToDataClick(this.inputLinkedDataId);
        } else {
          this.documentStorageForm.get('linkedDataName').disable();
          this.documentStorageForm
            .get('linkedDataName')
            .setValue(this.document.linkedDataName);
          this.linkToDataClick(this.document.linkedDataId);
        }

        this.documentStorageForm
          .get('comments')
          .setValue(this.document.comments);
        break;

      case DocumentDisplayMode.View:
        this.documentStorageForm.disable();
        this.documentStorageForm
          .get('documentType')
          .setValue(this.document.documentTypeId);

        this.documentStorageForm
          .get('linkedDataName')
          .setValue(this.document.linkedDataName);

        this.documentStorageForm
          .get('comments')
          .setValue(this.document.comments);
        break;
    }
  }

  async documentTypeClick(value: any) {
    this._handleDocumentTypeClick(value);

    if (!this.hasInputLinkedData) {
      this.spinner = true;

      this.linkToDataClick(null);
      this.editedLinkedData = true;
      
      this.linkToDataTreeItem = null;
      this.documentTypeChanged.emit();
      await this.docTypesService
        .getActiveLinkToDataOptionsAsync(value)
        .then((res) => {
          this.linkToDataTreeItem = res;
          this.spinner = false;
        });
    }
  }

  linkToDataClick(e: any) {
    this._handleLinkToDataClick(e);
    this.linkedDataId = e;
    this.isLinkedToData = this.linkedDataId !== null || this.originalLinkedDataId !== null;
  }

  handleLinkToDataSelected = (e: any) => {
    this.linkToDataClick(e);
  };

  editLinkedData() {
    this.editedLinkedData = true;
    this.originalLinkedDataId = this.linkedDataId;
    this.documentTypeClick(this.documentStorageForm.get('documentType').value);
  }

  async saveClick(e: any) {
    if(this.saveSpinner){
      return
    }
    this.saveSpinner = true;
    if (this.documentDisplayMode === DocumentDisplayMode.Add) {
      let createdDocument = await this.createDocument();
      this._handleSaveClick(createdDocument);
    } else if (this.documentDisplayMode === DocumentDisplayMode.Edit) {
      let documentUpdateOptions = new DocumentUpdateOptions();
      documentUpdateOptions.comments =
        this.documentStorageForm.get('comments').value;
      documentUpdateOptions.documentTypeId =
        this.documentStorageForm.get('documentType').value;
      documentUpdateOptions.linkedDataId =
        this.linkedDataId !== null
          ? this.linkedDataId
          : this.originalLinkedDataId;
      let updatedDocument = await this.documentService.updateActiveAsync(
        documentUpdateOptions,
        this.document.id
      );
      this._handleSaveClick(updatedDocument);
    }
    setTimeout(() => {
      this.saveSpinner = false;
    }, 1000);
    this.closeClick();
  }

  async saveAndAddNewClick(e: any) {
    this.saveAndNewSpinner = true;
    let createdDocument = await this.createDocument();
    this._handleSaveAndAddNewClick(createdDocument);
    this.documentStorageForm.reset();
    this.linkToDataTreeItem = null;
    this.initializeDocumentStorageFormState();
    this.saveAndNewSpinner = false;
  }

  async createDocument() {
    let documentCreateOptions = new DocumentCreateOptions();
    documentCreateOptions.comments =
      this.documentStorageForm.get('comments').value;
    documentCreateOptions.documentTypeId =
      this.documentStorageForm.get('documentType').value;
    documentCreateOptions.linkedDataId =
      this.linkedDataId !== null
        ? this.linkedDataId
        : this.originalLinkedDataId;
    documentCreateOptions.file =
      this.attachedDocument.fileName + ';' + this.attachedDocument.file;
    return await this.documentService.createAsync(documentCreateOptions);
  }
  closeClick() {
    this._handleCloseClick();
  }

  onFileChange(event: any) {
    let files: FileList = event.target.files;
    if (this.checkFileValidations(files)) {
      this.attachedDocument = new DocumentToUpload();
      this.attachedDocument.fileName = files[0]['name'];
      let reader = new FileReader();
      reader.onload = (e) => {
        this.attachedDocument.file = reader.result?.toString() ?? '';
      };
      reader.readAsDataURL(files[0]);
      this.isFileAttached = true;
    }
  }

  @HostListener('dragover', ['$event']) onDragOver(event: any) {
    this.dragAreaClass = 'droparea';
    event.preventDefault();
    event.stopPropagation();
  }
  @HostListener('dragenter', ['$event']) onDragEnter(event: any) {
    this.dragAreaClass = 'droparea';
    event.preventDefault();
  }
  @HostListener('dragend', ['$event']) onDragEnd(event: any) {
    this.dragAreaClass = 'dragarea';
    event.preventDefault();
  }
  @HostListener('dragleave', ['$event']) onDragLeave(event: any) {
    this.dragAreaClass = 'dragarea';
    event.preventDefault();
  }
  @HostListener('drop', ['$event']) onDrop(event: any) {
    this.dragAreaClass = 'dragarea';
    event.preventDefault();
    event.stopPropagation();
    if (event.dataTransfer.files) {
      let files: FileList = event.dataTransfer.files;
      if (this.checkFileValidations(files)) {
        this.attachedDocument = new DocumentToUpload();
        this.attachedDocument.fileName = files[0]['name'];
        let reader = new FileReader();
        reader.onload = (e) => {
          this.attachedDocument.file = reader.result?.toString() ?? '';
        };
        reader.readAsDataURL(files[0]);
        this.isFileAttached = true;
      }
    }
  }

  checkFileValidations(files: FileList) {
    if (files.length > 1) {
      this.error = 'Only one file at a time is allowed';
      return false;
    } else {
      this.error = '';
      return true;
    }
  }
}
