import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IdpDetailComponent } from './idp-detail.component';

describe('IdpDetailComponent', () => {
  let component: IdpDetailComponent;
  let fixture: ComponentFixture<IdpDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IdpDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IdpDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
