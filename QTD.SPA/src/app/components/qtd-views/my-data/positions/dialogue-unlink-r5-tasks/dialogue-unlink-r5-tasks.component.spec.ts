import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DialogueUnlinkR5TasksComponent } from './dialogue-unlink-r5-tasks.component';


describe('MatDialogueComponent', () => {
  let component: DialogueUnlinkR5TasksComponent;
  let fixture: ComponentFixture<DialogueUnlinkR5TasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogueUnlinkR5TasksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogueUnlinkR5TasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
