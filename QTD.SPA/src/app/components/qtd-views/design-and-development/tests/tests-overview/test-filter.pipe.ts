import { Pipe, PipeTransform } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Pipe({
  name: 'testFilter'
})
export class TestFilterPipe implements PipeTransform {

  transform(value: MatTableDataSource<any>, filterString: string): any {
    let newValue = value ? value.data : []
    let resultArray: any = []
    if (newValue.length === 0 || filterString === '') {
      return value
    }
    for (const item of newValue) {
      if (item['testTitle'].toLowerCase().includes(filterString.toLowerCase())) {
        resultArray.push(item)
      }
    }

    return new MatTableDataSource<any>(resultArray)
  }

}
