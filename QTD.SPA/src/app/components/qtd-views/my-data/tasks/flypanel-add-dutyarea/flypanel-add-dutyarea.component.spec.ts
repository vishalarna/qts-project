import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAddDutyareaComponent } from './flypanel-add-dutyarea.component';

describe('FlypanelAddDutyareaComponent', () => {
  let component: FlypanelAddDutyareaComponent;
  let fixture: ComponentFixture<FlypanelAddDutyareaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAddDutyareaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAddDutyareaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
