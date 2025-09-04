import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToolCategory } from 'src/app/_DtoModels/ToolCategory/ToolCategory';
import { ToolCategoryCreateOptions } from 'src/app/_DtoModels/ToolCategory/ToolCategoryCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-tool-category',
  templateUrl: './fly-panel-add-tool-category.component.html',
  styleUrls: ['./fly-panel-add-tool-category.component.scss'],
})
export class FlyPanelAddToolCategoryComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refreshCategories = new EventEmitter<any>();
  @Output() emitSavedCategoryID = new EventEmitter<string>();
  showSpinner: boolean = false;
  categoryForm: UntypedFormGroup;
  dateError = false;
  addAnother: boolean = false;
  @Input() oldToolCat:ToolCategory;
  @Input() isCopy:boolean;
  constructor(
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private datePipe: DatePipe,
    private toolSrvc: ToolsService,
    private dataBroadcastService : DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {
    this.readyCategoryForm();
    if(this.oldToolCat !== undefined){
      this.readyEditCopyData();
    }
  }

  readyEditCopyData(){
    this.categoryForm.get('title')?.setValue(this.oldToolCat.title);
    this.categoryForm.get('desc')?.setValue(this.oldToolCat.description);
    this.categoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(this.oldToolCat.effectiveDate,'yyyy-MM-dd'));
    this.categoryForm.get('AddAnother')?.setValue(false);
    this.categoryForm.get('website')?.setValue(this.oldToolCat.website);
  }

  readyCategoryForm() {
    this.categoryForm = this.fb.group({
      title: new UntypedFormControl('', [Validators.required]),
      desc: new UntypedFormControl(''),
      effectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      notes: new UntypedFormControl('', [Validators.required]),
      website: new UntypedFormControl(''),
    });
  }

 /*  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */

  async SaveNewCategory() {
    
    this.showSpinner = true;
      var options = new ToolCategoryCreateOptions(); 
      options.description = this.categoryForm.get('desc')?.value || '';
      options.title = this.categoryForm.get('title')?.value;
      if (this.isCopy) {
        options.title = this.oldToolCat.title.trim().toLowerCase() == options.title.trim().toLowerCase()
          ? options.title + ("-Copy") : options.title;
      }
      options.effectiveDate = this.categoryForm.get('effectiveDate')?.value;
      options.notes = this.categoryForm.get('notes')?.value;
      options.website = this.categoryForm.get('website')?.value;
    
    this.toolSrvc
      .createCategory(options)
      .then(async (res) => {
        
        if (this.addAnother) {
          this.categoryForm.reset();
          this.categoryForm.patchValue({
            effectiveDate: this.datePipe.transform(Date.now(), 'yyyy-MM-dd'),
          });
          this.addAnother = false;
        } else {
          this.closed.emit('tool-category-saved');
          this.emitSavedCategoryID.emit(res.id);
          this.router.navigate([`/my-data/tools/cat-detail/${res.id}`]);
        }

        if(this.isCopy === false){
          this.alert.successToast(await this.labelPipe.transform('Tool') + ' Category added successfully');
          this.router.navigate([`/my-data/tools/cat-detail/${res.id}`]);
        }else{
          this.alert.successToast(await this.labelPipe.transform('Tool') + ' Category copied successfully');
          this.router.navigate([`/my-data/tools/cat-detail/${res.id}`]);
        }
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }



  EditCategory(){
    
    this.showSpinner = true;
    var options = new ToolCategory();
    options.description = this.categoryForm.get("desc")?.value;
    options.title = this.categoryForm.get("title")?.value;
    options.notes = this.categoryForm.get("notes")?.value;
    options.effectiveDate = this.categoryForm.get("effectiveDate")?.value;
    options.website =this.categoryForm.get("website")?.value;

    this.toolSrvc.updateToolCategory(this.oldToolCat.id,options).then(async (res:any)=>{
      this.showSpinner = false;
      this.closed.emit('fp-add-sh-cat-closed');
      this.refreshCategories.emit();
      this.alert.successToast("Successfull Edited " + await this.labelPipe.transform('Tool') + " Category Data");
    }).finally(()=>{
      this.showSpinner = false;
    })
  }
}
