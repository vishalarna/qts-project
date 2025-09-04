import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AddEmpComponent } from './add-emp.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AddEmpModule } from './add-emp.module';

import { By } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

describe('AddEmpComponent', () => {
  let component: AddEmpComponent;
  let fixture: ComponentFixture<AddEmpComponent>;
  let translateService: TranslateService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        AddEmpModule,
        HttpClientTestingModule,
     LocalizeModule,
      ],
      declarations: [AddEmpComponent],
      providers: [TranslateService],
    }).compileComponents();

    fixture = TestBed.createComponent(AddEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it(
    'should disable the form button if form is invalid',
    waitForAsync(() => {
      fixture.detectChanges();
      fixture.whenStable().then(() => {
        component.empAddForm.controls['fName'].patchValue({
          fName: null,
          lName: null,
        });
        let btn = fixture.debugElement.query(By.css('#btnSubmit'));
        expect(btn.nativeElement.disabled).toBeTrue();
        expect(component.empAddForm.invalid).toBeTrue();
      });
    })
  );

  it('should call getCertBodies method', () => {
    fixture.detectChanges();
    spyOn(component, 'getCertBodies');
    component.getCertBodies();
    expect(component.getCertBodies).toHaveBeenCalled();
  });

  it('should call getAllOrganizations method', () => {
    fixture.detectChanges();
    spyOn(component, 'getAllOrganizations');
    component.getAllOrganizations();
    expect(component.getAllOrganizations).toHaveBeenCalled();
  });

  it('should call getAllPositions method', () => {
    fixture.detectChanges();
    spyOn(component, 'getAllPositions');
    component.getAllPositions();
    expect(component.getAllPositions).toHaveBeenCalled();
  });
});
