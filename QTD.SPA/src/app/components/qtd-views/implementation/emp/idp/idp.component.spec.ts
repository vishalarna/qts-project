import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IDPComponent } from './idp.component';

describe('IDPComponent', () => {
  let component: IDPComponent;
  let fixture: ComponentFixture<IDPComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IDPComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IDPComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
