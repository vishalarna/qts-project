import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelMetaIlaDataElementComponent } from './fly-panel-meta-ila-data-element.component';

describe('FlyPanelMetaIlaDataElementComponent', () => {
  let component: FlyPanelMetaIlaDataElementComponent;
  let fixture: ComponentFixture<FlyPanelMetaIlaDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelMetaIlaDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelMetaIlaDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
