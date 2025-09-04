import { Component, OnInit } from '@angular/core';
import { ApiDocumentsService } from 'src/app/_Services/QTD/Documents/api.document.Service';

@Component({
  selector: 'app-document-storage-view',
  templateUrl: './document-storage-view.component.html',
  styleUrls: ['./document-storage-view.component.scss'],
})
export class DocumentStorageViewComponent implements OnInit {
  documentList: any;
  constructor(private storageDocumentService: ApiDocumentsService) {}
  async ngOnInit() {}
}
