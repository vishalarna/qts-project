import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelAddCertificationComponent } from './fly-panel-add-certification.component';

describe('FlyPanelAddCertificationComponent', () => {
  let component: FlyPanelAddCertificationComponent;
  let fixture: ComponentFixture<FlyPanelAddCertificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelAddCertificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelAddCertificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
