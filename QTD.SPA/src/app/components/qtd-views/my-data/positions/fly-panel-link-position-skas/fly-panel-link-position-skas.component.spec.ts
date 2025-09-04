import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkPositionSkasComponent } from './fly-panel-link-position-skas.component';

describe('FlyPanelLinkPositionSkasComponent', () => {
  let component: FlyPanelLinkPositionSkasComponent;
  let fixture: ComponentFixture<FlyPanelLinkPositionSkasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkPositionSkasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkPositionSkasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
