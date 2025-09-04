import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAddSubdutyareaComponent } from './flypanel-add-subdutyarea.component';

describe('FlypanelAddSubdutyareaComponent', () => {
  let component: FlypanelAddSubdutyareaComponent;
  let fixture: ComponentFixture<FlypanelAddSubdutyareaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAddSubdutyareaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAddSubdutyareaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
