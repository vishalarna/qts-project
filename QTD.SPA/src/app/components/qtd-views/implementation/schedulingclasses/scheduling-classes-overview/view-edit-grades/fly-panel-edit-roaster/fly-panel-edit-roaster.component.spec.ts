import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEditRoasterComponent } from './fly-panel-edit-roaster.component';

describe('FlyPanelEditRoasterComponent', () => {
  let component: FlyPanelEditRoasterComponent;
  let fixture: ComponentFixture<FlyPanelEditRoasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEditRoasterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEditRoasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
