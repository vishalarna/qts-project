import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetaIdpDetailComponent } from './meta-idp-detail.component';

describe('MetaIdpDetailComponent', () => {
  let component: MetaIdpDetailComponent;
  let fixture: ComponentFixture<MetaIdpDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MetaIdpDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MetaIdpDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
