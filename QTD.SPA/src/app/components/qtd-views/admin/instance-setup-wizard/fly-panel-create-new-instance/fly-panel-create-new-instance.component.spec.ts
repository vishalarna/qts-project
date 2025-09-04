import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCreateNewInstanceComponent } from './fly-panel-create-new-instance.component';

describe('FlyPanelCreateNewInstanceComponent', () => {
  let component: FlyPanelCreateNewInstanceComponent;
  let fixture: ComponentFixture<FlyPanelCreateNewInstanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCreateNewInstanceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCreateNewInstanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
