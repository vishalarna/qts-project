import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ListInstancesComponent } from './list-instances.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { DataTablesModule } from 'angular-datatables';
import { By } from '@angular/platform-browser';

describe('ListInstancesComponent', () => {
  let component: ListInstancesComponent;
  let fixture: ComponentFixture<ListInstancesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        HttpClientTestingModule,
     LocalizeModule,
        DataTablesModule,
      ],
      declarations: [ListInstancesComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListInstancesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should show Add Instance and Back to client button if instance is selected', () => {
    component.selectClientInstance = false;
    fixture.detectChanges();
    let element = fixture.debugElement.query(By.css('#btnsRow'));
    expect(element).toBeTruthy();
  });

  it('should not show Add Instance and Back to client button if instance not is selected', () => {
    component.selectClientInstance = true;
    fixture.detectChanges();
    let element = fixture.debugElement.query(By.css('#btnsRow'));
    expect(element).toBeNull();
  });

  it('should show list of instances if instance is not selected', () => {
    component.selectClientInstance = true;
    fixture.detectChanges();
    let el = fixture.debugElement.query(By.css('#tblSelectInstance'));
    expect(el).toBeTruthy();
  });

  it('should  have hidden property to true of instances action table if instance is not selected', () => {
    component.selectClientInstance = true;
    fixture.detectChanges();
    let el = fixture.debugElement.query(By.css('#tblSelectInstance'));
    let tbl = fixture.debugElement.query(By.css('#tblInstances'));
    expect(tbl.nativeElement.hidden).toBeTruthy();
    expect(el).toBeTruthy();
  });

  it('should show list of instances to add, view and edit if instance is selected', () => {
    component.selectClientInstance = false;
    fixture.detectChanges();
    let el = fixture.debugElement.query(By.css('#tblSelectInstance'));
    let tbl = fixture.debugElement.query(By.css('#tblInstances'));
    expect(tbl).toBeTruthy();
    expect(el).toBeNull();
  });
});
