import {DatePipe} from '@angular/common';
import {Component, Input, OnInit, Output, ViewChild, EventEmitter} from '@angular/core';
import {MatSort} from '@angular/material/sort';
import {MatLegacyTableDataSource as MatTableDataSource} from '@angular/material/legacy-table';

@Component({
  selector: 'app-ceh-upload-export',
  templateUrl: './ceh-upload-export.component.html',
  styleUrls: ['./ceh-upload-export.component.scss']
})
export class CehUploadExportComponent implements OnInit {
  @Input() tableData: any;
  @Output() goBackevent: EventEmitter<boolean> = new EventEmitter();
  dataSource: MatTableDataSource<any>;
  datePipe = new DatePipe('en-US');
  displayedExpandedColumns: string[] = [
    'courseId',
    'enrollmentDate',
    'certificationNumber',
    'operatingTopicsCEH',
    'standards',
    'simulations'
  ];

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }

  constructor() {
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<any>(this.tableData);
  }

  private convertToCSV(data: any[]): string {
    const fields = Object.keys(data[0]);
    const header = fields.map(s => this.getHeaderName(s));
    const csv = [header.filter(r => !!r).join(',')];
    data.forEach((row) => {
      const line = fields.filter(r => !!this.getHeaderName(r)).map((fieldName) => this.getValue(fieldName, row[fieldName]));
      csv.push(line.join(','));
    });
    return csv.join('\n');
  }

  private getValue(fieldName, value): string {
    switch (fieldName) {
      case 'enrollmentDate': {
        var date = value + 'Z';
        var localDate = this.datePipe.transform(date, 'MM/dd/yyyy');
        return '"' + localDate.split('T')[0] + '"';
      }
      default :
        return '"' + value + '"';
    }
  }

  private getHeaderName(fieldName: string): string {
    switch (fieldName) {
      case 'courseNumber':
        return 'course_id';
      case 'enrollmentDate':
        return 'enrollment_date';
      case 'certificationNumber':
        return 'certification_number';
      case 'operatingTopicsCEH':
        return 'operating_topics_ceh';
      case 'standards':
        return 'standards';
      case 'simulations':
        return 'simulations';
    }

    return '';

  }


  downloadCSV() {
    const csvData = this.convertToCSV(this.dataSource.data);
    const blob = new Blob([csvData], {type: 'text/csv'});
    const url = window.URL.createObjectURL(blob);

    const a = document.createElement('a');
    a.href = url;
    a.download = 'cehUpload-data.csv';
    a.click();
    window.URL.revokeObjectURL(url);
  }

  goBack() {
    this.goBackevent.emit(false);
  }

}
