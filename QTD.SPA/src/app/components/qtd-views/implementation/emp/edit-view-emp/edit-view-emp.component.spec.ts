import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {
  ComponentFixture,
  fakeAsync,
  TestBed,
  tick,
  waitForAsync,
} from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { EditViewEmpComponent } from './edit-view-emp.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CommonModule, TitleCasePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { By } from '@angular/platform-browser';

describe('EditViewEmpComponent', () => {
  let component: EditViewEmpComponent;
  let fixture: ComponentFixture<EditViewEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        HttpClientTestingModule,
        CommonModule,
        FormsModule,
     LocalizeModule,
      ],
      declarations: [EditViewEmpComponent],
      providers: [TranslateService, TitleCasePipe],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditViewEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should not show IDP Tab Page if Employee Details Tab is selected', () => {
    component.activeTab = 'details';
    fixture.detectChanges();
    let idpTabPage = fixture.debugElement.query(By.css('#idpTab'));
    let empDetailTabPage = fixture.debugElement.query(By.css('#detailTab'));
    expect(empDetailTabPage).toBeTruthy();
    expect(idpTabPage).toBeNull();
  });

  it(
    'should not show Employee Tab Page if IDP Tab is selected',
    waitForAsync(() => {
      component.activeTab = 'idp';
      fixture.detectChanges();
      fixture.whenStable().then(() => {
        let idpTabPage = fixture.debugElement.query(By.css('#idpTab'));
        let empDetailTabPage = fixture.debugElement.query(By.css('#detailTab'));
        expect(empDetailTabPage).toBeNull();
        expect(idpTabPage).toBeTruthy();
      });
    })
  );

  it('should show employee details section only when employee detail accordian is clicked', fakeAsync(() => {
    component.activeTab = 'details';
    component.activeDetail = '';
    let btn = fixture.debugElement.query(By.css('#btnEmpDetails'));
    btn.nativeElement.click();
    fixture.detectChanges();
    tick();
    let detailDiv = fixture.debugElement.query(By.css('#empDetails'));
    let certificationsDiv = fixture.debugElement.query(
      By.css('#certifications')
    );
    let positionDiv = fixture.debugElement.query(By.css('#positions'));
    expect(detailDiv).toBeTruthy();
    expect(certificationsDiv).toBeNull();
    expect(positionDiv).toBeNull();
  }));

  it('should show certification details section only when certification accordian is clicked', fakeAsync(() => {
    component.activeTab = 'details';
    component.activeDetail = '';
    let btn = fixture.debugElement.query(By.css('#btnCertification'));
    btn.nativeElement.click();
    fixture.detectChanges();
    tick();
    let detailDiv = fixture.debugElement.query(By.css('#empDetails'));
    let certificationsDiv = fixture.debugElement.query(
      By.css('#certifications')
    );
    let positionDiv = fixture.debugElement.query(By.css('#positions'));

    expect(detailDiv).toBeNull();
    expect(certificationsDiv).toBeTruthy();
    expect(positionDiv).toBeNull();
  }));

  it('should show position section only when position accordian is clicked', fakeAsync(() => {
    component.activeTab = 'details';
    component.activeDetail = '';
    let btn = fixture.debugElement.query(By.css('#btnPosition'));
    btn.nativeElement.click();
    fixture.detectChanges();
    tick();
    let detailDiv = fixture.debugElement.query(By.css('#empDetails'));
    let certificationsDiv = fixture.debugElement.query(
      By.css('#certifications')
    );
    let positionDiv = fixture.debugElement.query(By.css('#positions'));

    expect(detailDiv).toBeNull();
    expect(certificationsDiv).toBeNull();
    expect(positionDiv).toBeTruthy();
  }));
});
