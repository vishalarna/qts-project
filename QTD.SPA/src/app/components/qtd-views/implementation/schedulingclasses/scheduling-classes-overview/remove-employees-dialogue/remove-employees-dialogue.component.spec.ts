import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveEmployeesDialogueComponent } from './remove-employees-dialogue.component';

describe('RemoveEmployeesDialogueComponent', () => {
  let component: RemoveEmployeesDialogueComponent;
  let fixture: ComponentFixture<RemoveEmployeesDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveEmployeesDialogueComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveEmployeesDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
