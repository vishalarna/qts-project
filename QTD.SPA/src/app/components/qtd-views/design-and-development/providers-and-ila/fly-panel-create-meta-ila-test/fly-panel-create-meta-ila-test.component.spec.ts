import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCreateMetaILATestComponent } from './fly-panel-create-meta-ila-test.component';

describe('FlyPanelCreateMetaILATestComponent', () => {
  let component: FlyPanelCreateMetaILATestComponent;
  let fixture: ComponentFixture<FlyPanelCreateMetaILATestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCreateMetaILATestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCreateMetaILATestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
