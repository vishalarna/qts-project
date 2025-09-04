import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTestitemDataElementComponent } from './fly-panel-testitem-data-element.component';

describe('FlyPanelTestitemDataElementComponent', () => {
  let component: FlyPanelTestitemDataElementComponent;
  let fixture: ComponentFixture<FlyPanelTestitemDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTestitemDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTestitemDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
