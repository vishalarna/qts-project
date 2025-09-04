import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelDownlodIlasComponent } from './fly-panel-downlod-ilas.component';

describe('FlyPanelDownlodIlasComponent', () => {
  let component: FlyPanelDownlodIlasComponent;
  let fixture: ComponentFixture<FlyPanelDownlodIlasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelDownlodIlasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelDownlodIlasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
