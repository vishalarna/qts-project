import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelMetaIlaComponent } from './fly-panel-meta-ila.component';

describe('FlyPanelMetaIlaComponent', () => {
  let component: FlyPanelMetaIlaComponent;
  let fixture: ComponentFixture<FlyPanelMetaIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelMetaIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelMetaIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
