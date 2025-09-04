import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelRandomlySelectAllEosComponent } from './flypanel-randomly-select-all-eos.component';

describe('FlypanelRandomlySelectAllEosComponent', () => {
  let component: FlypanelRandomlySelectAllEosComponent;
  let fixture: ComponentFixture<FlypanelRandomlySelectAllEosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelRandomlySelectAllEosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelRandomlySelectAllEosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
