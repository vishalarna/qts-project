import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelPretestDataElementComponent } from './fly-panel-pretest-data-element.component';

describe('FlyPanelPretestDataElementComponent', () => {
  let component: FlyPanelPretestDataElementComponent;
  let fixture: ComponentFixture<FlyPanelPretestDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelPretestDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelPretestDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
