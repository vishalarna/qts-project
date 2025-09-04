import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTestDataElementComponent } from './fly-panel-test-data-element.component';

describe('FlyPanelTestDataElementComponent', () => {
  let component: FlyPanelTestDataElementComponent;
  let fixture: ComponentFixture<FlyPanelTestDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTestDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTestDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
