
import { Pipe, PipeTransform } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: MatTableDataSource<any>, filterString:string): any {
    let newValue = value ? value.data:[]
    let resultArray:any = []
    if(newValue.length === 0 || filterString === ''){
      return value
    }
    for(const item of newValue){
      if((item['providerName']?.toLowerCase().includes(filterString) || item['topicName']?.toLowerCase().includes(filterString))  || (item['title'].toLowerCase().includes(filterString) || item['num']?.toLowerCase().includes(filterString)) ){
         resultArray.push(item)
      }
    }
    return new MatTableDataSource<any>(resultArray)
  }
}
