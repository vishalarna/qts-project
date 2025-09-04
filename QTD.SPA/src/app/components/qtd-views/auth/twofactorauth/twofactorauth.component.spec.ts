import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwoFactorAuthComponent } from './twofactorauth.component';

describe('TwoFactorAuthComponent', () => {
  let component: TwoFactorAuthComponent;
  let fixture: ComponentFixture<TwoFactorAuthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TwoFactorAuthComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TwoFactorAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
