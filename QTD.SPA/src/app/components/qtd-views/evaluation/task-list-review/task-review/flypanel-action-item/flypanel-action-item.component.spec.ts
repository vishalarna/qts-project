import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelActionItemComponent } from './flypanel-action-item.component';

describe('FlypanelActionItemComponent', () => {
  let component: FlypanelActionItemComponent;
  let fixture: ComponentFixture<FlypanelActionItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelActionItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelActionItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
