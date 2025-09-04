import { Pipe, PipeTransform } from '@angular/core';
import { SortDirection } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Pipe({
  name: 'taskSortPipe'
})
export class TaskSortPipePipe implements PipeTransform {

  transform(value: any[], sortOrder: SortDirection | string = 'asc', sortKey?: string):MatTableDataSource<any> {
    if (sortKey === 'number' || sortKey === 'taskNumber') {
      var sortedValue = value.sort((a, b) => {
        var splitNumA = a[sortKey === 'number' ? 'number':'taskNumber'].split('.');
        var splitNumB = b[sortKey === 'number' ? 'number':'taskNumber'].split('.');
        var letterA = splitNumA[0][0];
        var letterB = splitNumB[0][0];
        var daNumberA = Number.parseInt(splitNumA[0].substr(1, splitNumA[0].length - 1));
        var daNumberB = Number.parseInt(splitNumB[0].substr(1, splitNumB[0].length - 1));
        var sdaNumA = Number.parseInt(splitNumA[1]);
        var sdaNumB = Number.parseInt(splitNumB[1]);
        var taskNumA = Number.parseInt(splitNumA[2]);
        var taskNumB = Number.parseInt(splitNumB[2]);
        if (sortOrder === 'asc') {
          if (letterA < letterB) {
            return -1
          } else if (letterA > letterB) {
            return 1;
          }

          if (daNumberA < daNumberB) {
            return -1
          }
          else if (daNumberA > daNumberB) {
            return 1
          }

          if (sdaNumA < sdaNumB) {
            return -1
          }
          else if (sdaNumA > sdaNumB) {
            return 1
          }

          if (taskNumA < taskNumB) {
            return -1
          }
          else if (taskNumA > taskNumB) {
            return 1
          }
          else {
            return 0;
          }
        }
        else {
          if (letterA < letterB) {
            return 1
          } else if (letterA > letterB) {
            return -1;
          }

          if (daNumberA < daNumberB) {
            return 1
          }
          else if (daNumberA > daNumberB) {
            return -1
          }

          if (sdaNumA < sdaNumB) {
            return 1
          }
          else if (sdaNumA > sdaNumB) {
            return -1
          }

          if (taskNumA < taskNumB) {
            return 1
          }
          else if (taskNumA > taskNumB) {
            return -1
          }
          else {
            return 0;
          }
        }
        // return this.compare(letterA,letterB,sort) || this.compare(daNumberA,daNumberB,sort)
        //     || this.compare(sdaNumA,sdaNumB,sort) || this.compare(taskNumA,taskNumB,sort);
        //return 0;
      })
      return new MatTableDataSource(sortedValue);
    }
    else{
      var sortedOnOtherCols = value.sort((a,b)=>{
        if(sortOrder === 'asc'){
          if(a[sortKey] < b[sortKey]){
            return -1
          }
          else if(a[sortKey] > b[sortKey]){
            return 1;
          }
          else{
            return 0;
          }
        }
        else{
          if(a[sortKey] > b[sortKey]){
            return -1
          }
          else if(a[sortKey] < b[sortKey]){
            return 1;
          }
          else{
            return 0;
          }
        }
      })

      return new MatTableDataSource(sortedOnOtherCols);
    }

  }

}
