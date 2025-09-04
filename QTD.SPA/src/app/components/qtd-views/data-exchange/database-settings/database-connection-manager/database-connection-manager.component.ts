import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Component({
  selector: 'app-database-connection-manager',
  templateUrl: './database-connection-manager.component.html',
  styleUrls: ['./database-connection-manager.component.scss']
})
export class DatabaseConnectionManagerComponent implements OnInit {
  displayedColumns: string[] = ['id', 'databaseName', 'Action'];
  @Input()
  dataSourceDatabaseConnection: MatTableDataSource<any>;

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSourceDatabaseConnection.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSourceDatabaseConnection.paginator = paginator;
  }
  constructor() { }

  ngOnInit(): void {
  }
  filterDatabaseNames(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.dataSourceDatabaseConnection.filter = filter;
  }
  
}
