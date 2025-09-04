import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddMetaIlaComponent } from './fly-panel-add-meta-ila.component';

describe('FlyPanelAddMetaIlaComponent', () => {
  let component: FlyPanelAddMetaIlaComponent;
  let fixture: ComponentFixture<FlyPanelAddMetaIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddMetaIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddMetaIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
