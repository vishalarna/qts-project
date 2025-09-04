import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DialogueUnlinkObjectivesComponent } from './dialogue-unlink-objectives.component';


describe('DialogueUnlinkObjectivesComponent', () => {
  let component: DialogueUnlinkObjectivesComponent;
  let fixture: ComponentFixture<DialogueUnlinkObjectivesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogueUnlinkObjectivesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogueUnlinkObjectivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
