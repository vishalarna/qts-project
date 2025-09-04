import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
@Component({
  selector: 'app-delete-segment',
  templateUrl: './delete-segment.component.html',
  styleUrls: ['./delete-segment.component.scss']
})
export class DeleteSegmentComponent implements OnInit {
  
  header: string;
  description: string;
  cancelText: string;
  confirmText: string;

  constructor( 
    private translate: TranslateService,
    private alert: SweetAlertService) {

      const browserLang = localStorage.getItem('lang') ?? 'en';
      this.translate.use(browserLang);
     }

  ngOnInit(): void {
    this.settingDialogue();
  }

  settingDialogue() {
    
    this.header = this.translate.instant("L.DeleteSegment");
    this.description = 'Are you sure you want to delete this segment?';
    this.cancelText = 'Cancel';
    this.confirmText = 'Delete Segment';
  }

}
