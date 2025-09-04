import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddLocationComponent } from './fly-panel-add-location.component';

describe('FlyPanelAddLocationComponent', () => {
  let component: FlyPanelAddLocationComponent;
  let fixture: ComponentFixture<FlyPanelAddLocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddLocationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
