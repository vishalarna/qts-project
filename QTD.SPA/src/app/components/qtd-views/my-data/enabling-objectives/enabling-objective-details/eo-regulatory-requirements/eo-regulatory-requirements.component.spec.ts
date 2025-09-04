import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoRegulatoryRequirementsComponent } from './eo-regulatory-requirements.component';

describe('EoRegulatoryRequirementsComponent', () => {
  let component: EoRegulatoryRequirementsComponent;
  let fixture: ComponentFixture<EoRegulatoryRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoRegulatoryRequirementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoRegulatoryRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
