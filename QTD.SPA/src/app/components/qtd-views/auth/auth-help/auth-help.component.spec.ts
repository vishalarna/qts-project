import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthHelpComponent } from './auth-help.component';

describe('AuthHelpComponent', () => {
  let component: AuthHelpComponent;
  let fixture: ComponentFixture<AuthHelpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthHelpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthHelpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
