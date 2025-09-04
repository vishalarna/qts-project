import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AddEditClientComponent } from './add-edit-client.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { By } from '@angular/platform-browser';

describe('AddEditClientComponent', () => {
  let component: AddEditClientComponent;
  let fixture: ComponentFixture<AddEditClientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        LocalizeModule,
        HttpClientTestingModule,
        CommonModule,
        ReactiveFormsModule,
      ],
      declarations: [AddEditClientComponent],
      providers: [TranslateService, NgbActiveModal],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditClientComponent);
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
    component.clientForm.patchValue({ name: null });
    fixture.detectChanges();
    let btn = fixture.debugElement.query(By.css('#btnSubmit'));
    expect(btn.nativeElement.disabled).toBeTrue();
  });
});
