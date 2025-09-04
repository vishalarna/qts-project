import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelCertifyingBodyComponent } from './fly-panel-certifying-body.component';

describe('FlyPanelCertifyingBodyComponent', () => {
  let component: FlyPanelCertifyingBodyComponent;
  let fixture: ComponentFixture<FlyPanelCertifyingBodyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelCertifyingBodyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelCertifyingBodyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
