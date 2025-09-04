import { formatDate } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import saveAs from 'file-saver';
import {
  ReportExportOptions,
  ReportExportType,
} from 'src/app/_DtoModels/Report/ReportExportOptions';
import { ReportSendOptions } from 'src/app/_DtoModels/Report/ReportSendOptions';
import { ReportUpdateOptions } from 'src/app/_DtoModels/Report/ReportUpdateOptions';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { HttpResponse } from '@angular/common/http';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';


@Component({
  selector: 'app-report-view',
  templateUrl: './report-view.component.html',
  styleUrls: ['./report-view.component.scss'],
})
export class ReportViewComponent implements OnInit {
  @Input() reportMode: string;
  @Input() reportContent: string;
  @Input() reportId: number;
  @Input() reportCreateorUpdate: ReportUpdateOptions;
  @Output() goBackevent: EventEmitter<boolean> = new EventEmitter();
  public openSharePopup: Boolean = false;
  public openExportPopup: Boolean = false;
  public reportSendOption: ReportSendOptions;
  public reportExportOption: ReportExportOptions;
  public emailAddress;
  public shareSpinner;
  public exportSpinner;
  public emailExpression;
  public renderedHtml: SafeHtml;
  public validEmail: Boolean;
  public isButtonDisabled: boolean = true;
  public loggedInUser:string;
  public currentTime:string;

  constructor(
    private reportService: ApiReportsService,
    private alertService: SweetAlertService,
    private translate: TranslateService,
    private sanitizer: DomSanitizer,
    private renderer: Renderer2
  ) {}

  ngOnInit() {
    this.generateReportContent();
    this.shareSpinner = false;
    this.exportSpinner = false;
    this.initializeSendReportOptions();
    this.initializereportExportOptions();
    this.loggedInUser = jwtAuthHelper.LoggedInUser;
    this.getCurrentTime();
  }

  generateReportContent() {
    const template = this.renderer.createElement('template');
    template.innerHTML = this.reportContent.trim();
    const anchorNodes: NodeList = template.content.querySelectorAll('a');
    const anchors: Node[] = Array.from(anchorNodes);
    for (const anchor of anchors) {
      const href: string = (anchor as HTMLAnchorElement).getAttribute('href');
      if (href.indexOf('#') === 0) {
        this.renderer.setProperty(
          anchor,
          'href',
          `${window.location.pathname}${href}`
        );
      }
    }
    this.renderedHtml = this.sanitizer.bypassSecurityTrustHtml(
      template.innerHTML
    );
    setTimeout(() => {
      this.injectScript(template.innerHTML);
    }, 0);
  }

  injectScript(html: string) {
    const scripts = document.createElement('div');
    scripts.innerHTML = html;
    const scriptElements = scripts.getElementsByTagName('script');
  
    for (let i = 0; i < scriptElements.length; i++) {
      const script = scriptElements[i];
      const newScript = this.renderer.createElement('script');
      newScript.type = script.type || 'text/javascript';
  
      if (script.src) {
        newScript.src = script.src;
      } else {
        const scriptContent = script.innerHTML;
        newScript.text = `
          (function() {
            function waitForChartJs() {
              if (typeof Chart !== 'undefined') {
                ${scriptContent}
              } else {
                setTimeout(waitForChartJs, 50);
              }
            }
            waitForChartJs();
          })();
        `;
      }
      this.renderer.appendChild(document.body, newScript);
    }
  }

  initializeSendReportOptions() {
    this.reportSendOption = new ReportSendOptions();
    this.reportSendOption.getCreateOrUpdateOption(this.reportCreateorUpdate);
  }

  initializereportExportOptions() {
    this.reportExportOption = new ReportExportOptions();
    this.reportExportOption.getCreateOrUpdateOption(this.reportCreateorUpdate);
  }

  async goBack() {
    this.goBackevent.emit(false);
  }

  public onShareClick(item: any) {
    this.openSharePopup = item;
  }

  public onExportClick(item: any) {
    this.openExportPopup = item;
  }

  public onExportTypeChange(event: any) {
    if (this.openSharePopup) {
      this.reportSendOption.getExportType(event.target.value);
    } else {
      this.reportExportOption.getExportType(event.target.value);
      this.isButtonDisabled = false;
    }
  }

  public onExport() {
    this.exportSpinner = true;
    if (this.reportMode == 'create') {
        this.reportService
        .exportReportAsync(this.reportExportOption)
        .then((res) => {
          this.onExportDataResponse(res);
          this.exportSpinner = false;
        });
    } else if (this.reportMode == 'update') {
      try {
        this.reportService
          .exportReportByIdAsync(this.reportExportOption, this.reportId)
          .then(async (res) => {
            this.onExportDataResponse(res);
            this.exportSpinner = false;
          });
      } catch (e) {
        this.exportSpinner = false;
      }
    }
  }

  public onExportDataResponse(res) {
    if (this.reportExportOption.exportType == ReportExportType.Pdf) {
      this.createAndDownloadBlobFile(res);
    } else {
      let timeStamp = formatDate(new Date(), 'MMddyyyy', 'en');
      saveAs(
        res.body,
        this.reportExportOption.options.internalReportTitle +
          timeStamp +
          '.xlsx'
      );
    }
    this.alertService.notificationSuccessToast(
      this.translate.instant('File Download Successfully')
    );
    this.openExportPopup = false;
    this.isButtonDisabled = true;
  }

  public onEmailAddressChange(email: string) {
    this.emailExpression = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
    this.validEmail = this.emailExpression.test(email);
    if (this.validEmail == true) {
      this.reportSendOption.getTos(email);
    }
  }

  public async onShare(): Promise<void> {
    this.shareSpinner = true;
    if (this.reportMode == 'create') {
      await this.reportService.sendReportAsync(this.reportSendOption);
      this.alertService.notificationSuccessToast(
        this.translate.instant('Email Sent To Email Address Successfully')
      );
      this.openSharePopup = false;
      this.shareSpinner = false;
      this.initializeSendReportOptions();
      this.emailAddress = '';
    } else if (this.reportMode == 'update') {
      await this.reportService.sendReportByIdAsync(
        this.reportSendOption,
        this.reportId
      );
      this.alertService.notificationSuccessToast(
        this.translate.instant('Email Sent To Email Address Successfully')
      );
      this.openSharePopup = false;
      this.shareSpinner = false;
      this.initializeSendReportOptions();
      this.emailAddress = '';
    }
  }

  public createAndDownloadBlobFile(response: HttpResponse<Blob>) {
    const contentDispositionHeader = response.headers.get(
      'content-disposition'
    );
    const defaultFileName = 'downloaded-file.pdf';
    let fileName = defaultFileName;

    if (contentDispositionHeader) {
      const match = contentDispositionHeader.match(
        /filename=['"]?([^'";]+)['"]?/
      );
      fileName = match ? match[1] : defaultFileName;
    }
    const nameAndDateMatch = fileName.match(/^(.*?)\s?(\d{8})/);
    const name = nameAndDateMatch ? nameAndDateMatch[1] : defaultFileName;
    const date = nameAndDateMatch ? nameAndDateMatch[2] : '';
    fileName = `${name.replace(/\s+/g, '')}${date ? date : ''}.pdf`;

    const blob = new Blob([response.body!], {
      type: 'application/octet-stream',
    });
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }


  public onPrintClick() {
    let printContents, popupWin;
    printContents = document.getElementById('print-section').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>Print tab</title>
          <style>
          //........Customized style.......
          </style>
        </head>
    <body onload="window.print();window.close()">${printContents}</body>
      </html>`);
    popupWin.document.close();
  }

  public getCurrentTime(){
    var date = new Date();
    this.currentTime = `${date.getHours()}:${date.getMinutes().toString().length<2 ? "0"+ date.getMinutes() : date.getMinutes()}`
  }
}
