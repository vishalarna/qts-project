import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelIlaDataElementComponent } from './fly-panel-ila-data-element.component';

describe('FlyPanelIlaDataElementComponent', () => {
  let component: FlyPanelIlaDataElementComponent;
  let fixture: ComponentFixture<FlyPanelIlaDataElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelIlaDataElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelIlaDataElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
