import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RosterBulkUpdateDialogComponent } from './roster-bulk-update-dialog.component';

describe('RosterBulkUpdateDialogComponent', () => {
  let component: RosterBulkUpdateDialogComponent;
  let fixture: ComponentFixture<RosterBulkUpdateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RosterBulkUpdateDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RosterBulkUpdateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
