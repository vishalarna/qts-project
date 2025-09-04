import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ListUsersComponent } from './list-users.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DataTablesModule } from 'angular-datatables';

describe('ListUsersComponent', () => {
  let component: ListUsersComponent;
  let fixture: ComponentFixture<ListUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        HttpClientTestingModule,
        DataTablesModule,
      ],
      declarations: [ListUsersComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });
});
