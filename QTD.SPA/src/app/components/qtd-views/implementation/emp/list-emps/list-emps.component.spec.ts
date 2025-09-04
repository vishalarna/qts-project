import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ListEmpsComponent } from './list-emps.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataTablesModule } from 'angular-datatables';

describe('ListEmpsComponent', () => {
  let component: ListEmpsComponent;
  let fixture: ComponentFixture<ListEmpsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        HttpClientTestingModule,
     LocalizeModule,
        CommonModule,
        FormsModule,
        DataTablesModule,
      ],
      declarations: [ListEmpsComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListEmpsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });
});
