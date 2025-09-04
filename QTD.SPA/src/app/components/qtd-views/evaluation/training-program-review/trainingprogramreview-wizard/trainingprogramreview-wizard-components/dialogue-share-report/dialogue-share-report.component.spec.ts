import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogueShareReportComponent } from './dialogue-share-report.component';

describe('DialogueShareReportComponent', () => {
  let component: DialogueShareReportComponent;
  let fixture: ComponentFixture<DialogueShareReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogueShareReportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogueShareReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
