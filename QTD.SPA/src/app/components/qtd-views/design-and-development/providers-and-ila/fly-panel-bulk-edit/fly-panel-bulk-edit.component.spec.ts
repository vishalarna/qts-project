import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelBulkEditComponent } from './fly-panel-bulk-edit.component';

describe('FlyPanelBulkEditComponent', () => {
  let component: FlyPanelBulkEditComponent;
  let fixture: ComponentFixture<FlyPanelBulkEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelBulkEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelBulkEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
