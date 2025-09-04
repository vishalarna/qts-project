import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelChangeEoComponent } from './flypanel-change-eo.component';

describe('FlypanelChangeEoComponent', () => {
  let component: FlypanelChangeEoComponent;
  let fixture: ComponentFixture<FlypanelChangeEoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelChangeEoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelChangeEoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
