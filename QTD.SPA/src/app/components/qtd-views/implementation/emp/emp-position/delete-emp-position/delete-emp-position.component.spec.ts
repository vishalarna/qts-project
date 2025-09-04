import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { DeleteEmpPositionComponent } from './delete-emp-position.component';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import {
  NgbActiveModal,
  NgbModal,
  NgbModalModule,
  NgbModule,
} from '@ng-bootstrap/ng-bootstrap';

describe('DeleteEmpPositionComponent', () => {
  let component: DeleteEmpPositionComponent;
  let fixture: ComponentFixture<DeleteEmpPositionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, LocalizeModule, HttpClientTestingModule],
      declarations: [DeleteEmpPositionComponent],
      providers: [TranslateService, NgbActiveModal],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteEmpPositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });
});
