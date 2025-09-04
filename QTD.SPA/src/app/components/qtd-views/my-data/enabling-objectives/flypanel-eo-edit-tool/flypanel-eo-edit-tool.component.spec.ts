import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoEditToolComponent } from './flypanel-eo-edit-tool.component';

describe('FlypanelEoEditToolComponent', () => {
  let component: FlypanelEoEditToolComponent;
  let fixture: ComponentFixture<FlypanelEoEditToolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoEditToolComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoEditToolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
