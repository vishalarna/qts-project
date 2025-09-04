import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelMetaIlaIlaRequirementsComponent } from './fly-panel-meta-ila-ila-requirements.component';

describe('FlyPanelMetaIlaIlaRequirementsComponent', () => {
  let component: FlyPanelMetaIlaIlaRequirementsComponent;
  let fixture: ComponentFixture<FlyPanelMetaIlaIlaRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelMetaIlaIlaRequirementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelMetaIlaIlaRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
