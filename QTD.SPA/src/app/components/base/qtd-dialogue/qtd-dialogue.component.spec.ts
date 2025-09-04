import { ComponentFixture, TestBed } from '@angular/core/testing';
import { QTDDialogueComponent } from './qtd-dialogue.component';


describe('MatDialogueComponent', () => {
  let component: QTDDialogueComponent;
  let fixture: ComponentFixture<QTDDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QTDDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QTDDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
