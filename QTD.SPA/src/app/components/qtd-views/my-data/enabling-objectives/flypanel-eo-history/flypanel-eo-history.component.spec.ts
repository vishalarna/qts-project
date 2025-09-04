import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoHistoryComponent } from './flypanel-eo-history.component';

describe('FlypanelEoHistoryComponent', () => {
  let component: FlypanelEoHistoryComponent;
  let fixture: ComponentFixture<FlypanelEoHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
