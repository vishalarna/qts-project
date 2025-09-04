import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelSelectEoComponent } from './flypanel-select-eo.component';

describe('FlypanelSelectEoComponent', () => {
  let component: FlypanelSelectEoComponent;
  let fixture: ComponentFixture<FlypanelSelectEoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelSelectEoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelSelectEoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
