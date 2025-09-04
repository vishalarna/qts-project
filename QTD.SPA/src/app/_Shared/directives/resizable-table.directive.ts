import {AfterViewInit,Directive,DoCheck,ElementRef,HostListener,Input,KeyValueDiffers,OnChanges,Renderer2, SimpleChanges} from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Directive({
  selector: '[resizableTable]',
})
export class ResizableTableDirective implements AfterViewInit, DoCheck  {
  @Input('resizableTable') isResizable: boolean = false;
  @Input() dataSource: MatTableDataSource<any>; 
  private pressed = false;
  private startX: number;
  private startWidth: number;
  private isResizingRight: boolean;
  private currentResizeIndex: number;
  columns: any[] = [];
  resizableMousemove: () => void;
  resizableMouseup: () => void;
  private differ: any;
  constructor(private elementRef: ElementRef,private differs: KeyValueDiffers, private renderer: Renderer2) {
    this.differ = this.differs.find([]).create();
  }
  ngAfterViewInit() {
    if (this.isResizable) {
      this.createColumnsArray();
    }
  }
  ngDoCheck() {
    let changes = this.differ.diff(this.dataSource.data);
    let sortChanges = this.differ.diff(this.dataSource.sort)
    if (changes || sortChanges) {
      this.setColumnWidth();
    }
  }

  private createColumnsArray() {
    const headerRow =
      this.elementRef.nativeElement.querySelector('mat-header-row');
    if (headerRow) {
      const headers = headerRow.querySelectorAll('mat-header-cell');
      headers.forEach((header: HTMLElement,index) => {
        if(index != (headers.length - 1)){
          header.classList.add('col-resize');
          const text = this.renderer.createText(' | ');
          const resizer = this.renderer.createElement('span');
          this.renderer.appendChild(resizer,text);
          this.renderer.addClass(resizer, 'resize-handle');
          this.renderer.appendChild(header, resizer);
        }
          const matColumnDefClass = Array.from(header.classList).find((cls) =>
          cls.startsWith('mat-column-')
        );
        if (matColumnDefClass) {
          const field = matColumnDefClass.replace('mat-column-', '');
          this.columns.push({ field, width: header.offsetWidth });
          header.dataset.field = field;
        }
      });
    }
  }

  @HostListener('mousedown', ['$event'])
  onMouseDown(event: MouseEvent) {
    if (!this.isResizable) {
      return;
    }
    const resizeHandle = event.target as HTMLElement;
    if (resizeHandle.nodeName === 'SPAN' && Array.from(resizeHandle.classList).findIndex((cls) => cls == "resize-handle") != -1) {
      const headerCell = resizeHandle.parentElement;
      const columnIndex = this.columns.findIndex(
        (col) =>
          col.field ===
          Array.from(headerCell.classList)
            .find((cls) => cls.startsWith('mat-column-'))
            ?.replace('mat-column-', '')
      );
      if (columnIndex !== -1) {
        this.checkResizing(event, columnIndex);
        this.pressed = true;
        this.startX = event.pageX;
        this.startWidth = headerCell.clientWidth;
        this.currentResizeIndex = columnIndex;
        event.preventDefault();
        this.mouseMove(columnIndex);
      }
    }
  }

  private checkResizing(event: MouseEvent, index: number) {
    const cellData = this.getCellData(index);
    if (
      index === 0 ||
      (Math.abs(event.pageX - cellData.right) < cellData.width / 2 &&
        index !== this.columns.length - 1)
    ) {
      this.isResizingRight = true;
    } else {
      this.isResizingRight = false;
    }
  }
  
  private getCellData(index: number) {
    const headerRow = this.elementRef.nativeElement.querySelector('mat-header-row');
    const cell = headerRow.children[index];
    return cell.getBoundingClientRect();
  }

  mouseMove(index: number) {
    this.resizableMousemove = this.renderer.listen('document','mousemove',(event) => {
        if (this.pressed && event.buttons) {
          const dx = this.isResizingRight ? event.pageX - this.startX : -event.pageX + this.startX;
          const width = this.startWidth + dx;
          if (this.currentResizeIndex === index && width > 50) {
            this.setColumnWidthChanges(index, width);
          }
        }
      }
    );
    this.resizableMouseup = this.renderer.listen('document','mouseup',(event) => {
        if (this.pressed) {
          this.pressed = false;
          this.currentResizeIndex = -1;
          this.resizableMousemove();
          this.resizableMouseup();
        }
      }
    );
  }

  private setColumnWidthChanges(index: number, width: number) {
    const orgWidth = this.columns[index].width;
    const dx = width - orgWidth;
    if (dx !== 0) {
      const j = this.isResizingRight ? index + 1 : index - 1;
      if (j >= 0 && j < this.columns.length) {
        const newWidth = this.columns[j].width - dx;
        if (newWidth > 50) {
          this.columns[index].width = width;
          this.setColumnWidth();
          this.columns[j].width = newWidth;
          this.setColumnWidth();
        }
      }
    }
  }

  private setColumnWidth() {
    for(var column of this.columns){
      const columnEls = Array.from(
        document.getElementsByClassName('mat-column-' + column.field)
        );
        columnEls.forEach((el: HTMLElement) => {
          el.style.flexBasis = column.width + 'px';
        });
      }
    }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.adjustColumnWidths();
  }

  private adjustColumnWidths() {
    const headerRow = this.elementRef.nativeElement.querySelector('mat-header-row');
    if (headerRow) {
      const headers = headerRow.querySelectorAll('mat-header-cell');
      headers.forEach((header, index) => {
        const newWidth = header.offsetWidth;
        this.columns[index].width = newWidth < 50 ? 50 : newWidth;
        this.setColumnWidth();
      });
    }
  }

}
