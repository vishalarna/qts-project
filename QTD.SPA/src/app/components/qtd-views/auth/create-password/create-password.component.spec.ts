import { CUSTOM_ELEMENTS_SCHEMA, DebugElement } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { CreatePasswordComponent } from './create-password.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('CreatePasswordComponent', () => {
  let component: CreatePasswordComponent;
  let fixture: ComponentFixture<CreatePasswordComponent>;
  let formBtn: DebugElement;
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
      declarations: [CreatePasswordComponent],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePasswordComponent);
    component = fixture.componentInstance;
    formBtn = fixture.debugElement.query(By.css('button[type="submit"]'));
    spyOn(component, 'readyCreatePassForm').and.callThrough();
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('CreatePasswordForm Should have been called', () => {
    expect(component.readyCreatePassForm).toHaveBeenCalled();
  });

  it('create password button should be disabled if form is invalid', () => {
    component.createPassForm.patchValue({ email: null });
    expect(formBtn.nativeElement.disabled).toBeTruthy();
  });
});
