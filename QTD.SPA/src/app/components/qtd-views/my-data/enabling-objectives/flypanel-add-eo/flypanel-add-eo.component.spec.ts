import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAddEoComponent } from './flypanel-add-eo.component';

describe('FlypanelAddEoComponent', () => {
  let component: FlypanelAddEoComponent;
  let fixture: ComponentFixture<FlypanelAddEoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAddEoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAddEoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
