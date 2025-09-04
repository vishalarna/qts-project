import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AddEditInstanceComponent } from './add-edit-instance.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AppModule } from 'src/app/app.module';
import {
  NgbActiveModal,
  NgbModalModule,
  NgbModule,
} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { By } from '@angular/platform-browser';

describe('AddEditInstanceComponent', () => {
  let component: AddEditInstanceComponent;
  let fixture: ComponentFixture<AddEditInstanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        HttpClientTestingModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
      ],
      declarations: [AddEditInstanceComponent],
      providers: [TranslateService, NgbActiveModal],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditInstanceComponent);
    component = fixture.componentInstance;
    spyOn(component, 'readyForm').and.callThrough();
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should call readyForm method on instantiation of component', () => {
    fixture.detectChanges();
    expect(component.readyForm).toHaveBeenCalled();
  });

  it('should show disabled submit button if form is empty or invalid', () => {
    component.Form.patchValue({ name: null });
    fixture.detectChanges();
    let btn = fixture.debugElement.query(By.css('#btnSubmit'));
    expect(btn.nativeElement.disabled).toBeTrue();
  });
});
