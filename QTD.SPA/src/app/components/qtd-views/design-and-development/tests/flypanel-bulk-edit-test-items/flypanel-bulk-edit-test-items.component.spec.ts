import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelBulkEditTestItemsComponent } from './flypanel-bulk-edit-test-items.component';

describe('FlypanelBulkEditTestItemsComponent', () => {
  let component: FlypanelBulkEditTestItemsComponent;
  let fixture: ComponentFixture<FlypanelBulkEditTestItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelBulkEditTestItemsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelBulkEditTestItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
