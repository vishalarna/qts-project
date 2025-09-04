import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddCoversheetsComponent } from './fly-panel-add-coversheets.component';

describe('FlyPanelAddCoversheetsComponent', () => {
  let component: FlyPanelAddCoversheetsComponent;
  let fixture: ComponentFixture<FlyPanelAddCoversheetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddCoversheetsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddCoversheetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
