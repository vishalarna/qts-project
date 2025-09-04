import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateProcedureDialogueComponent } from './update-procedure-dialogue.component';

describe('UpdateProcedureDialogueComponent', () => {
  let component: UpdateProcedureDialogueComponent;
  let fixture: ComponentFixture<UpdateProcedureDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateProcedureDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateProcedureDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
