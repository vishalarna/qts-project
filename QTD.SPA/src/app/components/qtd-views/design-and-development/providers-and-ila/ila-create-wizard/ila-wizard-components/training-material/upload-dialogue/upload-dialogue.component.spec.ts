import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadDialogueComponent } from './upload-dialogue.component';

describe('UploadDialogueComponent', () => {
  let component: UploadDialogueComponent;
  let fixture: ComponentFixture<UploadDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UploadDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
