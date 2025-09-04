
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateService } from '@ngx-translate/core';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgbActiveModal, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { DeleteSegmentComponent } from './delete-segment.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';

describe('DeleteSegmentComponent', () => {
  let component: DeleteSegmentComponent;
  let fixture: ComponentFixture<DeleteSegmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, LocalizeModule, HttpClientTestingModule],
      declarations: [ DeleteSegmentComponent ],
      providers: [TranslateService, NgbActiveModal],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteSegmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
