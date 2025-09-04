import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogueLimitEmpComponent } from './dialogue-limit-emp.component';

describe('DialogueLimitEmpComponent', () => {
  let component: DialogueLimitEmpComponent;
  let fixture: ComponentFixture<DialogueLimitEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogueLimitEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogueLimitEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
