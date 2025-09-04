import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { UploadEmpsComponent } from './upload-emps.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

describe('UploadEmpsComponent', () => {
  let component: UploadEmpsComponent;
  let fixture: ComponentFixture<UploadEmpsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        HttpClientTestingModule,
     LocalizeModule,
        CommonModule,
        FormsModule,
      ],
      declarations: [UploadEmpsComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadEmpsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });
});
