import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCoversheetsComponent } from './fly-panel-coversheets.component';

describe('FlyPanelCoversheetsComponent', () => {
  let component: FlyPanelCoversheetsComponent;
  let fixture: ComponentFixture<FlyPanelCoversheetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCoversheetsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCoversheetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
