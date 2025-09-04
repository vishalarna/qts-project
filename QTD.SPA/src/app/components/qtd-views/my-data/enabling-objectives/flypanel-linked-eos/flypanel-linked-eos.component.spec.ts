import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelLinkedEosComponent } from './flypanel-linked-eos.component';

describe('FlypanelLinkedEosComponent', () => {
  let component: FlypanelLinkedEosComponent;
  let fixture: ComponentFixture<FlypanelLinkedEosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelLinkedEosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelLinkedEosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
