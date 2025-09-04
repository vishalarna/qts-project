import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateService } from '@ngx-translate/core';
import { ListClientsComponent } from './list-clients.component';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { DataTablesModule } from 'angular-datatables';

describe('ListClientsComponent', () => {
  let component: ListClientsComponent;
  let fixture: ComponentFixture<ListClientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        HttpClientTestingModule,
     LocalizeModule,
        DataTablesModule,
      ],
      declarations: [ListClientsComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListClientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });
});
