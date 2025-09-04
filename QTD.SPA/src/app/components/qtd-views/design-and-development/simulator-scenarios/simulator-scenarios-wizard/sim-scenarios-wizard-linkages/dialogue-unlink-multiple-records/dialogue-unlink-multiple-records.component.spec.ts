import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DialogueUnlinkMultipleRecordsComponent } from './dialogue-unlink-multiple-records.component';


describe('DialogueUnlinkMultipleRecordsComponent', () => {
  let component: DialogueUnlinkMultipleRecordsComponent;
  let fixture: ComponentFixture<DialogueUnlinkMultipleRecordsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogueUnlinkMultipleRecordsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogueUnlinkMultipleRecordsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
