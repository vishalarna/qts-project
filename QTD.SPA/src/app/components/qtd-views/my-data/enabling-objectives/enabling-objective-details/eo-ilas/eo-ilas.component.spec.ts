import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoIlasComponent } from './eo-ilas.component';

describe('EoIlasComponent', () => {
  let component: EoIlasComponent;
  let fixture: ComponentFixture<EoIlasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoIlasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoIlasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
