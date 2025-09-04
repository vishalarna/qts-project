import { Pipe, PipeTransform } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Pipe({
  name: 'sort'
})
export class SortPipe implements PipeTransform {

  transform(value: MatTableDataSource<any>, ...args: any[]): any {
    let sortIndex = args[0];
    let sortOrder = sortIndex[1]
    let multiplier = 1
    if(sortOrder === 'asc'){
      multiplier = -1
    }

    let newValue = value ? value.data:[]

    newValue.sort((a:any,b:any)=>{
      if(a[sortIndex[0]]<b[sortIndex[0]]){
        return 1 * multiplier
      }
      else if (a[sortIndex[0]]>b[sortIndex[0]]){
        return -1 * multiplier
      }
      else{
        return 0
      }
    })
    value = new MatTableDataSource<any>(newValue)

    return value
  }

}
