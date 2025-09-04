import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteEmpDialogueComponent } from './delete-emp-dialogue.component';

describe('DeleteEmpDialogueComponent', () => {
  let component: DeleteEmpDialogueComponent;
  let fixture: ComponentFixture<DeleteEmpDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteEmpDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteEmpDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
