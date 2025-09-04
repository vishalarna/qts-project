import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoNotLinkedComponent } from './flypanel-eo-not-linked.component';

describe('FlypanelEoNotLinkedComponent', () => {
  let component: FlypanelEoNotLinkedComponent;
  let fixture: ComponentFixture<FlypanelEoNotLinkedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoNotLinkedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoNotLinkedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
